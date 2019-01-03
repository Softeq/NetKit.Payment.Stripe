// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Softeq.NetKit.Payments.Data.Models.Card;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.SQLRepository.Repository
{
    public class CardRepository : RepositoryBase<CreditCard, Guid>, ICardRepository
    {
        public CardRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}