// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Invoice.Request;
using Softeq.NetKit.Payments.Service.Utility;

namespace Softeq.NetKit.Payments.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/webhook/invoice")]
    public class StripeWebHookController : BaseApiController
    {
        private readonly IInvoiceService _invoiceService;

        public StripeWebHookController(ILogger logger, IInvoiceService invoiceService) : base(logger)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> CreateInvoiceAsync()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var invoice = JsonConvert.DeserializeObject<CreateInvoiceRequest>(json, new InvoiceConverter());
            var res = await _invoiceService.CreateInvoiceAsync(invoice);
            return Ok(res);
        }
    }
}