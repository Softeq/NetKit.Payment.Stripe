// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Softeq.NetKit.Payments.Service.Test.Dependency
{
    public class StartupModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            builder.RegisterInstance(configurationRoot)
                .As<IConfigurationRoot>()
                .As<IConfiguration>();

            builder.RegisterType<LoggerFactory>()
                .As<ILoggerFactory>();

            builder.Register(context =>
                {
                    var temp = context.Resolve<IComponentContext>();
                    return new AutofacServiceProvider(temp);
                })
                .As<IServiceProvider>()
                .SingleInstance();
        }
    }
}