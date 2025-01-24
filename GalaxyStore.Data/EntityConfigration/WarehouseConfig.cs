using GalaxyStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace GalaxyStore.Data.EntityConfigration
{
    internal class WarehouseConfig : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasMany(w => w.Inventories)
                   .WithOne(i => i.Warehouse)
                   .HasForeignKey(i => i.WarehouseId);

            builder.HasMany(w => w.InvoiceItems)
                   .WithOne(ii => ii.Warehouse)
                   .HasForeignKey(ii => ii.WarehouseId);
        }
    }
}
