// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using InvoiceApp.Models;

namespace InvoiceApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.TaxRate)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Invoice>()
                .Property(i => i.Discount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InvoiceItem>()
                .Property(ii => ii.UnitPrice)
                .HasPrecision(18, 2);

            // Configure relationships
            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems) // FIXED: Ensure 'Invoice' type has 'InvoiceItems' navigation property
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Customer)
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InvoiceItem>()
                .HasOne(ii => ii.Product)
                .WithMany(static p => p.InvoiceItems)
                .HasForeignKey(ii => ii.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>().ToTable("Invoices");


            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    Name = "Acme Corp",
                    Email = "contact@acme.com",
                    Phone = "555-0100",
                    Address = "123 Business St",
                    City = "New York",
                    Country = "USA"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Tech Solutions Inc",
                    Email = "info@techsolutions.com",
                    Phone = "555-0200",
                    Address = "456 Tech Ave",
                    City = "San Francisco",
                    Country = "USA"
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Web Development",
                    Description = "Custom website development",
                    Price = 500.00m
                },
                new Product
                {
                    Id = 2,
                    Name = "Consulting Hour",
                    Description = "Technical consulting services",
                    Price = 150.00m
                },
                new Product
                {
                    Id = 3,
                    Name = "Maintenance Package",
                    Description = "Monthly maintenance and support",
                    Price = 200.00m
                },
                new Product
                {
                    Id = 4,
                    Name = "SEO Optimization",
                    Description = "Search engine optimization services",
                    Price = 350.00m
                }
            );
        }
    }
}