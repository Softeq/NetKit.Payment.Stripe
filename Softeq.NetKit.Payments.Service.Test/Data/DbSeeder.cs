// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Softeq.NetKit.Payments.Data.Models.Card;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Data.Models.User;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.Service.Test.Data
{
    public class DbSeeder
    {
        private readonly IUnitOfWork _unitOfWork;

        public DbSeeder(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Seed()
        {
            using (StreamReader reader = File.OpenText("SeedValues.json"))
            {
                var values = JToken.Parse(reader.ReadToEnd());
                await SeedUsers(values["Users"]);
                await SeedCreditCards(values["CreditCards"]);
                await SeedSubscriptionPlans(values["SubscriptionPlans"]);
            }
        }

        public async Task SeedCreditCards(JToken values)
        {
            foreach (var data in values)
            {
                var creditCard = (await _unitOfWork.CardRepository.Query(x => x.StripeId == data["StripeId"].Value<string>()).ToListAsync())
                    .FirstOrDefault();
                if (creditCard == null)
                {
                    _unitOfWork.CardRepository.Add(new CreditCard
                    {
                        Id = new Guid(data["Id"].Value<string>()),
                        CardCountry = data["CardCountry"].Value<string>(),
                        ExpirationYear = data["ExpirationYear"].Value<int>(),
                        ExpirationMonth = data["ExpirationMonth"].Value<int>(),
                        Fingerprint = data["Fingerprint"].Value<string>(),
                        Last4 = data["Last4"].Value<string>(),
                        SaasUserId = data["SaasUserId"].Value<string>(),
                        StripeCustomerId = data["StripeCustomerId"].Value<string>(),
                        StripeId = data["StripeId"].Value<string>(),
                        UserId = new Guid(data["UserId"].Value<string>())
                    });
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task SeedUsers(JToken values)
        {
            foreach (var data in values)
            {
                var user = (await _unitOfWork.UserRepository.Query(x => x.StripeCustomerId == data["StripeCustomerId"].Value<string>()).ToListAsync())
                    .FirstOrDefault();
                if (user == null)
                {
                    _unitOfWork.UserRepository.Add(new User
                    {
                        Id = new Guid(data["Id"].Value<string>()),
                        Delinquent = data["Delinquent"].Value<bool>(),
                        StripeCustomerId = data["StripeCustomerId"].Value<string>()
                    });
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task SeedSubscriptionPlans(JToken values)
        {
            foreach (var data in values)
            {
                var plan = (await _unitOfWork.SubscriptionPlanRepository.Query(x => x.StripeId == data["StripeId"].Value<string>()).ToListAsync())
                    .FirstOrDefault();
                if (plan == null)
                {
                    _unitOfWork.SubscriptionPlanRepository.Add(new SubscriptionPlan
                    {
                        Id = new Guid(data["Id"].Value<string>()),
                        StripeId = data["StripeId"].Value<string>(),
                        Name = data["Name"].Value<string>(),
                        Currency = data["Currency"].Value<string>(),
                        Status = data["Status"].ToObject<SubscriptionPlanStatus>(),
                        Interval = data["Interval"].ToObject<SubscriptionInterval>(),
                        Price = data["Price"].Value<double>(),
                        TrialPeriodInDays = data["TrialPeriodInDays"].Value<int>()
                    });
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }
    }
}