// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Softeq.NetKit.Payments.Service.TransportModels.Invoice.Request;

namespace Softeq.NetKit.Payments.Service.Utility
{
    public class InvoiceConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject data = JObject.Load(reader);
            var startDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var model = new CreateInvoiceRequest
            {
                StripeId = data.SelectToken("data.object.id").Value<string>(),
                AmountDue = data.SelectToken("data.object.amount_due").Value<int>(),
                AttemptCount = data.SelectToken("data.object.attempt_count").Value<int>(),
                Attempted = data.SelectToken("data.object.attempted").Value<bool>(),
                Closed = data.SelectToken("data.object.closed").Value<bool>(),
                Currency = data.SelectToken("data.object.currency").Value<string>(),
                StripeCustomerId = data.SelectToken("data.object.customer").Value<string>(),
                Created = startDateTime.AddSeconds(data.SelectToken("data.object.date").Value<double>()),
                EndingBalance = data.SelectToken("data.object.ending_balance").Value<int>(),
                Forgiven = data.SelectToken("data.object.forgiven").Value<bool>(),
                Paid = data.SelectToken("data.object.paid").Value<bool>(),
                PeriodEnd = startDateTime.AddSeconds(data.SelectToken("data.object.period_end").Value<double>()),
                PeriodStart = startDateTime.AddSeconds(data.SelectToken("data.object.period_start").Value<double>()),
                StartingBalance = data.SelectToken("data.object.starting_balance").Value<int>(),
                Subtotal = data.SelectToken("data.object.subtotal").Value<int>(),
                Tax = data.SelectToken("data.object.tax").Value<int?>(),
                TaxPercent = data.SelectToken("data.object.tax_percent").Value<int?>(),
                Total = data.SelectToken("data.object.total").Value<int>()
            };
            return model;
        }

        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CreateInvoiceRequest);
        }
    }
}