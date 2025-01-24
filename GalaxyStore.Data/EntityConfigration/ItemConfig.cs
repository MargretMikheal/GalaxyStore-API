using GalaxyStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace GalaxyStore.Data.EntityConfigration
{
    internal class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasOne(i => i.Product)
                   .WithMany(p => p.Items)
                   .HasForeignKey(i => i.ProductId);

            builder.HasOne(i => i.Supplier)
                   .WithMany()
                   .HasForeignKey(i => i.SupplierId);
            builder.HasIndex(i => i.Barcode).IsUnique();
        }
    }
}
