using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using safonenko.Data;

#nullable disable

namespace safonenko.Migrations;

[DbContext(typeof(WarehouseContext))]
partial class WarehouseContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.5")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("safonenko.Models.Category", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            b.HasKey("Id");

            b.HasIndex("Name")
                .IsUnique();

            b.ToTable("Categories");
        });

        modelBuilder.Entity("safonenko.Models.Product", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

            b.Property<string>("Article")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)");

            b.Property<int>("CategoryId")
                .HasColumnType("int");

            b.Property<decimal>("MinStock")
                .HasColumnType("decimal(18,2)");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar(200)");

            b.Property<string>("Unit")
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("nvarchar(20)");

            b.Property<decimal>("Weight")
                .HasColumnType("decimal(18,2)");

            b.HasKey("Id");

            b.HasIndex("Article")
                .IsUnique();

            b.HasIndex("CategoryId");

            b.ToTable("Products");
        });

        modelBuilder.Entity("safonenko.Models.StockMovement", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

            b.Property<DateTime>("Date")
                .HasColumnType("datetime2");

            b.Property<string>("DocumentBase")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            b.Property<int>("OperationType")
                .HasColumnType("int");

            b.Property<int>("ProductId")
                .HasColumnType("int");

            b.Property<decimal>("Quantity")
                .HasColumnType("decimal(18,2)");

            b.Property<int>("StorageLocationId")
                .HasColumnType("int");

            b.Property<int>("SupplierId")
                .HasColumnType("int");

            b.HasKey("Id");

            b.HasIndex("ProductId");

            b.HasIndex("StorageLocationId");

            b.HasIndex("SupplierId");

            b.ToTable("StockMovements");
        });

        modelBuilder.Entity("safonenko.Models.StorageLocation", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

            b.Property<string>("Cell")
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("nvarchar(10)");

            b.Property<string>("Row")
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("nvarchar(10)");

            b.Property<string>("Shelf")
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnType("nvarchar(10)");

            b.HasKey("Id");

            b.HasIndex("Row", "Shelf", "Cell")
                .IsUnique();

            b.ToTable("StorageLocations");
        });

        modelBuilder.Entity("safonenko.Models.Supplier", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

            b.Property<string>("Details")
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnType("nvarchar(300)");

            b.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar(200)");

            b.HasKey("Id");

            b.ToTable("Suppliers");
        });

        modelBuilder.Entity("safonenko.Models.Product", b =>
        {
            b.HasOne("safonenko.Models.Category", "Category")
                .WithMany("Products")
                .HasForeignKey("CategoryId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.Navigation("Category");
        });

        modelBuilder.Entity("safonenko.Models.StockMovement", b =>
        {
            b.HasOne("safonenko.Models.Product", "Product")
                .WithMany("StockMovements")
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.HasOne("safonenko.Models.StorageLocation", "StorageLocation")
                .WithMany("StockMovements")
                .HasForeignKey("StorageLocationId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.HasOne("safonenko.Models.Supplier", "Supplier")
                .WithMany("StockMovements")
                .HasForeignKey("SupplierId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            b.Navigation("Product");

            b.Navigation("StorageLocation");

            b.Navigation("Supplier");
        });
#pragma warning restore 612, 618
    }
}
