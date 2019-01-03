// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Invoice.Response;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;

namespace Softeq.NetKit.Payments.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ErrorDto>), 400)]
    [ProducesResponseType(typeof(ErrorDto), 400)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [ApiVersion("1.0")]
    [Route("api/invoice")]
    [Authorize(Roles = "User")]
    public class InvoiceController : BaseApiController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(ILogger logger, IInvoiceService invoiceService) : base(logger)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(InvoiceResponse), 200)]
        [Route("{invoiceId}")]
        public async Task<IActionResult> GetInvoiceByIdAsync(string invoiceId)
        {
            var userId = GetCurrentUserId();
            var res = await _invoiceService.GetInvoiceByIdAsync(userId, invoiceId);
            return Ok(res);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InvoiceResponse>), 200)]
        public async Task<IActionResult> GetAllInvoicesAsync()
        {
            var res = await _invoiceService.GetAllInvoicesAsync();
            return Ok(res);
        }

        [HttpGet]
        [Route("/api/user/invoice")]
        [ProducesResponseType(typeof(IEnumerable<InvoiceResponse>), 200)]
        public async Task<IActionResult> GetAllUserInvoicesAsync()
        {
            var userId = GetCurrentUserId();
            var res = await _invoiceService.GetAllUserInvoicesAsync(userId);
            return Ok(res);
        }
    }
}