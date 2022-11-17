﻿// <auto-generated />
using System;
using BusinessSolutions.Services.Ordering.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessSolutions.Services.Ordering.API.Migrations
{
    [DbContext(typeof(OrderingContext))]
    partial class OrderingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessSolutions.Services.Ordering.Domain.Entities.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("Items", "ordering");

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            Name = "Green Apples",
                            OrderId = 1,
                            Quantity = 1m,
                            Unit = "Kilogram"
                        },
                        new
                        {
                            ItemId = 2,
                            Name = "Lifejacket",
                            OrderId = 1,
                            Quantity = 2m,
                            Unit = "Item"
                        },
                        new
                        {
                            ItemId = 3,
                            Name = "Soccer Ball",
                            OrderId = 1,
                            Quantity = 2m,
                            Unit = "Item"
                        },
                        new
                        {
                            ItemId = 4,
                            Name = "Chess Board",
                            OrderId = 2,
                            Quantity = 1m,
                            Unit = "Item"
                        },
                        new
                        {
                            ItemId = 5,
                            Name = "Red Fabric",
                            OrderId = 2,
                            Quantity = 5m,
                            Unit = "Meter"
                        },
                        new
                        {
                            ItemId = 6,
                            Name = "GOOD KID, m.A.A.d CITY",
                            OrderId = 3,
                            Quantity = 1m,
                            Unit = "Item"
                        },
                        new
                        {
                            ItemId = 7,
                            Name = "Organic Milk",
                            OrderId = 3,
                            Quantity = 3m,
                            Unit = "Liter"
                        });
                });

            modelBuilder.Entity("BusinessSolutions.Services.Ordering.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsInactive")
                        .HasColumnType("bit");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProviderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("Orders", "ordering");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTimeOffset(new DateTime(2021, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            IsInactive = false,
                            Number = "13eb0c8c-8362-4bcb-b732-7dc4700233e0",
                            ProviderId = 1
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTimeOffset(new DateTime(2017, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            IsInactive = false,
                            Number = "12963b79-3207-4ed9-b353-7c66fe3f3ae9",
                            ProviderId = 2
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTimeOffset(new DateTime(2017, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            IsInactive = false,
                            Number = "0e360e54-5b27-40bc-bc91-f10354357952",
                            ProviderId = 3
                        });
                });

            modelBuilder.Entity("BusinessSolutions.Services.Ordering.Domain.Entities.Provider", b =>
                {
                    b.Property<int>("ProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProviderId"));

                    b.Property<string>("ProviderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProviderId");

                    b.ToTable("Providers", "ordering");

                    b.HasData(
                        new
                        {
                            ProviderId = 1,
                            ProviderName = "Considine-Bauch"
                        },
                        new
                        {
                            ProviderId = 2,
                            ProviderName = "Herzog PLC"
                        },
                        new
                        {
                            ProviderId = 3,
                            ProviderName = "Steuber, Considine and Hermann"
                        },
                        new
                        {
                            ProviderId = 4,
                            ProviderName = "Klocko Group"
                        },
                        new
                        {
                            ProviderId = 5,
                            ProviderName = "Stracke Group"
                        });
                });

            modelBuilder.Entity("BusinessSolutions.Services.Ordering.Domain.Entities.Item", b =>
                {
                    b.HasOne("BusinessSolutions.Services.Ordering.Domain.Entities.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessSolutions.Services.Ordering.Domain.Entities.Order", b =>
                {
                    b.HasOne("BusinessSolutions.Services.Ordering.Domain.Entities.Provider", "Provider")
                        .WithMany("Orders")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("BusinessSolutions.Services.Ordering.Domain.Entities.Order", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("BusinessSolutions.Services.Ordering.Domain.Entities.Provider", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
