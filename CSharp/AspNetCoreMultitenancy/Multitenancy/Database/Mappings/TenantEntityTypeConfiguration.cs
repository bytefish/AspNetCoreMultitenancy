// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreMultitenancy.Multitenancy
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder
                .ToTable("tenant", "sample")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("tenant_id");

            builder
                .Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();

            builder
                .Property(x => x.Description)
                .HasColumnName("description")
                .IsRequired();
        }
    }
}
