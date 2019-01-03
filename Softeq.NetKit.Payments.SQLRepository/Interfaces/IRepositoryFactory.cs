// Developed by Softeq Development Corporation
// http://www.softeq.com

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface IRepositoryFactory
    {
        IUserRepository UserRepository { get; }
        ICardRepository CardRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        ISubscriptionRepository SubscriptionRepository { get; }
        ISubscriptionPlanRepository SubscriptionPlanRepository { get; }
        IChargeRepository ChargeRepository { get; }
    }
}