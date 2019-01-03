// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Softeq.NetKit.Payments.Data.Models.Card;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.Service.DataServices.Storage
{
    public class CardDataService : BaseService, ICardDataService
    {
        public CardDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <inheritdoc />
        public async Task<List<CreditCard>> GetAllAsync(string userId)
        {
            var creditCards = await UnitOfWork.CardRepository
                .Query(x => x.SaasUserId == userId)
                .ToListAsync();
            return creditCards;
        }

        /// <inheritdoc />
        public async Task<CreditCard> FindAsync(string userId, string stripeCardId)
        {
            var creditCard = await UnitOfWork.CardRepository
                .Query(x => x.StripeId == stripeCardId && x.SaasUserId == userId)
                .FirstOrDefaultAsync();
            if (creditCard == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Credit card does not exist."));
            }

            return creditCard;
        }

        /// <inheritdoc />
        public async Task AddAsync(CreditCard creditCard)
        {
            UnitOfWork.CardRepository.Add(creditCard);
            await UnitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(string userId, CreditCard creditCard)
        {
            UnitOfWork.CardRepository.Update(creditCard);
            await UnitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(string userId, CreditCard creditCard)
        {
            UnitOfWork.CardRepository.Delete(creditCard);
            await UnitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<bool> AnyAsync(string cardId, string userId)
        {
            var creditCard = await UnitOfWork.CardRepository.Query(x => x.StripeId == cardId && x.SaasUserId == userId)
                .FirstOrDefaultAsync();
            return creditCard != null;
        }
    }
}