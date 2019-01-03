// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Softeq.NetKit.Payments.Data.Models.User;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.TransportModels.User.Request;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.Service.DataServices.Storage
{
    public class UserDataService : BaseService, IUserDataService
    {
        public UserDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <inheritdoc />
        public async Task<User> GetAsync(string userId)
        {
            var user = await UnitOfWork.UserRepository.Query(x => x.SaasUserId == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "User does not exist."));
            }

            return user;
        }

        public async Task CreateAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Delinquent = request.Delinquent,
                SaasUserId = request.UserId,
                StripeCustomerId = request.StripeCustomerId
            };
            UnitOfWork.UserRepository.Add(user);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateUserRequest request)
        {
            var user = await GetAsync(request.UserId);
            user.StripeCustomerId = request.StripeCustomerId;
            UnitOfWork.UserRepository.Update(user);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DoesExistAsync(string userId)
        {
            var user = await UnitOfWork.UserRepository.Query(x => x.SaasUserId == userId)
                .FirstOrDefaultAsync();
            return user != null;
        }
    }
}