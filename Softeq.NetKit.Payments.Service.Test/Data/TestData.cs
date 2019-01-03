// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.Card;
using Softeq.NetKit.Payments.Data.Models.Charge;
using Softeq.NetKit.Payments.Data.Models.Invoice;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Data.Models.User;

namespace Softeq.NetKit.Payments.Service.Test.Data
{
    public static class TestData
    {
        public static CreditCard CreateCreditCardRequest(string saasUserId, Guid userId)
        {
            var request = new CreditCard
            {
                Id = Guid.NewGuid(),
                CardCountry = "US",
                ExpirationMonth = 10,
                ExpirationYear = 2021,
                Fingerprint = "test",
                Last4 = "4424",
                SaasUserId = saasUserId,
                StripeCustomerId = "testStripeCustomerId",
                StripeId = "testStripeId",
                UserId = userId
            };
            return request;
        }

        public static Charge CreateChargeRequest(string saasUserId, Guid creditCardId, Guid userId)
        {
            var request = new Charge
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Currency = "USD",
                Amount = 100,
                CreditCardId = creditCardId,
                StripeId = "testStripeId",
                Description = "test",
                SaasUserId = saasUserId
            };
            return request;
        }

        public static User CreateUserRequest(string saasUserId)
        {
            var request = new User
            {
                Id = Guid.NewGuid(),
                StripeCustomerId = "test",
                Delinquent = false,
                SaasUserId = saasUserId
            };
            return request;
        }

        public static Invoice CreateInvoiceRequest(string saasUserId)
        {
            var request = new Invoice
            {
                Id = Guid.NewGuid(),
                AmountDue = 100,
                AttemptCount = 10,
                Attempted = false,
                Closed = false,
                Currency = "USD",
                EndingBalance = 100,
                Forgiven = false,
                Paid = true,
                PeriodEnd = DateTime.UtcNow.AddDays(-2),
                PeriodStart = DateTime.UtcNow.AddDays(-3),
                StartingBalance = 200,
                StripeCustomerId = "cus_Ci2LHKWjPi70QX",
                StripeId = "in_1CJ2Y6KnOAUQyqMUBlvsHio5",
                Subtotal = 5,
                Total = 10,
                SaasUserId = saasUserId
            };
            return request;
        }

        public static Invoice UpdateInvoiceRequest(string saasUserId)
        {
            var request = new Invoice
            {
                Id = Guid.NewGuid(),
                AttemptCount = 20,
                Attempted = true,
                Closed = true,
                Paid = true,
                NextPaymentAttempt = DateTime.UtcNow.AddDays(20),
                StripeCustomerId = "cus_Ci2LHKWjPi70QX",
                StripeId = "in_1CJ2Y6KnOAUQyqMUBlvsHio5",
                SaasUserId = saasUserId
            };
            return request;
        }

        public static Subscription UpdateSubscriptionRequest(string saasUserId, Guid userId, Guid subscriptionPlanId)
        {
            var request = new Subscription
            {
                CancelReason = "test",
                End = DateTime.UtcNow.AddDays(2),
                Start = DateTime.UtcNow.AddDays(-4),
                Status = "active",
                StripeId = "test",
                SubscriptionPlanId = subscriptionPlanId,
                TaxPercent = 20,
                TrialEnd = DateTime.UtcNow.AddHours(3),
                TrialStart = DateTime.UtcNow.AddHours(-6),
                UserId = userId,
                SaasUserId = saasUserId
            };
            return request;
        }

        public static SubscriptionPlan CreateSubscriptionPlanRequest()
        {
            var request = new SubscriptionPlan
            {
                Id = Guid.NewGuid(),
                Currency = "USD",
                Interval = SubscriptionInterval.Weekly,
                Name = "test",
                Price = 1000,
                Status = SubscriptionPlanStatus.Enabled,
                StripeId = "test",
                TrialPeriodInDays = 50
            };
            return request;
        }

        public static SubscriptionPlan UpdateSubscriptionPlanRequest()
        {
            var request = new SubscriptionPlan
            {
                Name = "test2"
            };
            return request;
        }
    }
}