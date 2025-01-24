using GalaxyStore.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;


namespace GalaxyStore.Data.EntityConfigration
{
    internal class PartnerConfig : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.ToTable("Partners");
            builder.HasDiscriminator<string>("Type")
                   .HasValue<Customer>("Customer")
                   .HasValue<Supplier>("Supplier");

            builder.Property("Type")
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
