// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Subscription.Request;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.Serilog.Extension;

namespace Softeq.NetKit.Payments.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ErrorDto>), 400)]
    [ProducesResponseType(typeof(ErrorDto), 400)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [ApiVersion("1.0")]
    [Route("api/subscription")]
    [Authorize(Roles = "User")]
    public class SubscriptionController : BaseApiController
    {
        private readonly ISubscriptionsService _subscriptionsService;

        public SubscriptionController(ISubscriptionsService subscriptionsService, ILogger logger) : base(logger)
        {
            _subscriptionsService = subscriptionsService;
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), 200)]
        [Route("{subscriptionId}")]
        public async Task<IActionResult> UpdateSubscriptionAsync(string subscriptionId, [FromBody] UpdateSubscriptionRequest request)
        {
            var userId = GetCurrentUserId();
            await _subscriptionsService.UpdateSubscriptionAsync(new UpdateSubscriptionRequest(subscriptionId, userId, request.PlanId, request.Prorate));
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> SubscribeUserAsync([FromBody] CreateSubscriptionRequest request)
        {
            var userId = GetCurrentUserId();
            var email = GetCurrentUserEmail();
            Logger.Event("CreateSubscription").With.Message("UserId: {userId}", userId).AsInformation();
            await _subscriptionsService.SubscribeUserAsync(new CreateSubscriptionRequest(userId, email, request.PlanId, request.CardId, request.TaxPercent));
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        [Route("billing")]
        public async Task<IActionResult> SubscribeUserWithBillingCycleAsync([FromBody] CreateSubscriptionRequest request)
        {
            var userId = GetCurrentUserId();
            var email = GetCurrentUserEmail();
            Logger.Event("CreateSubscription").With.Message("UserId: {userId}", userId).AsInformation();
            await _subscriptionsService.SubscribeUserWithBillingCycleAsync(new CreateSubscriptionRequest(userId, email, request.PlanId, request.CardId, request.TaxPercent));
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [Route("/api/user/subscription/{subscriptionId}")]
        public async Task<IActionResult> DeleteSubscriptionsAsync(string subscriptionId, [FromBody] EndSubscriptionRequest request)
        {
            var userId = GetCurrentUserId();
            Logger.Event("DeleteSubscription").With.Message("UserId: {userId}", userId).AsInformation();
            var res = await _subscriptionsService.DeleteSubscriptionAsync(new EndSubscriptionRequest(subscriptionId, userId, request.CancelAtPeriodEnd));
            return Ok(res);
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [Route("/api/user/subscription")]
        public async Task<IActionResult> DeleteSubscriptionsAsync([FromBody] EndSubscriptionsRequest request)
        {
            var userId = GetCurrentUserId();
            Logger.Event("DeleteSubscriptions").With.Message("UserId: {userId}", userId).AsInformation();
            await _subscriptionsService.DeleteSubscriptionsAsync(new EndSubscriptionsRequest(userId, request.CustomerId, request.CancelAtPeriodEnd));
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Subscription>), 200)]
        [Route("/api/user/subscription/active")]
        public async Task<IActionResult> GetActiveUserSubscriptionsAsync()
        {
            var userId = GetCurrentUserId();
            var res = await _subscriptionsService.UserActiveSubscriptionsAsync(userId);
            return Ok(res);
        }

        [HttpGet]
        [ProducesResponseType(typeof(int), 200)]
        [Route("/api/user/subscription/{subscriptionId}/timeline")]
        public async Task<IActionResult> GetSubscriptionDaysTrialLeft(string subscriptionId)
        {
            var userId = GetCurrentUserId();
            var res = await _subscriptionsService.GetDaysTrialLeftAsync(userId, subscriptionId);
            return Ok(res);
        }
    }
}