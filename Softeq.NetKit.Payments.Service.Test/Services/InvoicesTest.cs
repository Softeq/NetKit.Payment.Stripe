// Developed by Softeq Development Corporation
// http://www.softeq.com

using Autofac;
using Softeq.NetKit.Payments.Service.Test.Abstract;
using Softeq.NetKit.Payments.Service.Test.Data;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Xunit;

namespace Softeq.NetKit.Payments.Service.Test.Services
{
    public class InvoicesTest : BaseTest
    {
        private const string SaasUserId = "d865176a-f08f-4123-8db2-ec2e35f4d4a4";

        private readonly IInvoiceDataService _invoiceDataService;

        public InvoicesTest()
        {
            _invoiceDataService = LifetimeScope.Resolve<IInvoiceDataService>();
        }

        [Fact]
        public async void GetUserInvoicesAsyncTest()
        {
            var invoices = await _invoiceDataService.UserInvoicesAsync(SaasUserId);
            Assert.NotNull(invoices);
        }

        [Fact]
        public async void GetAllInvoicesAsyncTest()
        {
            var invoices = await _invoiceDataService.GetInvoicesAsync();
            Assert.NotNull(invoices);
        }

        [Fact]
        public async void AddOrUpdateInvoiceAsyncTest()
        {
            var userInvoices = await _invoiceDataService.UserInvoicesAsync(SaasUserId);
            Assert.Empty(userInvoices);
            var testInvoice = TestData.CreateInvoiceRequest(SaasUserId);
            await _invoiceDataService.CreateOrUpdateAsync(testInvoice);
            var invoice = await _invoiceDataService.UserInvoiceAsync(SaasUserId, testInvoice.StripeId);
            Assert.Equal(testInvoice.AmountDue, invoice.AmountDue);
            Assert.Equal(testInvoice.AttemptCount, invoice.AttemptCount);
            Assert.Equal(testInvoice.Attempted, invoice.Attempted);
            Assert.Equal(testInvoice.Closed, invoice.Closed);
            Assert.Equal(testInvoice.Currency, invoice.Currency);
            Assert.Equal(testInvoice.EndingBalance, invoice.EndingBalance);
            Assert.Equal(testInvoice.Forgiven, invoice.Forgiven);
            Assert.Equal(testInvoice.Paid, invoice.Paid);
            Assert.Equal(testInvoice.PeriodEnd, invoice.PeriodEnd);
            Assert.Equal(testInvoice.PeriodStart, invoice.PeriodStart);
            Assert.Equal(testInvoice.StartingBalance, invoice.StartingBalance);
            Assert.Equal(testInvoice.StripeCustomerId, invoice.StripeCustomerId);
            Assert.Equal(testInvoice.StripeId, invoice.StripeId);
            Assert.Equal(testInvoice.Subtotal, invoice.Subtotal);
            Assert.Equal(testInvoice.Total, invoice.Total);
            Assert.Equal(testInvoice.SaasUserId, invoice.SaasUserId);
            userInvoices = await _invoiceDataService.UserInvoicesAsync(SaasUserId);
            Assert.NotEmpty(userInvoices);
            Assert.True(userInvoices.Count == 1);
            var updatedInvoice = TestData.UpdateInvoiceRequest(SaasUserId);
            await _invoiceDataService.CreateOrUpdateAsync(updatedInvoice);
            var newInvoice = await _invoiceDataService.UserInvoiceAsync(SaasUserId, updatedInvoice.StripeId);
            Assert.Equal(updatedInvoice.AttemptCount, newInvoice.AttemptCount);
            Assert.Equal(updatedInvoice.Attempted, newInvoice.Attempted);
            Assert.Equal(updatedInvoice.Closed, newInvoice.Closed);
            Assert.Equal(updatedInvoice.Paid, newInvoice.Paid);
            Assert.Equal(updatedInvoice.NextPaymentAttempt, newInvoice.NextPaymentAttempt);
            Assert.Equal(updatedInvoice.StripeId, newInvoice.StripeId);
            Assert.Equal(updatedInvoice.SaasUserId, newInvoice.SaasUserId);
            userInvoices = await _invoiceDataService.UserInvoicesAsync(SaasUserId);
            Assert.True(userInvoices.Count == 1);
        }

        [Fact]
        public async void GetUserInvoiceAsyncTest()
        {
            var testInvoice = TestData.CreateInvoiceRequest(SaasUserId);
            await _invoiceDataService.CreateOrUpdateAsync(testInvoice);
            var invoice = await _invoiceDataService.UserInvoiceAsync(SaasUserId, testInvoice.StripeId);
            Assert.Equal(testInvoice.AmountDue, invoice.AmountDue);
            Assert.Equal(testInvoice.AttemptCount, invoice.AttemptCount);
            Assert.Equal(testInvoice.Attempted, invoice.Attempted);
            Assert.Equal(testInvoice.Closed, invoice.Closed);
            Assert.Equal(testInvoice.Currency, invoice.Currency);
            Assert.Equal(testInvoice.EndingBalance, invoice.EndingBalance);
            Assert.Equal(testInvoice.Forgiven, invoice.Forgiven);
            Assert.Equal(testInvoice.Paid, invoice.Paid);
            Assert.Equal(testInvoice.PeriodEnd, invoice.PeriodEnd);
            Assert.Equal(testInvoice.PeriodStart, invoice.PeriodStart);
            Assert.Equal(testInvoice.StartingBalance, invoice.StartingBalance);
            Assert.Equal(testInvoice.StripeCustomerId, invoice.StripeCustomerId);
            Assert.Equal(testInvoice.StripeId, invoice.StripeId);
            Assert.Equal(testInvoice.Subtotal, invoice.Subtotal);
            Assert.Equal(testInvoice.Total, invoice.Total);
            Assert.Equal(testInvoice.SaasUserId, invoice.SaasUserId);
        }
    }
}