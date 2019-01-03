// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.Subscription;

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface ISubscriptionRepository : IRepository<Subscription, Guid>
    {
    }
}