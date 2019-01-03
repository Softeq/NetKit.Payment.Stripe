// Developed by Softeq Development Corporation
// http://www.softeq.com

using Autofac;
using CorrelationId;

namespace Softeq.NetKit.Payments.DI
{
    public class Startup : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CorrelationContextAccessor>().As<ICorrelationContextAccessor>().SingleInstance();
            builder.RegisterType<CorrelationContextFactory>().As<ICorrelationContextFactory>().InstancePerDependency();
        }
    }
}