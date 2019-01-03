// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Request;
using Softeq.NetKit.Payments.Service.TransportModels.SubscriptionPlan.Response;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.Serilog.Extension;

namespace Softeq.NetKit.Payments.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ErrorDto>), 400)]
    [ProducesResponseType(typeof(ErrorDto), 400)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [ApiVersion("1.0")]
    [Route("api/subscription/plan")]
    [Authorize(Roles = "Admin")]
    public class SubscriptionPlanController : BaseApiController
    {
        private readonly ISubscriptionPlansService _subscriptionPlansService;

        public SubscriptionPlanController(ISubscriptionPlansService subscriptionPlansService, ILogger logger) : base(logger)
        {
            _subscriptionPlansService = subscriptionPlansService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionPlanResponse>), 200)]
        [Route("/api/subscription/plans")]
        public async Task<IActionResult> GetAllSubscriptionPlansAsync()
        {
            var res = await _subscriptionPlansService.GetAllAsync();
            return Ok(res);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SubscriptionPlanResponse), 200)]
        [Route("{planId}")]
        public async Task<IActionResult> GetSubscriptionPlanAsync(Guid planId)
        {
            var res = await _subscriptionPlansService.FindAsync(planId);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> AddSubscriptionPlanAsync([FromBody] CreateSubscriptionPlanRequest subscriptionPlan)
        {
            var userId = GetCurrentUserId();
            Logger.Event("CreateSubscriptionPlan").With.Message("UserId: {userId}", userId).AsInformation();
            await _subscriptionPlansService.AddAsync(subscriptionPlan);
            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        [Route("{planId}")]
        public async Task<IActionResult> UpdateSubscriptionPlanAsync(Guid planId, [FromBody] UpdateSubscriptionPlanRequest subscriptionPlan)
        {
            subscriptionPlan.PlanId = planId;
            await _subscriptionPlansService.UpdateAsync(subscriptionPlan);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [Route("{planId}")]
        public async Task<IActionResult> DeleteSubscriptionPlanAsync(Guid planId)
        {
            var userId = GetCurrentUserId();
            Logger.Event("DeleteSubscriptionPlan").With.Message("UserId: {userId}", userId).AsInformation();
            await _subscriptionPlansService.DeleteAsync(planId);
            return Ok();
        }
    }
}