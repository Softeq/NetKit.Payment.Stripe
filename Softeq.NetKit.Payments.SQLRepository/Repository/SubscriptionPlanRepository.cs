// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.SQLRepository.Repository
{
    public class SubscriptionPlanRepository : RepositoryBase<SubscriptionPlan, Guid>, ISubscriptionPlanRepository
    {
        public SubscriptionPlanRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}