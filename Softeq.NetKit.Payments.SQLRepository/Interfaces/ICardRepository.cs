// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.Card;

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface ICardRepository : IRepository<CreditCard, Guid>
    {
    }
}