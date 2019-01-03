// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.Extensions;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.Service.DataServices.Storage
{
    public class SubscriptionDataService : BaseService, ISubscriptionDataService
    {
        public SubscriptionDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <inheritdoc />
        public async Task<Subscription> FindByIdAsync(string subscriptionId)
        {
            var subscription = await UnitOfWork.SubscriptionRepository.Query(x => x.StripeId == subscriptionId)
                .FirstOrDefaultAsync();
            if (subscription == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Subscription does not exist."));
            }

            return subscription;
        }

        /// <inheritdoc />
        public async Task<Subscription> SubscribeUserAsync(string saasUserId, Guid userId, string planId, string subscriptionId = null, int? trialPeriodInDays = null, decimal taxPercent = 0)
        {
            var plan = await UnitOfWork.SubscriptionPlanRepository.Query(x => x.StripeId == planId)
                .FirstOrDefaultAsync();

            if (plan == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Subscription plan does not exist."));
            }

            var now = DateTime.UtcNow;
            var subscription = new Subscription
            {
                Start = now,
                End = null,
                TrialEnd = now.AddDays(trialPeriodInDays ?? plan.TrialPeriodInDays),
                TrialStart = now,
                UserId = userId,
                SubscriptionPlan = plan,
                Status = trialPeriodInDays == null ? "active" : "trialing",
                TaxPercent = taxPercent,
                StripeId = subscriptionId,
                SaasUserId = saasUserId,
            };

            UnitOfWork.SubscriptionRepository.Add(subscription);
            await UnitOfWork.SaveChangesAsync();

            return subscription;
        }

        /// <inheritdoc />
        public async Task<List<Subscription>> UserSubscriptionsAsync(string userId)
        {
            var subscriptions = await UnitOfWork.SubscriptionRepository.Query(x => x.SaasUserId == userId)
                .ToListAsync();
            return subscriptions;
        }

        /// <inheritdoc />
        public async Task<Subscription> UserActiveSubscriptionAsync(string userId, string subscriptionId)
        {
            var subscription = (await UserActiveSubscriptionsAsync(userId))
                .FirstOrDefault(x => x.StripeId == subscriptionId);
            if (subscription == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "User does not have active subscriptions."));
            }

            return subscription;
        }

        /// <inheritdoc />
        public async Task<List<Subscription>> UserActiveSubscriptionsAsync(string userId)
        {
            var activeSubscriptions = await UnitOfWork.SubscriptionRepository
                .Query(x => x.SaasUserId == userId &&
                            x.Status != "canceled" && x.Status != "unpaid" &&
                            (x.End == null || x.End > DateTime.UtcNow))
                .Include(x => x.SubscriptionPlan)
                .ThenInclude(x => x.Properties)
                .ToListAsync();
            return activeSubscriptions;
        }

        /// <inheritdoc />
        public async Task EndSubscriptionAsync(string subscriptionId, DateTime subscriptionEnDateTime)
        {
            var subscription = await UnitOfWork.SubscriptionRepository.Query(x => x.StripeId == subscriptionId)
                .FirstOrDefaultAsync();
            if (subscription == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Subscription does not exist."));
            }

            subscription.End = subscriptionEnDateTime;
            await UpdateSubscriptionAsync(subscription);
        }

        /// <inheritdoc />
        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            UnitOfWork.SubscriptionRepository.Update(subscription);
            await UnitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateSubscriptionTax(Guid subscriptionId, decimal taxPercent)
        {
            var subscription = UnitOfWork.SubscriptionRepository.GetById(subscriptionId);
            if (subscription == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Subscription does not exist."));
            }

            subscription.TaxPercent = taxPercent;
            await UpdateSubscriptionAsync(subscription);
        }

        /// <inheritdoc />
        public async Task DeleteSubscriptionsAsync(string userId)
        {
            var subscriptions = await UnitOfWork.SubscriptionRepository.Query(x => x.SaasUserId == userId &&
                                                                                   x.Status != "canceled" && x.Status != "unpaid" &&
                                                                                   (x.TrialEnd == null || x.TrialEnd > DateTime.UtcNow))
                .ToListAsync();
            var newSubscriptions = subscriptions.UpdateSubscriptionTimeEnd();
            foreach (var subscription in newSubscriptions)
            {
                UnitOfWork.SubscriptionRepository.Update(subscription);
            }

            await UnitOfWork.SaveChangesAsync();
        }
    }
}