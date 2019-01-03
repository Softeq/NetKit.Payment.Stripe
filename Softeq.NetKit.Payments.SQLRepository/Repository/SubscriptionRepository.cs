// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.SQLRepository.Repository
{
    public class SubscriptionRepository : RepositoryBase<Subscription, Guid>, ISubscriptionRepository
    {
        public SubscriptionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}