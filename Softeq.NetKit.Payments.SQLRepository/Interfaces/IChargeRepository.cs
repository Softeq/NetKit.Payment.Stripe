// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.Charge;

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface IChargeRepository : IRepository<Charge, Guid>
    {
    }
}