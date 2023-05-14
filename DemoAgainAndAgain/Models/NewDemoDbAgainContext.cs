using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DemoAgainAndAgain;

public partial class NewDemoDbAgainContext : DbContext
{
    public NewDemoDbAgainContext()
    {
    }

    public NewDemoDbAgainContext(DbContextOptions<NewDemoDbAgainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientService> ClientServices { get; set; }

    public virtual DbSet<DivisionCode> DivisionCodes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeCategory> EmployeeCategories { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAdditionalPhoto> ProductAdditionalPhotos { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceAdditionalPhoto> ServiceAdditionalPhotos { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-U9I41QJ\\SQLEXPRESS; Initial Catalog=NewDemoDbAgain; Trusted_Connection = true; MultipleActiveResultSets = true; TrustServerCertificate = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client");

            entity.Property(e => e.Birthday).HasColumnType("date");
            entity.Property(e => e.RegistrationDate).HasColumnType("date");

            entity.HasOne(d => d.Tag).WithMany(p => p.Clients)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_Client_Tag");
        });

        modelBuilder.Entity<ClientService>(entity =>
        {
            entity.ToTable("ClientService");

            entity.Property(e => e.Start).HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientServices)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_ClientService_Client");

            entity.HasOne(d => d.Service).WithMany(p => p.ClientServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ClientService_Service");
        });

        modelBuilder.Entity<DivisionCode>(entity =>
        {
            entity.ToTable("DivisionCode");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Birthday).HasColumnType("date");

            entity.HasOne(d => d.DivisionCode).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DivisionCodeId)
                .HasConstraintName("FK_Employee_DivisionCode");

            entity.HasOne(d => d.EmployeeCategory).WithMany(p => p.Employees)
                .HasForeignKey(d => d.EmployeeCategoryId)
                .HasConstraintName("FK_Employee_EmployeeCategory");
        });

        modelBuilder.Entity<EmployeeCategory>(entity =>
        {
            entity.ToTable("EmployeeCategory");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.ToTable("Manufacturer");

            entity.Property(e => e.StartupDate).HasColumnType("date");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.ManufacturerId)
                .HasConstraintName("FK_Product_Manufacturer");

            entity.HasOne(d => d.ProductCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategoryId)
                .HasConstraintName("FK_Product_ProductCategory");
        });

        modelBuilder.Entity<ProductAdditionalPhoto>(entity =>
        {
            entity.HasKey(e => e.ProductAdditionalPhoto1);

            entity.ToTable("ProductAdditionalPhoto");

            entity.Property(e => e.ProductAdditionalPhoto1).HasColumnName("ProductAdditionalPhoto");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductAdditionalPhotos)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductAdditionalPhoto_Product");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("ProductCategory");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.ToTable("Service");
        });

        modelBuilder.Entity<ServiceAdditionalPhoto>(entity =>
        {
            entity.ToTable("ServiceAdditionalPhoto");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceAdditionalPhotos)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK_ServiceAdditionalPhoto_Service");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
