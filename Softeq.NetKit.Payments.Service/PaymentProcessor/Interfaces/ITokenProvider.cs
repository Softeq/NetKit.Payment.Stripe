// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Stripe;

namespace Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces
{
    public interface ITokenProvider
    {
        Task<StripeToken> GetTokenAsync(string tokenId);
    }
}