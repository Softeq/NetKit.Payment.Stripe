// Developed by Softeq Development Corporation
// http://www.softeq.com

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softeq.NetKit.Payments.Data.Models.Card;
using Softeq.NetKit.Payments.SQLRepository.Mappings.Abstract;

namespace Softeq.NetKit.Payments.SQLRepository.Mappings
{
    internal class CreditCardMapping : EntityMappingConfiguration<CreditCard>
    {
        public override void Map(EntityTypeBuilder<CreditCard> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.CreditCards)
                .HasForeignKey(card => card.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}