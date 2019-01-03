// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Threading.Tasks;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;
using Softeq.NetKit.Payments.SQLRepository.Repository;

namespace Softeq.NetKit.Payments.SQLRepository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected ApplicationDbContext DbContext;

        public UnitOfWork(ApplicationDbContext context)
        {
            DbContext = context;
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync(true);
        }

        public void Dispose()
        {
            DbContext?.Dispose();
        }

        #region Repositories

        private IUserRepository _userRepository;
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(DbContext));

        private ICardRepository _cardRepository;
        public ICardRepository CardRepository => _cardRepository ?? (_cardRepository = new CardRepository(DbContext));

        private IInvoiceRepository _invoiceRepository;
        public IInvoiceRepository InvoiceRepository => _invoiceRepository ?? (_invoiceRepository = new InvoiceRepository(DbContext));

        private ISubscriptionRepository _subscriptionRepository;
        public ISubscriptionRepository SubscriptionRepository => _subscriptionRepository ?? (_subscriptionRepository = new SubscriptionRepository(DbContext));

        private ISubscriptionPlanRepository _subscriptionPlanRepository;
        public ISubscriptionPlanRepository SubscriptionPlanRepository => _subscriptionPlanRepository ?? (_subscriptionPlanRepository = new SubscriptionPlanRepository(DbContext));

        private IChargeRepository _chargeRepository;
        public IChargeRepository ChargeRepository => _chargeRepository ?? (_chargeRepository = new ChargeRepository(DbContext));

        #endregion
    }
}