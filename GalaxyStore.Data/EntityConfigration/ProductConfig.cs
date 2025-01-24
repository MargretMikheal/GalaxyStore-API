using GalaxyStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyStore.Data.EntityConfigration
{
    internal class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasMany(p => p.Inventories)
                   .WithOne(i => i.Product)
                   .HasForeignKey(i => i.ProductId);

            builder.HasMany(p => p.InvoiceItems)
                   .WithOne(ii => ii.Product)
                   .HasForeignKey(ii => ii.ProductId);

            builder.HasMany(p => p.Items)
                   .WithOne(i => i.Product)
                   .HasForeignKey(i => i.ProductId);

            builder.HasIndex(p => p.Serial)
                   .IsUnique();
        }
    }
}
