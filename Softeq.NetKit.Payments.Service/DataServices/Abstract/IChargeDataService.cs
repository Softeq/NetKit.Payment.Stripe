// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.Charge;

namespace Softeq.NetKit.Payments.Service.DataServices.Abstract
{
    public interface IChargeDataService
    {
        Task AddAsync(Charge request);

        Task<IEnumerable<Charge>> GetAllUserCharges(string userId);
    }
}