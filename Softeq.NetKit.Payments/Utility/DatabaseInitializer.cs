// Developed by Softeq Development Corporation
// http://www.softeq.com

using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;

namespace Softeq.NetKit.Payments.Utility
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseInitializer(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Seed()
        {
            using (StreamReader reader = File.OpenText("SeedValues.json"))
            {
                var values = JToken.Parse(reader.ReadToEnd());
                await SeedSubscriptionPlans(values["SubscriptionPlans"]);
            }
        }

        public async Task SeedSubscriptionPlans(JToken values)
        {
            foreach (var data in values)
            {
                var plan = await _unitOfWork.SubscriptionPlanRepository
                    .Query(x => x.StripeId == data["StripeId"].Value<string>())
                    .FirstOrDefaultAsync();

                if (plan == null)
                {
                    _unitOfWork.SubscriptionPlanRepository.Add(new SubscriptionPlan
                    {
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