// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Softeq.NetKit.Payments.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("api/BaseApi")]
    public class BaseApiController : Controller
    {
        protected readonly ILogger Logger;

        public BaseApiController(ILogger logger)
        {
            Logger = logger;
        }

        #region Helpers

        protected string GetCurrentUserEmail()
        {
            return User.FindFirstValue(JwtClaimTypes.Email);
        }

        protected string GetCurrentUserId()
        {
            return User.FindFirstValue(JwtClaimTypes.Subject);
        }

        #endregion
    }
}