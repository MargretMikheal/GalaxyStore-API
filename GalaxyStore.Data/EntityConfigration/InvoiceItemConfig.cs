using GalaxyStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace GalaxyStore.Data.EntityConfigration
{
    internal class InvoiceItemConfig : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasOne(ii => ii.Invoice)
                   .WithMany(i => i.InvoiceItems)
                   .HasForeignKey(ii => ii.InvoiceId);

            builder.HasOne(ii => ii.Product)
                   .WithMany(p => p.InvoiceItems)
                   .HasForeignKey(ii => ii.ProductId);

            builder.HasOne(ii => ii.Warehouse)
                   .WithMany(w => w.InvoiceItems)
                   .HasForeignKey(ii => ii.WarehouseId);
        }
    }
}
