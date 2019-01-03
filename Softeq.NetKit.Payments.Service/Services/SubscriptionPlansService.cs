// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Mappers;
using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Request;
using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Response;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Stripe;

namespace Softeq.NetKit.Payments.Service.Services
{
    /// <summary>
    /// Subscription Plans Facade to manage the subscription plans for your application.
    /// </summary>
    public class SubscriptionPlansService : ISubscriptionPlansService
    {
        private readonly ISubscriptionPlanDataService _subscriptionPlanDataService;
        private readonly ISubscriptionPlanProvider _subscriptionPlanProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriptionPlansService"/> class.
        /// </summary>
        /// <param name="subscriptionPlanDataService">The data.</param>
        /// <param name="subscriptionPlanProvider">The plan provider.</param>
        public SubscriptionPlansService(ISubscriptionPlanDataService subscriptionPlanDataService, ISubscriptionPlanProvider subscriptionPlanProvider)
        {
            _subscriptionPlanDataService = subscriptionPlanDataService;
            _subscriptionPlanProvider = subscriptionPlanProvider;
        }

        /// <summary>
        /// Gets all subscription plans asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SubscriptionPlanResponse>> GetAllAsync()
        {
            var subscriptionPlans = await _subscriptionPlanDataService.GetAllAsync();
            return subscriptionPlans.Select(x => x.ToSubscriptionPlanResponse());
        }

        /// <summary>
        /// Adds the subscription plan asynchronous.
        /// </summary>
        /// <param name="request">The subscription plan request model.</param>
        /// <returns></returns>
        public async Task AddAsync(CreateSubscriptionPlanRequest request)
        {
            try
            {
                var stripeSubscriptionPlan = (StripePlan) _subscriptionPlanProvider.Add(request);
                var newSubscriptionPlan = new SubscriptionPlan
                {
                    StripeId = stripeSubscriptionPlan.Id,
                    Currency = request.Currency,
                    Status = request.Status,
                    Interval = request.Interval,
                    Name = request.Name,
                    Price = request.Price,
                    TrialPeriodInDays = request.TrialPeriodInDays,
                    Properties = request.Properties
                };
                await _subscriptionPlanDataService.AddAsync(newSubscriptionPlan);
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        /// <summary>
        /// Updates the subscription plan asynchronous.
        /// </summary>
        /// <param name="request">The subscription plan request model.</param>
        /// <returns></returns>
        public async Task UpdateAsync(UpdateSubscriptionPlanRequest request)
        {
            try
            {
                var subscriptionPlan = await _subscriptionPlanDataService.FindAsync(request.PlanId);
                subscriptionPlan.Name = request.Name;
                subscriptionPlan.Properties = request.Properties;
                await _subscriptionPlanDataService.UpdateAsync(subscriptionPlan);
                _subscriptionPlanProvider.Update(subscriptionPlan);
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        /// <summary>
        /// Deletes the subscription plan asynchronous.
        /// </summary>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">subscriptionPlanId</exception>
        public async Task DeleteAsync(Guid subscriptionPlanId)
        {
            try
            {
                var plan = await _subscriptionPlanDataService.FindAsync(subscriptionPlanId);

                if (plan.Status == SubscriptionPlanStatus.Enabled)
                {
                    var countUsersInPlan = await _subscriptionPlanDataService.CountUsersAsync(plan.Id);

                    // If plan has users only disable
                    if (countUsersInPlan > 0)
                    {
                        await _subscriptionPlanDataService.DisableAsync(subscriptionPlanId);
                    }
                    else
                    {
                        await _subscriptionPlanDataService.DeleteAsync(subscriptionPlanId);
                    }

                    // Delete from Stripe
                    _subscriptionPlanProvider.Delete(plan.StripeId);
                }
            }
            catch (StripeException ex)
            {
                throw new ServiceException(new ErrorDto(ex.StripeError.Code, ex.StripeError.Message));
            }
        }

        /// <summary>
        /// Finds the subscription plan asynchronous.
        /// </summary>
        /// <param name="subscriptionPlanId">The subscription plan identifier.</param>
        /// <returns>The Subscription Plan</returns>
        public async Task<SubscriptionPlanResponse> FindAsync(Guid subscriptionPlanId)
        {
            var subscriptionPlan = (await _subscriptionPlanDataService.FindAsync(subscriptionPlanId))
                .ToSubscriptionPlanResponse();
            return subscriptionPlan;
        }
    }
}