// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Charge.Request;
using Softeq.NetKit.Payments.Service.TransportModels.Subscription.Request;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Stripe;

namespace Softeq.NetKit.Payments.Service.Services
{
    /// <summary>
    /// Subscriptions Facade to manage the subscription for your application's users.
    /// </summary>
    /// <remarks>
    /// 	<para>This is one of the main classes that you will instantiate from your application to interact with SaasEcom.Core. This class is using internally the data
    /// services to store all the billing related data in the database, as well as the Payment Provider to inegrate all the billing data with Stripe and keep it in
    /// sync.</para>
    /// </remarks>
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly ISubscriptionDataService _subscriptionDataService;
        private readonly ISubscriptionPlanDataService _subscriptionPlanDataService;
        private readonly ISubscriptionProvider _subscriptionProvider;
        private readonly ICustomerProvider _customerProvider;
        private readonly IChargeService _chargeService;
        private readonly ICardDataService _cardDataService;
        private readonly IUserDataService _userDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionsService" /> class.
        /// </summary>
        /// <param name="subscriptionDataService">The subscription data service.</param>
        /// <param name="subscriptionProvider">The subscription provider.</param>
        /// <param name="cardDataService">The card data service.</param>
        /// <param name="customerProvider">The customer provider.</param>
        /// <param name="subscriptionPlanDataService">The subscription plan data service.</param>
        /// <param name="userDataService">The user data service.</param>
        /// <param name="chargeService">The charge service.</param>
        public SubscriptionsService(
            ISubscriptionDataService subscriptionDataService,
            ISubscriptionPlanDataService subscriptionPlanDataService,
            ISubscriptionProvider subscriptionProvider,
            ICustomerProvider customerProvider,
            ICardDataService cardDataService,
            IUserDataService userDataService,
            IChargeService chargeService)
        {
            _subscriptionDataService = subscriptionDataService;
            _subscriptionPlanDataService = subscriptionPlanDataService;
            _subscriptionProvider = subscriptionProvider;
            _customerProvider = customerProvider;
            _cardDataService = cardDataService;
            _userDataService = userDataService;
            _chargeService = chargeService;
        }

        /// <summary>
        /// Subscribes the user to a Stripe plan. If the user doesn't exist in Stripe, is created
        /// </summary>
        /// <param name="request">Create subscription request model.</param>
        /// <returns>
        /// Subscription
        /// </returns>
        public async Task<Subscription> SubscribeUserAsync(CreateSubscriptionRequest request)
        {
            try
            {
                var user = await _userDataService.GetAsync(request.UserId);
                await _cardDataService.FindAsync(request.UserId, request.CardId);

                // Save subscription details in Stripe
                var stripeSubscriptionId = await _subscriptionProvider.SubscribeUserAsync(user.StripeCustomerId, request.PlanId);

                // Save subscription details in Db
                var subscription = await _subscriptionDataService.SubscribeUserAsync(request.UserId, user.Id, request.PlanId, stripeSubscriptionId);

                return subscription;
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        /// <summary>
        /// Cancel subscription from Stripe
        /// </summary>
        /// <param name="request">End subscription request</param>
        /// <returns>The Date when the subscription ends (it can be future if cancelAtPeriodEnd is true)</returns>
        public async Task<DateTime?> CancelSubscriptionAsync(EndSubscriptionRequest request)
        {
            try
            {
                var subscription = await _subscriptionDataService.UserActiveSubscriptionAsync(request.UserId, request.SubscriptionId);
                var subscriptionEnd = await _subscriptionProvider.EndSubscriptionAsync(subscription.StripeId, request.CancelAtPeriodEnd);
                await _subscriptionDataService.CancelSubscriptionAsync(request.SubscriptionId, subscriptionEnd);
                return subscriptionEnd;
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        /// <summary>
        /// Updates the subscription asynchronous, if the new plan is more expensive the customer is charged immediately
        /// </summary>
        /// <param name="request">The update subscription request model.</param>
        /// <returns></returns>
        public async Task UpdateSubscriptionAsync(UpdateSubscriptionRequest request)
        {
            try
            {
                await _userDataService.GetAsync(request.UserId);
                var subscription = await _subscriptionDataService.FindByIdAsync(request.SubscriptionId);

                // Check end date in case that we are re-activating
                if (subscription.SubscriptionPlanId != request.PlanId || subscription.End != null)
                {
                    var changingPlan = subscription.SubscriptionPlanId != request.PlanId;

                    var currentPlan = await _subscriptionPlanDataService.FindAsync(subscription.SubscriptionPlanId);
                    var newPlan = await _subscriptionPlanDataService.FindAsync(request.PlanId);

                    // Do Stripe charge if the new plan is more expensive
                    if (changingPlan && currentPlan.Price < newPlan.Price)
                    {
                        var upgradeCharge = await CalculateProrate(request.PlanId) -
                                            await CalculateProrate(subscription.SubscriptionPlanId);

                        var upgradeChargeWithTax = upgradeCharge * (1 + subscription.TaxPercent / 100);

                        var planCurrency = await GetPlanCurrency(request.PlanId);
                        await _chargeService.CreateChargeAsync(new CreateChargeRequest
                        {
                            Amount = (int) upgradeChargeWithTax,
                            CardSourceId = null,
                            Currency = planCurrency,
                            Description = "Fluxifi Upgrade",
                            UserId = request.UserId
                        });
                    }

                    //Update Stripe
                    await _subscriptionProvider.UpdateSubscriptionAsync(subscription.StripeId, newPlan.StripeId, request.Prorate);

                    // Update DB
                    subscription.SubscriptionPlanId = request.PlanId;
                    subscription.End = null; // In case that we are reactivating
                    await _subscriptionDataService.UpdateSubscriptionAsync(subscription);
                }
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        /// <summary>
        /// Get a list of active subscriptions for the User
        /// </summary>
        /// <param name="userId">Application User PlanId</param>
        /// <returns>List of Active Subscriptions</returns>
        public async Task<List<Subscription>> GetUserActiveSubscriptionsAsync(string userId)
        {
            return await _subscriptionDataService.UserActiveSubscriptionsAsync(userId);
        }

        /// <summary>This method returns the number of days of trial left for a given user. It will return 0 if there aren't any days left or no active subscriptions for the user.</summary>
        /// <param name="userId">The user identifier.</param>
        /// /// <param name="subscriptionId">The subscription identifier.</param>
        /// <returns></returns>
        /// <exception caption="" cref="System.NotImplementedException"></exception>
        public async Task<int> GetDaysTrialLeftAsync(string userId, string subscriptionId)
        {
            var currentSubscription = (await GetUserActiveSubscriptionsAsync(userId))
                .FirstOrDefault(x => x.StripeId == subscriptionId);

            if (currentSubscription == null)
            {
                return 0;
            }

            if (currentSubscription.IsTrialing() && currentSubscription.TrialEnd.HasValue)
            {
                var timeSpan = currentSubscription.TrialEnd.Value - DateTime.UtcNow;
                return timeSpan.Hours > 12 ? timeSpan.Days + 1 : timeSpan.Days;
            }

            return 0;
        }

        /// <summary>
        /// Subscribes the user, with a billing cycle that goes from the 1st of the month asynchronous.
        /// Creates the user in Stripe if doesn't exist already.
        /// Saves de Subscription data in the database if the subscription suceeds.
        /// </summary>
        /// <param name="request">Create subscription request model.</param>
        /// <returns></returns>
        public async Task SubscribeUserWithBillingCycleAsync(CreateSubscriptionRequest request)
        {
            try
            {
                var user = await _userDataService.GetAsync(request.UserId);
                await _cardDataService.FindAsync(request.UserId, request.CardId);

                // Save subscription details in Stripe
                var stripeSubscription = await _subscriptionProvider.SubscribeUserNaturalMonthAsync(user.StripeCustomerId, request.PlanId, GetStartNextMonth(), request.TaxPercent);

                // Save subscription details in Db
                await _subscriptionDataService.SubscribeUserAsync(request.UserId, user.Id, request.PlanId, stripeSubscription.Id, null, request.TaxPercent);
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        /// <summary>
        /// Deletes the subscriptions.
        /// </summary>
        /// <param name="request">The end subscriptions identifier.</param>
        /// <returns></returns>
        public async Task CancelSubscriptionsAsync(EndSubscriptionsRequest request)
        {
            try
            {
                // Cancel subscriptions in Stripe
                await EndSubscriptionsAsync(new EndSubscriptionsRequest(request.UserId, request.CustomerId, request.CancelAtPeriodEnd));
                // Delete from Db
                await _subscriptionDataService.CancelSubscriptionsAsync(request.UserId);
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        #region Helpers

        private async Task EndSubscriptionsAsync(EndSubscriptionsRequest request)
        {
            var subscriptions = await _customerProvider.GetUserActiveSubscriptionsAsync(request.CustomerId);
            foreach (var subscription in subscriptions)
            {
                await _subscriptionProvider.EndSubscriptionAsync(subscription.Id, request.CancelAtPeriodEnd);
            }
        }

        private async Task<string> GetPlanCurrency(Guid planId)
        {
            var plan = await _subscriptionPlanDataService.FindAsync(planId);

            return plan.Currency;
        }

        private async Task<int> CalculateProrate(Guid planId)
        {
            var plan = await _subscriptionPlanDataService.FindAsync(planId);

            var now = DateTime.UtcNow;
            var beginningMonth = new DateTime(now.Year, now.Month, 1);
            var endMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month), 23, 59, 59);

            var totalHoursMonth = (endMonth - beginningMonth).TotalHours;
            var hoursRemaining = (endMonth - now).TotalHours;

            var amountInCurrency = plan.Price * hoursRemaining / totalHoursMonth;

            switch (plan.Currency.ToLower())
            {
                case ("usd"):
                case ("gbp"):
                case ("eur"):
                    return (int) Math.Ceiling(amountInCurrency * 100);
                default:
                    return (int) Math.Ceiling(amountInCurrency);
            }
        }

        private DateTime? GetStartNextMonth()
        {
            var now = DateTime.UtcNow;
            var year = now.Month == 12 ? now.Year + 1 : now.Year;
            var month = now.Month == 12 ? 1 : now.Month + 1;

            return new DateTime(year, month, 1);
        }

        #endregion
    }
}