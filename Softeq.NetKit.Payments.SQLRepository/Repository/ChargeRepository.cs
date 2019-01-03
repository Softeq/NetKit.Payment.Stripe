// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.Charge;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.SQLRepository.Repository
{
    public class ChargeRepository : RepositoryBase<Charge, Guid>, IChargeRepository
    {
        public ChargeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}