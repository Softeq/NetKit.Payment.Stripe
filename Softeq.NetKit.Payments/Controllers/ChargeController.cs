// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Charge.Request;
using Softeq.NetKit.Payments.Service.TransportModels.Charge.Response;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.Serilog.Extension;

namespace Softeq.NetKit.Payments.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ErrorDto>), 400)]
    [ProducesResponseType(typeof(ErrorDto), 400)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [ApiVersion("1.0")]
    [Route("api/charge")]
    [Authorize(Roles = "User")]
    public class ChargeController : BaseApiController
    {
        private readonly IChargeService _chargeService;

        public ChargeController(IChargeService chargeService, ILogger logger) : base(logger)
        {
            _chargeService = chargeService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ChargeResponse), 200)]
        public async Task<IActionResult> CreateCharge([FromBody] CreateChargeRequest request)
        {
            request.UserId = GetCurrentUserId();
            Logger.Event("CreateCharge").With.Message("UserId: {userId}", request.UserId).AsInformation();
            var charge = await _chargeService.CreateChargeAsync(request);
            return Ok(charge);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ChargeResponse>), 200)]
        public async Task<IActionResult> GetUserChargesAsync()
        {
            var userId = GetCurrentUserId();
            var charge = await _chargeService.GetAllUserCharges(userId);
            return Ok(charge);
        }
    }
}