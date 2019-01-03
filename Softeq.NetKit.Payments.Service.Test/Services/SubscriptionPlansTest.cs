// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Linq;
using Autofac;
using Softeq.NetKit.Payments.Service.Test.Abstract;
using Softeq.NetKit.Payments.Service.Test.Data;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Xunit;

namespace Softeq.NetKit.Payments.Service.Test.Services
{
    public class SubscriptionPlansTest : BaseTest
    {
        private const string StripeSubscriptionPlanId = "plan_CfRT2dCEcYnMu0";
        private const string SaasUserId = "d865176a-f08f-4123-8db2-ec2e35f4d4a4";

        private readonly Guid _userId = new Guid("0c8f803d-668e-4d95-aee2-1a0467f9ba5c");
        private readonly Guid _subscriptionPlanId = new Guid("8ab4c732-c839-4fa5-97ef-b38d3b9c6e77");

        private readonly ISubscriptionPlanDataService _subscriptionPlanDataService;
        private readonly ISubscriptionDataService _subscriptionDataService;

        public SubscriptionPlansTest()
        {
            _subscriptionPlanDataService = LifetimeScope.Resolve<ISubscriptionPlanDataService>();
            _subscriptionDataService = LifetimeScope.Resolve<ISubscriptionDataService>();
        }

        [Fact]
        public async void GetAllSubscriptionPlansAsyncTest()
        {
            var subscriptionPlans = await _subscriptionPlanDataService.GetAllAsync();
            Assert.NotNull(subscriptionPlans);
        }

        [Fact]
        public async void GetSubscriptionPlanByIdAsyncTest()
        {
            await _subscriptionPlanDataService.FindAsync(_subscriptionPlanId);
        }

        [Fact]
        public async void CreateSubscriptionPlanAsyncTest()
        {
            var plan = TestData.CreateSubscriptionPlanRequest();
            await _subscriptionPlanDataService.AddAsync(plan);
            var newPlan = await _subscriptionPlanDataService.FindAsync(plan.Id);
            Assert.Equal(newPlan.StripeId, plan.StripeId);
            Assert.Equal(newPlan.TrialPeriodInDays, plan.TrialPeriodInDays);
            Assert.Equal(newPlan.Currency, plan.Currency);
            Assert.Equal(newPlan.Interval, plan.Interval);
            Assert.Equal(newPlan.Name, plan.Name);
            Assert.Equal(newPlan.Price, plan.Price);
            Assert.Equal(newPlan.Status, plan.Status);
        }

        [Fact]
        public async void UpdateSubscriptionPlanAsyncTest()
        {
            await _subscriptionPlanDataService.AddAsync(TestData.CreateSubscriptionPlanRequest());
            var plan = (await _subscriptionPlanDataService.GetAllAsync()).First();
            var updatedPlan = TestData.UpdateSubscriptionPlanRequest();
            plan.Name = updatedPlan.Name;
            await _subscriptionPlanDataService.UpdateAsync(plan);
            var newPlan = await _subscriptionPlanDataService.FindAsync(plan.Id);
            Assert.Equal(newPlan.Name, plan.Name);
        }

        [Fact]
        public async void DeleteSubscriptionPlanAsyncTest()
        {
            var testPlan = TestData.CreateSubscriptionPlanRequest();
            await _subscriptionPlanDataService.AddAsync(testPlan);
            var previousSubscriptionPlans = await _subscriptionPlanDataService.GetAllAsync();
            var plan = previousSubscriptionPlans.First();
            await _subscriptionPlanDataService.DeleteAsync(plan.Id);
            var currentSubscriptionPlans = await _subscriptionPlanDataService.GetAllAsync();
            Assert.True(previousSubscriptionPlans.Count > currentSubscriptionPlans.Count);
        }

        [Fact]
        public async void DisableSubscriptionPlanAsyncTest()
        {
            var testPlan = TestData.CreateSubscriptionPlanRequest();
            await _subscriptionPlanDataService.AddAsync(testPlan);
            var plan = (await _subscriptionPlanDataService.GetAllAsync()).First();
            await _subscriptionPlanDataService.DisableAsync(plan.Id);
            var newPlan = await _subscriptionPlanDataService.FindAsync(plan.Id);
            Assert.True(newPlan.Status == SubscriptionPlanStatus.Disabled);
        }

        [Fact]
        public async void CountUsersAsyncTest()
        {
            await _subscriptionDataService.SubscribeUserAsync(SaasUserId, _userId, StripeSubscriptionPlanId);
            var subscription = (await _subscriptionDataService.UserSubscriptionsAsync(SaasUserId)).First();
            subscription.End = DateTime.UtcNow.AddDays(2);
            subscription.TrialEnd = DateTime.UtcNow.AddDays(4);
            await _subscriptionDataService.UpdateSubscriptionAsync(subscription);
            var count = await _subscriptionPlanDataService.CountUsersAsync(_subscriptionPlanId);
            Assert.Equal(count, 1);
        }
    }
}