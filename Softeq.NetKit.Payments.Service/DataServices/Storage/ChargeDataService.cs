// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.Service.DataServices.Storage
{
    public class ChargeDataService : BaseService, IChargeDataService
    {
        public ChargeDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task AddAsync(Data.Models.Charge.Charge charge)
        {
            UnitOfWork.ChargeRepository.Add(charge);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Data.Models.Charge.Charge>> GetAllUserCharges(string userId)
        {
            var charges = await UnitOfWork.ChargeRepository.Query(x => x.SaasUserId == userId)
                .ToListAsync();
            return charges;
        }
    }
}