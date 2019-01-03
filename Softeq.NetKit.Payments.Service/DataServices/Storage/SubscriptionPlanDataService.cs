// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.Exceptions;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.Service.DataServices.Storage
{
    public class SubscriptionPlanDataService : BaseService, ISubscriptionPlanDataService
    {
        public SubscriptionPlanDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
        /// <inheritdoc />
        public async Task<List<SubscriptionPlan>> GetAllAsync()
        {
            var plans = await UnitOfWork.SubscriptionPlanRepository.GetAll()
                .ToListAsync();
            return plans;
        }

        /// <inheritdoc />
        public async Task<SubscriptionPlan> FindAsync(Guid planId)
        {
            var plan = await UnitOfWork.SubscriptionPlanRepository.Query(x => x.Id == planId)
                .Include(x => x.Properties)
                .FirstOrDefaultAsync();
            if (plan == null)
            {
                throw new NotFoundException(new ErrorDto(ErrorCode.NotFound, "Subscription plan does not exist."));
            }

            return plan;
        }

        /// <inheritdoc />
        public async Task AddAsync(SubscriptionPlan plan)
        {
            UnitOfWork.SubscriptionPlanRepository.Add(plan);
            await UnitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task UpdateAsync(SubscriptionPlan plan)
        {
            // By definition only the plan name can be updated
            UnitOfWork.SubscriptionPlanRepository.Update(plan);
            await UnitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<int> DeleteAsync(Guid planId)
        {
            var plan = await FindAsync(planId);
            UnitOfWork.SubscriptionPlanRepository.Delete(plan);
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<int> DisableAsync(Guid id)
        {
            var plan = await FindAsync(id);
            plan.Status = SubscriptionPlanStatus.Disabled;
            UnitOfWork.SubscriptionPlanRepository.Update(plan);
            return await UnitOfWork.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<int> CountUsersAsync(Guid planId)
        {
            var count = await UnitOfWork.SubscriptionRepository
                .Query(x => (x.End == null || x.End > DateTime.UtcNow) &&
                            x.SubscriptionPlanId == planId)
                .CountAsync();
            return count;
        }
    }
}