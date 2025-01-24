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
    internal class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(i => i.Type)
                   .HasConversion<string>();

            builder.HasOne(i => i.Partner)
                   .WithMany(p => p.Invoices)
                   .HasForeignKey(i => i.PartnerId);
        }
    }
}
