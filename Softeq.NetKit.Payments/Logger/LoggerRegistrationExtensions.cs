// Developed by Softeq Development Corporation
// http://www.softeq.com

using Autofac;
using CorrelationId;
using Serilog;
using Softeq.NetKit.Payments.Utility;

namespace Softeq.NetKit.Payments.Logger
{
    public static class LoggerRegistrationExtensions
    {
        public static void AddLogger(this ContainerBuilder builder)
        {
            builder.Register((c, p) =>
                {
                    var correlationContextAccessor = c.Resolve<ICorrelationContextAccessor>();
                    return Log.Logger.ForContext(new CorrelationIdEnricher(correlationContextAccessor));
                })
                .As<ILogger>().InstancePerLifetimeScope();
        }
    }
}