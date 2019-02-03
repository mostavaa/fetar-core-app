﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("GUID");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<string>("SubName");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Data.ItemDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount");

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("GUID");

                    b.Property<int?>("ItemId");

                    b.Property<string>("Notes");

                    b.Property<int?>("OrderDetailsId");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderDetailsId");

                    b.ToTable("ItemDetails");
                });

            modelBuilder.Entity("Data.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("GUID");

                    b.Property<string>("Name");

                    b.Property<bool>("Ordered");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Data.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("GUID");

                    b.Property<string>("Notes");

                    b.Property<int?>("OrderId");

                    b.Property<bool>("Payed");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("GUID");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.ItemDetails", b =>
                {
                    b.HasOne("Data.Item", "Item")
                        .WithMany("ItemDetails")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Data.OrderDetails", "OrderDetails")
                        .WithMany("ItemDetails")
                        .HasForeignKey("OrderDetailsId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Data.OrderDetails", b =>
                {
                    b.HasOne("Data.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Data.User", "User")
                        .WithMany("OrderDetails")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
