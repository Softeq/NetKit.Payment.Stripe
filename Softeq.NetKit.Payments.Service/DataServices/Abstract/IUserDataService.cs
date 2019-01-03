// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Threading.Tasks;
using Softeq.NetKit.Payments.Data.Models.User;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.TransportModels.User.Request;

namespace Softeq.NetKit.Payments.Service.DataServices.Abstract
{
    public interface IUserDataService
    {
        /// <exception cref="NotFoundException"></exception>
        Task<User> GetAsync(string userId);
        Task CreateAsync(CreateUserRequest request);
        Task UpdateAsync(UpdateUserRequest request);
        Task<bool> DoesExistAsync(string userId);
    }
}