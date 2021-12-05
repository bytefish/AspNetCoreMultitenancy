// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using AspNetCoreMultitenancy.Database.Mappings;
using AspNetCoreMultitenancy.Models;
using AspNetCoreMultitenancy.Multitenancy;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreMultitenancy.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// Customers.
        /// </summary>
        public DbSet<Customer> Customers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
