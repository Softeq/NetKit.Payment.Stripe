// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Softeq.NetKit.Payments.Data.Abstractions;
using Softeq.NetKit.Payments.Data.Models.BillingAddress;
using Softeq.NetKit.Payments.Data.Models.Card;
using Softeq.NetKit.Payments.Data.Models.Charge;
using Softeq.NetKit.Payments.Data.Models.Invoice;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.Data.Models.User;
using Softeq.NetKit.Payments.SQLRepository.Mappings;

namespace Softeq.NetKit.Payments.SQLRepository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<BillingAddress> BillingAddress { get; set; }
        public DbSet<CreditCard> CreditCard { get; set; }
        public DbSet<Charge> Charge { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItem { get; set; }
        public DbSet<InvoicePeriod> InvoicePeriod { get; set; }
        public DbSet<InvoicePlan> InvoicePlan { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlan { get; set; }
        public DbSet<SubscriptionPlanProperty> SubscriptionPlanProperty { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //put db configuration here
            base.OnModelCreating(builder);

            builder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
        }

        private void AddTimestamps()
        {
            var entitiesAdded = ChangeTracker.Entries().Where(x => x.Entity is ICreated && x.State == EntityState.Added);
            foreach (var entity in entitiesAdded)
            {
                ((ICreated) entity.Entity).Created = DateTime.UtcNow;
            }

            var entitiesModified = ChangeTracker.Entries().Where(x => x.Entity is IUpdated && x.State == EntityState.Modified);
            foreach (var entity in entitiesModified)
            {
                ((IUpdated) entity.Entity).Updated = DateTime.UtcNow;
            }
        }
    }
}