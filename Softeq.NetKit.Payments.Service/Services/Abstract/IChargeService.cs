// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Service.TransportModels.Charge.Request;
using Softeq.NetKit.Payments.Service.TransportModels.Charge.Response;

namespace Softeq.NetKit.Payments.Service.Services.Abstract
{
    public interface IChargeService
    {
        Task<ChargeResponse> CreateChargeAsync(CreateChargeRequest request);
        Task<IEnumerable<ChargeDetailsResponse>> GetAllUserCharges(string userId);
    }
}