// Developed by Softeq Development Corporation
// http://www.softeq.com

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softeq.NetKit.Payments.Data.Models.User;
using Softeq.NetKit.Payments.SQLRepository.Mappings.Abstract;

namespace Softeq.NetKit.Payments.SQLRepository.Mappings
{
    internal class UserMapping : EntityMappingConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(p => p.CreditCards)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}