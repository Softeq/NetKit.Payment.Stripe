using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.SQLRepository.Mappings.Abstract;

namespace Softeq.NetKit.Payments.SQLRepository.Mappings
{
    internal class SubscriptionMapping : EntityMappingConfiguration<Subscription>
    {
        public override void Map(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.Subscriptions)
                .HasForeignKey(card => card.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
