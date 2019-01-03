// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.TransportModels.Customer.Request;

namespace Softeq.NetKit.Payments.Service.Services.Abstract
{
    public interface ICustomerService
    {
        Task<object> GetCustomerEphemeralKeyAsync(CreateEphemeralKeyRequest request);
        Task CreateCustomerAsync(string userId, string email);
    }
}