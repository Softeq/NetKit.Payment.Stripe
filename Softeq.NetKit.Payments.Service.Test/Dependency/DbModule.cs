// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Softeq.NetKit.Payments.Service.Test.Data;
using Softeq.NetKit.Payments.SQLRepository;

namespace Softeq.NetKit.Payments.Service.Test.Dependency
{
    public class DbModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(context =>
                {
                    // Register dbContext to database in memory
                    var dbBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                    var dbContext = new ApplicationDbContext(dbBuilder.Options);
                    return dbContext;
                })
                .As<DbContext>()
                .AsSelf()
                .SingleInstance();

            builder.Register(x => new UnitOfWork(x.Resolve<ApplicationDbContext>()))
                .AsImplementedInterfaces();

            builder.RegisterType<DbSeeder>()
                .AsSelf();
        }
    }
}