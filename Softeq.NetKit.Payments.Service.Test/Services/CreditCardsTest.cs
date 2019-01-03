// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Autofac;
using Softeq.NetKit.Payments.Service.Test.Abstract;
using Softeq.NetKit.Payments.Service.Test.Data;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Xunit;

namespace Softeq.NetKit.Payments.Service.Test.Services
{
    public class CreditCardsTest : BaseTest
    {
        private const string SaasUserId = "d865176a-f08f-4123-8db2-ec2e35f4d4a4";
        private const string StripeCustomerId = "d775176a-f08f-4123-8db2-ec2e35f4d4a4";
        private const string StripeId = "d775176a-e08f-4123-8db2-ec2e35f4d4a4";
        private const string FakeCreditCardId = "d775176a-e11f-4123-8db2-ec2e35f4d4a4";

        private readonly Guid _userId = new Guid("0C8F803D-668E-4D95-AEE2-1A0467F9BA5C");
        private readonly Guid _userId2 = new Guid("11A07ADA-C3E6-4E6C-935C-7A9207D5A8B3");

        private readonly ICardDataService _cardDataService;

        public CreditCardsTest()
        {
            _cardDataService = LifetimeScope.Resolve<ICardDataService>();
        }

        [Fact]
        public async void GetCreditCardByIdAsyncTest()
        {
            var card = TestData.CreateCreditCardRequest(SaasUserId, _userId);
            await _cardDataService.AddAsync(card);
            var newCard = await _cardDataService.FindAsync(SaasUserId, card.StripeId);
            Assert.Equal(card.UserId, newCard.UserId);
            Assert.Equal(card.Name, newCard.Name);
            Assert.Equal(card.StripeId, newCard.StripeId);
            Assert.Equal(card.CardCountry, newCard.CardCountry);
            Assert.Equal(card.ExpirationMonth, newCard.ExpirationMonth);
            Assert.Equal(card.ExpirationYear, newCard.ExpirationYear);
            Assert.Equal(card.Fingerprint, newCard.Fingerprint);
            Assert.Equal(card.Last4, newCard.Last4);
            Assert.Equal(card.SaasUserId, newCard.SaasUserId);
            Assert.Equal(card.StripeCustomerId, newCard.StripeCustomerId);
            Assert.Equal(card.StripeToken, newCard.StripeToken);
            Assert.Equal(card.Type, newCard.Type);
        }

        [Fact]
        public async void AddCreditCardAsyncTest()
        {
            var previousCreditCards = await _cardDataService.GetAllAsync(SaasUserId);
            var testData = TestData.CreateCreditCardRequest(SaasUserId, _userId);
            await _cardDataService.AddAsync(testData);
            await _cardDataService.FindAsync(SaasUserId, testData.StripeId);
            var currentCreditCards = await _cardDataService.GetAllAsync(SaasUserId);
            Assert.True(previousCreditCards.Count < currentCreditCards.Count);
        }

        [Fact]
        public async void DeleteCreditCardAsyncTest()
        {
            var testData = TestData.CreateCreditCardRequest(SaasUserId, _userId);
            await _cardDataService.AddAsync(testData);
            var previousCreditCards = await _cardDataService.GetAllAsync(SaasUserId);
            await _cardDataService.DeleteAsync(SaasUserId, testData);
            var currentCreditCards = await _cardDataService.GetAllAsync(SaasUserId);
            Assert.True(previousCreditCards.Count > currentCreditCards.Count);
            var isExist = true;
            try
            {
                await _cardDataService.FindAsync(SaasUserId, testData.StripeId);
            }
            catch (Exception)
            {
                isExist = false;
            }

            Assert.False(isExist);
        }

        [Fact]
        public async void GetAllCreditCardsAsyncTest()
        {
            var creditCards = await _cardDataService.GetAllAsync(SaasUserId);
            Assert.NotNull(creditCards);
        }

        [Fact]
        public async void UpdateCreditCardAsyncTest()
        {
            var testCreditCard = TestData.CreateCreditCardRequest(SaasUserId, _userId);
            await _cardDataService.AddAsync(testCreditCard);
            var card = await _cardDataService.FindAsync(SaasUserId, testCreditCard.StripeId);
            card.ExpirationMonth = 12;
            card.ExpirationYear = 2030;
            card.Fingerprint = "testtest";
            card.Last4 = "1111";
            card.StripeToken = "testtest";
            card.Type = "testtest";
            card.Name = "testtest";
            card.UserId = _userId2;
            card.StripeCustomerId = StripeCustomerId;
            card.SaasUserId = SaasUserId;
            card.StripeId = StripeId;
            card.CardCountry = "testtest";
            await _cardDataService.UpdateAsync(SaasUserId, card);
            var newCard = await _cardDataService.FindAsync(SaasUserId, StripeId);
            Assert.Equal(newCard.UserId, _userId2);
            Assert.Equal(newCard.Name, "testtest");
            Assert.Equal(newCard.StripeId, StripeId);
            Assert.Equal(newCard.CardCountry, "testtest");
            Assert.Equal(newCard.ExpirationMonth, 12);
            Assert.Equal(newCard.ExpirationYear, 2030);
            Assert.Equal(newCard.Fingerprint, "testtest");
            Assert.Equal(newCard.Last4, "1111");
            Assert.Equal(newCard.SaasUserId, SaasUserId);
            Assert.Equal(newCard.StripeCustomerId, StripeCustomerId);
            Assert.Equal(newCard.StripeToken, "testtest");
            Assert.Equal(newCard.Type, "testtest");
        }

        [Fact]
        public async void CheckIfCreditCardExistAsyncTest()
        {
            var testData = TestData.CreateCreditCardRequest(SaasUserId, _userId);
            await _cardDataService.AddAsync(testData);
            var isExist = await _cardDataService.AnyAsync(testData.StripeId, SaasUserId);
            Assert.True(isExist);
            isExist = await _cardDataService.AnyAsync(FakeCreditCardId, SaasUserId);
            Assert.False(isExist);
        }
    }
}