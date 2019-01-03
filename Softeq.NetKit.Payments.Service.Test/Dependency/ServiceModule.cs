// Developed by Softeq Development Corporation
// http://www.softeq.com

using Autofac;
using Softeq.NetKit.Payments.Service.DataServices.Abstract;
using Softeq.NetKit.Payments.Service.DataServices.Storage;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Interfaces;
using Softeq.NetKit.Payments.Service.PaymentProcessor.Stripe;
using Softeq.NetKit.Payments.Service.Services;
using Softeq.NetKit.Payments.Service.Services.Abstract;

namespace Softeq.NetKit.Payments.Service.Test.Dependency
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Data services
            builder.RegisterType<CardDataService>()
                .As<ICardDataService>();

            builder.RegisterType<InvoiceDataService>()
                .As<IInvoiceDataService>();

            builder.RegisterType<SubscriptionDataService>()
                .As<ISubscriptionDataService>();

            builder.RegisterType<SubscriptionPlanDataService>()
                .As<ISubscriptionPlanDataService>();

            builder.RegisterType<UserDataService>()
                .As<IUserDataService>();

            builder.RegisterType<ChargeDataService>()
                .As<IChargeDataService>();

            // Payment providers
            builder.RegisterType<StripeSettings>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<CardProvider>()
                .As<ICardProvider>();

            builder.RegisterType<ChargeProvider>()
                .As<IChargeProvider>();

            builder.RegisterType<CustomerProvider>()
                .As<ICustomerProvider>();

            builder.RegisterType<SubscriptionPlanProvider>()
                .As<ISubscriptionPlanProvider>();

            builder.RegisterType<SubscriptionProvider>()
                .As<ISubscriptionProvider>();

            builder.RegisterType<TokenProvider>()
                .As<ITokenProvider>();

            // Services
            builder.RegisterType<SubscriptionsService>()
                .As<ISubscriptionsService>();

            builder.RegisterType<SubscriptionPlansService>()
                .As<ISubscriptionPlansService>();

            builder.RegisterType<CustomerService>()
                .As<ICustomerService>();

            builder.RegisterType<CardService>()
                .As<ICardService>();

            builder.RegisterType<ChargeService>()
                .As<IChargeService>();

            builder.RegisterType<InvoiceService>()
                .As<IInvoiceService>();
        }
    }
}