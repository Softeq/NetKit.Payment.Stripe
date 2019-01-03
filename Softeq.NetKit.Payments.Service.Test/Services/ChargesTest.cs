// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Linq;
using Autofac;
using Softeq.NetKit.Payments.Service.Test.Abstract;
using Softeq.NetKit.Payments.Service.Test.Data;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Xunit;

namespace Softeq.NetKit.Payments.Service.Test.Services
{
    public class ChargesTest : BaseTest
    {
        private const string SaasUserId = "07c3b9dc-1813-410f-89b0-c12cde00de66";

        private readonly Guid _userId = new Guid("11A07ADA-C3E6-4E6C-935C-7A9207D5A8B3");
        private readonly Guid _creditCardId = new Guid("8AB4C732-C838-4FA5-97EF-A58D3B9C6E77");

        private readonly IChargeDataService _chargeDataService;

        public ChargesTest()
        {
            _chargeDataService = LifetimeScope.Resolve<IChargeDataService>();
        }

        [Fact]
        public async void GetAllUserChargesAsyncTest()
        {
            var charges = await _chargeDataService.GetAllUserCharges(SaasUserId);
            Assert.NotNull(charges);
        }

        [Fact]
        public async void AddChargeAsyncTest()
        {
            var charge = TestData.CreateChargeRequest(SaasUserId, _creditCardId, _userId);
            await _chargeDataService.AddAsync(charge);
            var newCharge = (await _chargeDataService.GetAllUserCharges(SaasUserId))
                .FirstOrDefault();
            Assert.NotNull(newCharge);
            Assert.Equal(charge.UserId, newCharge.UserId);
            Assert.Equal(charge.Description, newCharge.Description);
            Assert.Equal(charge.StripeId, newCharge.StripeId);
            Assert.Equal(charge.Amount, newCharge.Amount);
            Assert.Equal(charge.Currency, newCharge.Currency);
            Assert.Equal(charge.CreditCardId, newCharge.CreditCardId);
        }
    }
}