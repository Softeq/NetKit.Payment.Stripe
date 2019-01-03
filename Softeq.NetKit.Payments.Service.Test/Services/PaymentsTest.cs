// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Threading.Tasks;
using Autofac;
using Softeq.NetKit.Payments.Service.Test.Abstract;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe;
using Stripe;
using Xunit;

namespace Softeq.NetKit.Payments.Service.Test.Services
{
    public class PaymentsTest : BaseTest
    {
        private readonly string _stripeApiKey;

        public PaymentsTest()
        {
            _stripeApiKey = LifetimeScope.Resolve<StripeSettings>().ApiKey;
        }

        [Fact]
        public void MakeTestPayment()
        {
            // Set your secret key: remember to change this to your live secret key in production
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.SetApiKey(_stripeApiKey);

            var options = new StripeChargeCreateOptions
            {
                Amount = 999,
                Currency = "usd",
                SourceTokenOrExistingSourceId = "tok_visa",
                ReceiptEmail = "jenny.rosen@example.com"
            };
            var service = new StripeChargeService();
            StripeCharge charge = service.Create(options);
        }

        [Fact]
        public async Task MakePayment()
        {
            try
            {
                StripeConfiguration.SetApiKey(_stripeApiKey);

                var tokenOptions = new StripeTokenCreateOptions
                {
                    Card = new StripeCreditCardOptions
                    {
                        Number = "5555555555554444",
                        ExpirationYear = 2019,
                        ExpirationMonth = 4,
                        Cvc = "123"
                    }
                };
                var tokenService = new StripeTokenService();
                StripeToken stripeToken = tokenService.Create(tokenOptions);


                var customerService = new StripeCustomerService();
                var customer = await customerService.CreateAsync(new StripeCustomerCreateOptions
                {
                    Email = "eugene.kirvel@softeq.com",
                    SourceToken = "tok_mastercard"
                });

                var chargeService = new StripeChargeService();
                StripeCharge charge = await chargeService.CreateAsync(new StripeChargeCreateOptions
                {
                    Amount = 50, //charge in cents
                    Description = "Test Payement Description",
                    Currency = "usd",
                    CustomerId = customer.Id
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            // set this property if using a customer - this MUST be set if you are using an existing source!
            //myCharge.CustomerId = *customerId *;

            // set this if you have your own application fees (you must have your application configured first within Stripe)
            //myCharge.ApplicationFee = 25;

            // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
            //myCharge.Capture = true;
        }
    }
}