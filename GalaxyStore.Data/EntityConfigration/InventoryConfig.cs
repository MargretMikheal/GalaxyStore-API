using GalaxyStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GalaxyStore.Data.EntityConfigration
{
    internal class InventoryConfig : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasOne(i => i.Warehouse)
                   .WithMany(w => w.Inventories)
                   .HasForeignKey(i => i.WarehouseId);

            builder.HasOne(i => i.Product)
                   .WithMany(p => p.Inventories)
                   .HasForeignKey(i => i.ProductId);
        }
    }
}
