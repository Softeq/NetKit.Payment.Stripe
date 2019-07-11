// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Linq;
using Autofac;
using Softeq.NetKit.Payments.Service.Test.Abstract;
using Softeq.NetKit.Payments.Service.Test.Data;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Xunit;

namespace Softeq.NetKit.Payments.Service.Test.Services
{
    public class SubscriptionsTest : BaseTest
    {
        private const string StripeSubscriptionPlanId = "plan_CfRT2dCEcYnMu0";
        private const string SaasUserId = "d865176a-f08f-4123-8db2-ec2e35f4d4a4";

        private readonly Guid _userId = new Guid("0c8f803d-668e-4d95-aee2-1a0467f9ba5c");
        private readonly Guid _subscriptionPlanId = new Guid("8ab4c732-c839-4fa5-97ef-b38d3b9c6e77");

        private readonly ISubscriptionDataService _subscriptionDataService;

        public SubscriptionsTest()
        {
            _subscriptionDataService = LifetimeScope.Resolve<ISubscriptionDataService>();
        }

        [Fact]
        public async void SubscribeUserAsyncTest()
        {
            var previousUserSubscriptions = await _subscriptionDataService.UserActiveSubscriptionsAsync(SaasUserId);
            Assert.NotNull(previousUserSubscriptions);
            await _subscriptionDataService.SubscribeUserAsync(SaasUserId, _userId, StripeSubscriptionPlanId);
            var currentUserSubscriptions = await _subscriptionDataService.UserActiveSubscriptionsAsync(SaasUserId);
            Assert.True(previousUserSubscriptions.Count < currentUserSubscriptions.Count);
            Assert.NotNull(currentUserSubscriptions);
        }

        [Fact]
        public async void GetAllUserSubscriptionsAsyncTest()
        {
            var userSubscriptions = await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId);
            Assert.NotNull(userSubscriptions);
        }

        [Fact]
        public async void GetAllActiveUserSubscriptionsAsyncTest()
        {
            var userSubscriptions = await _subscriptionDataService.UserActiveSubscriptionsAsync(SaasUserId);
            Assert.NotNull(userSubscriptions);
        }

        [Fact]
        public async void GetUserActiveSubscriptionAsyncTest()
        {
            await _subscriptionDataService.SubscribeUserAsync(SaasUserId, _userId, StripeSubscriptionPlanId);
            var subscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId))
                .FirstOrDefault();
            Assert.NotNull(subscription);
            subscription.End = DateTime.UtcNow.AddDays(20);
            subscription.Status = "active";
            await _subscriptionDataService.UpdateSubscriptionAsync(subscription);
            await _subscriptionDataService.UserActiveSubscriptionAsync(SaasUserId, subscription.StripeId);
        }

        [Fact]
        public async void GetSubscriptionByIdAsyncTest()
        {
            await _subscriptionDataService.SubscribeUserAsync(SaasUserId, _userId, StripeSubscriptionPlanId);
            var subscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId)).First();
            var newSubscription = await _subscriptionDataService.FindByIdAsync(subscription.StripeId);
            Assert.NotNull(newSubscription);
        }

        [Fact]
        public async void EndSubscriptionAsyncTest()
        {
            await _subscriptionDataService.SubscribeUserAsync(SaasUserId, _userId, StripeSubscriptionPlanId);
            var subscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId)).First();
            var endDateTime = DateTime.UtcNow.AddDays(-2);
            await _subscriptionDataService.CancelSubscriptionAsync(subscription.StripeId, endDateTime);
            var updatedSubscription = await _subscriptionDataService.FindByIdAsync(subscription.StripeId);
            Assert.NotNull(updatedSubscription);
            Assert.Equal(updatedSubscription.End, endDateTime);
        }

        [Fact]
        public async void UpdateSubscriptionAsyncTest()
        {
            await _subscriptionDataService.SubscribeUserAsync(SaasUserId, _userId, StripeSubscriptionPlanId);
            var subscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId)).First();
            var testData = TestData.UpdateSubscriptionRequest(SaasUserId, _userId, _subscriptionPlanId);
            subscription.SaasUserId = testData.SaasUserId;
            subscription.End = testData.End;
            subscription.TrialEnd = testData.TrialEnd;
            subscription.Status = testData.Status;
            subscription.StripeId = testData.StripeId;
            subscription.TaxPercent = testData.TaxPercent;
            subscription.TrialStart = testData.TrialStart;
            subscription.UserId = testData.UserId;
            subscription.CancelReason = testData.CancelReason;
            subscription.SubscriptionPlanId = testData.SubscriptionPlanId;
            subscription.Start = testData.Start;
            await _subscriptionDataService.UpdateSubscriptionAsync(subscription);
            var newSubscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId))
                .FirstOrDefault();
            Assert.NotNull(newSubscription);
            Assert.Equal(newSubscription.UserId, testData.UserId);
            Assert.Equal(newSubscription.CancelReason, testData.CancelReason);
            Assert.Equal(newSubscription.End, testData.End);
            Assert.Equal(newSubscription.Status, testData.Status);
            Assert.Equal(newSubscription.Start, testData.Start);
            Assert.Equal(newSubscription.StripeId, testData.StripeId);
            Assert.Equal(newSubscription.SubscriptionPlanId, testData.SubscriptionPlanId);
            Assert.Equal(newSubscription.TaxPercent, testData.TaxPercent);
            Assert.Equal(newSubscription.TrialEnd, testData.TrialEnd);
            Assert.Equal(newSubscription.TrialStart, testData.TrialStart);
            Assert.Equal(newSubscription.SaasUserId, testData.SaasUserId);
        }

        [Fact]
        public async void UpdateSubscriptionTaxAsyncTest()
        {
            await _subscriptionDataService.SubscribeUserAsync(SaasUserId, _userId, StripeSubscriptionPlanId);
            var subscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId)).First();
            var taxPercent = 60;
            await _subscriptionDataService.UpdateSubscriptionTax(subscription.Id, taxPercent);
            var newSubscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId))
                .FirstOrDefault();
            Assert.NotNull(newSubscription);
            Assert.Equal(newSubscription.TaxPercent, taxPercent);
        }

        [Fact]
        public async void DeleteSubscriptionsAsyncTest()
        {
            await _subscriptionDataService.SubscribeUserAsync(SaasUserId, _userId, StripeSubscriptionPlanId);
            var subscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId)).First();
            subscription.End = DateTime.UtcNow.AddDays(2);
            subscription.TrialEnd = DateTime.UtcNow.AddDays(5);
            await _subscriptionDataService.UpdateSubscriptionAsync(subscription);
            var previousUserActiveSubscriptions = await _subscriptionDataService.UserActiveSubscriptionsAsync(SaasUserId);
            await _subscriptionDataService.CancelSubscriptionsAsync(SaasUserId);
            var currentUserActiveSubscriptions = await _subscriptionDataService.UserActiveSubscriptionsAsync(SaasUserId);
            Assert.True(previousUserActiveSubscriptions.Count > currentUserActiveSubscriptions.Count);
        }
    }
}