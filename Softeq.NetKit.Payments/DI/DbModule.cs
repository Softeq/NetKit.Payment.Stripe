// Developed by Softeq Development Corporation
// http://www.softeq.com

using Autofac;
using Softeq.NetKit.Payments.SQLRepository;
using Softeq.NetKit.Payments.SQLRepository.Interfaces;
using Softeq.NetKit.Payments.Utility;

namespace Softeq.NetKit.Payments.DI
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>();

            builder.RegisterType<DatabaseInitializer>()
                .As<IDatabaseInitializer>();
        }
    }
}