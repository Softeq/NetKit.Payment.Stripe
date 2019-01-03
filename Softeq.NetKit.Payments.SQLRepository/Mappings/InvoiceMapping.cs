// Developed by Softeq Development Corporation
// http://www.softeq.com

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Softeq.NetKit.Payments.Data.Models.Invoice;
using Softeq.NetKit.Payments.SQLRepository.Mappings.Abstract;

namespace Softeq.NetKit.Payments.SQLRepository.Mappings
{
    internal class InvoiceMapping : EntityMappingConfiguration<Invoice>
    {
        public override void Map(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.StripeCustomerId).IsUnique();
        }
    }
}