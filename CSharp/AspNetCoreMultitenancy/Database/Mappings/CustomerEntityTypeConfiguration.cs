// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AspNetCoreMultitenancy.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AspNetCoreMultitenancy.Database.Mappings
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .ToTable("sample", "customer")
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasColumnName("customer_id");

            builder
                .Property(x => x.FirstName)
                .HasColumnName("first_name");

            builder
                .Property(x => x.LastName)
                .HasColumnName("last_name");
        }
    }
}
