// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface ISubscriptionPlanRepository : IRepository<SubscriptionPlan, Guid>
    {
    }
}