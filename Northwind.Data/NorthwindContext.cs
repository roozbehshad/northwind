using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Northwind.Models.Domain;

namespace Northwind.Data
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database= Northwind;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasDatabaseName("CategoryName");

                entity.Property(nameof(Category.Id)).HasColumnName("CategoryID");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.City)
                    .HasDatabaseName("City");

                entity.HasIndex(e => e.CompanyName)
                    .HasDatabaseName("CompanyName");

                entity.HasIndex(e => e.PostalCode)
                    .HasDatabaseName("PostalCode");

                entity.HasIndex(e => e.Region)
                    .HasDatabaseName("Region");

                entity.Property(e => e.Code).IsFixedLength();

                entity.Ignore(nameof(Customer.Id));
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.LastName)
                    .HasDatabaseName("LastName");

                entity.HasIndex(e => e.PostalCode)
                    .HasDatabaseName("PostalCode");

                entity.HasOne(d => d.ReportsTo)
                    .WithMany(p => p.DirectReports)
                    .HasForeignKey(d => d.ReportsToId)
                    .HasConstraintName("FK_Employees_Employees");

                entity.Property(nameof(Employee.Id)).HasColumnName("EmployeeID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasDatabaseName("CustomersOrders");

                entity.HasIndex(e => e.EmployeeId)
                    .HasDatabaseName("EmployeesOrders");

                entity.HasIndex(e => e.OrderDate)
                    .HasDatabaseName("OrderDate");

                entity.HasIndex(e => e.ShipPostalCode)
                    .HasDatabaseName("ShipPostalCode");

                entity.HasIndex(e => e.ShipperId)
                    .HasDatabaseName("ShippersOrders");

                entity.HasIndex(e => e.ShippedDate)
                    .HasDatabaseName("ShippedDate");

                entity.Property(e => e.CustomerId).IsFixedLength();

                entity.Property(e => e.Freight).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.ShipVia)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipperId)
                    .HasConstraintName("FK_Orders_Shippers");

                entity.Property(nameof(Order.Id)).HasColumnName("OrderID");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_Order_Details");

                entity.HasIndex(e => e.OrderId)
                    .HasDatabaseName("OrdersOrder_Details");

                entity.HasIndex(e => e.ProductId)
                    .HasDatabaseName("ProductsOrder_Details");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Products");

                entity.Ignore(nameof(OrderDetail.Id));
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasDatabaseName("CategoryID");

                entity.HasIndex(e => e.Name)
                    .HasDatabaseName("ProductName");

                entity.HasIndex(e => e.SupplierId)
                    .HasDatabaseName("SuppliersProducts");

                entity.Property(e => e.ReorderLevel).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsInStock).HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Products_Suppliers");

                entity.Property(nameof(Product.Id)).HasColumnName("ProductID");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasIndex(e => e.CompanyName)
                    .HasDatabaseName("CompanyName");

                entity.HasIndex(e => e.PostalCode)
                    .HasDatabaseName("PostalCode");

                entity.Property(nameof(Supplier.Id)).HasColumnName("SupplierID");
            });

            modelBuilder.Entity<Shipper>(entity => {
                entity.Property(nameof(Shipper.Id)).HasColumnName("ShipperID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
