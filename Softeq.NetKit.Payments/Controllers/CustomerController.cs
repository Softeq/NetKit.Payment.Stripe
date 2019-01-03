// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Customer.Request;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.Serilog.Extension;

namespace Softeq.NetKit.Payments.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ErrorDto>), 400)]
    [ProducesResponseType(typeof(ErrorDto), 400)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [ApiVersion("1.0")]
    [Route("api/customer")]
    [Authorize(Roles = "User")]
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService, ILogger logger) : base(logger)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Route("key")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> GetCustomerEphemeralKeyAsync([FromQuery] string version)
        {
            var userId = GetCurrentUserId();
            var email = GetCurrentUserEmail();
            var key = await _customerService.GetCustomerEphemeralKeyAsync(new CreateEphemeralKeyRequest(userId, email, version));
            return Ok(key);
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> CreateCustomerAsync()
        {
            var userId = GetCurrentUserId();
            var email = GetCurrentUserEmail();
            Logger.Event("CreateCustomer").With.Message("UserId: {userId}", userId).AsInformation();
            await _customerService.CreateCustomerAsync(userId, email);
            return Ok();
        }
    }
}