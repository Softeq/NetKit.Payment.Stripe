// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;

namespace Softeq.NetKit.Payments.SQLRepository.Interfaces
{
    public interface IUnitOfWork : IRepositoryFactory
    {
        Task<int> SaveChangesAsync();
    }
}