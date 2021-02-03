﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20210106151058_NewInit")]
    partial class NewInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ModelLayer.Models.Customer", b =>
                {
                    b.Property<Guid>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CustomerAge")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CustomerBirthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerFName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CustomerLName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CustomerPassword")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CustomerUserName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid?>("PerferedStoreStoreLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool?>("isAdmin")
                        .HasColumnType("bit");

                    b.HasKey("CustomerID");

                    b.HasIndex("PerferedStoreStoreLocationId");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("ModelLayer.Models.Inventory", b =>
                {
                    b.Property<Guid>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("int");

                    b.Property<Guid>("StoreLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InventoryId");

                    b.HasIndex("ProductID");

                    b.HasIndex("StoreLocationId");

                    b.ToTable("inventories");
                });

            modelBuilder.Entity("ModelLayer.Models.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CustomerID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StoreLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("isCart")
                        .HasColumnType("bit");

                    b.Property<bool>("isOrdered")
                        .HasColumnType("bit");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerID");

                    b.HasIndex("StoreLocationId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("ModelLayer.Models.OrderLineDetails", b =>
                {
                    b.Property<Guid>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ItemInventoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("OrderDetailsPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OrderDetailsQuantity")
                        .HasColumnType("int");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderDetailsId");

                    b.HasIndex("ItemInventoryId");

                    b.HasIndex("OrderId");

                    b.ToTable("orderLineDetails");
                });

            modelBuilder.Entity("ModelLayer.Models.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAgeRestricted")
                        .HasColumnType("bit");

                    b.Property<string>("ProductDesc")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<decimal>("ProductPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductID");

                    b.ToTable("products");
                });

            modelBuilder.Entity("ModelLayer.Models.StoreLocation", b =>
                {
                    b.Property<Guid>("StoreLocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StoreLocationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreLocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreLocationId");

                    b.ToTable("locations");
                });

            modelBuilder.Entity("ModelLayer.Models.Customer", b =>
                {
                    b.HasOne("ModelLayer.Models.StoreLocation", "PerferedStore")
                        .WithMany("FrequentCustomers")
                        .HasForeignKey("PerferedStoreStoreLocationId");

                    b.Navigation("PerferedStore");
                });

            modelBuilder.Entity("ModelLayer.Models.Inventory", b =>
                {
                    b.HasOne("ModelLayer.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.HasOne("ModelLayer.Models.StoreLocation", "StoreLocation")
                        .WithMany("Inventory")
                        .HasForeignKey("StoreLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("StoreLocation");
                });

            modelBuilder.Entity("ModelLayer.Models.Order", b =>
                {
                    b.HasOne("ModelLayer.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerID");

                    b.HasOne("ModelLayer.Models.StoreLocation", "Store")
                        .WithMany()
                        .HasForeignKey("StoreLocationId");

                    b.Navigation("Customer");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("ModelLayer.Models.OrderLineDetails", b =>
                {
                    b.HasOne("ModelLayer.Models.Inventory", "Item")
                        .WithMany()
                        .HasForeignKey("ItemInventoryId");

                    b.HasOne("ModelLayer.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");

                    b.Navigation("Item");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ModelLayer.Models.StoreLocation", b =>
                {
                    b.Navigation("FrequentCustomers");

                    b.Navigation("Inventory");
                });
#pragma warning restore 612, 618
        }
    }
}
