// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Softeq.NetKit.Payments.Service.Services.Abstract;
using Softeq.NetKit.Payments.Service.TransportModels.Card.Response;
using Softeq.NetKit.Payments.Service.Utility.ErrorHandling;
using Softeq.Serilog.Extension;

namespace Softeq.NetKit.Payments.Controllers
{
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<ErrorDto>), 400)]
    [ProducesResponseType(typeof(ErrorDto), 400)]
    [ProducesResponseType(typeof(ErrorDto), 500)]
    [ApiVersion("1.0")]
    [Route("api/card")]
    [Authorize(Roles = "User")]
    public class CardController : BaseApiController
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService, ILogger logger) : base(logger)
        {
            _cardService = cardService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CardResponse), 200)]
        [Route("{cardId}")]
        public async Task<IActionResult> GetCreditCardByIdAsync(string cardId)
        {
            var userId = GetCurrentUserId();
            var res = await _cardService.GetCreditCardByIdAsync(userId, cardId);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 200)]
        [Route("{sourceTokenId}")]
        public async Task<IActionResult> AddCreditCardAsync(string sourceTokenId)
        {
            var userId = GetCurrentUserId();
            Logger.Event("CreateCreditCard").With.Message("UserId: {userId}", userId).AsInformation();
            await _cardService.AddCreditCardToDbAsync(userId, sourceTokenId);
            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), 200)]
        [Route("{cardId}")]
        public async Task<IActionResult> DeleteCreditCardAsync(string cardId)
        {
            var userId = GetCurrentUserId();
            Logger.Event("DeleteCreditCard").With.Message("UserId: {userId}", userId).AsInformation();
            await _cardService.DeleteCreditCardAsync(userId, cardId);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CardResponse>), 200)]
        [Route("")]
        public async Task<IActionResult> GetAllUserCreditCardsAsync()
        {
            var userId = GetCurrentUserId();
            var res = await _cardService.GetCreditCardsAsync(userId);
            return Ok(res);
        }
    }
}