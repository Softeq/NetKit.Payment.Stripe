// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces
{
    /// <summary>
    /// Interface for CRUD related to customers with Stripe
    /// </summary>
    public interface ICustomerProvider
    {
        Task<StripeCustomer> CreateCustomerAsync(string email);

        Task<object> CreateEphemeralKeyAsync(string stripeCustomerId, string userId, string email, string stripeVersion);

        Task<IEnumerable<StripeSubscription>> GetUserActiveSubscriptionsAsync(string customerId);
    }
}