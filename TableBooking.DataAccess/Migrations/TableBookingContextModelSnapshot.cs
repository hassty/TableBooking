﻿// <auto-generated />
using System;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(TableBookingContext))]
    partial class TableBookingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entities.Menu.MenuCategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuCategoryEntity");
                });

            modelBuilder.Entity("Core.Entities.Menu.MenuEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("Id");

                    b.ToTable("MenuEntity");
                });

            modelBuilder.Entity("Core.Entities.Menu.MenuItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderEntityId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OrderEntityId");

                    b.ToTable("MenuItemEntity");
                });

            modelBuilder.Entity("Core.Entities.OrderEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ConfirmedByAdmin")
                        .HasColumnType("bit");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("ReservationDuration")
                        .HasColumnType("time");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("int");

                    b.Property<int?>("TableId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("TableId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Core.Entities.RestaurantEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("OpenedFrom")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("OpenedTill")
                        .HasColumnType("time");

                    b.Property<int?>("OrderOptionsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name", "Address");

                    b.HasIndex("OrderOptionsId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Core.Entities.RestaurantOrderOptionsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LatestOrderDate")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("LongestReservationDuration")
                        .HasColumnType("time");

                    b.Property<string>("OffDays")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("ShortestReservationDuration")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("RestaurantOrderOptionsEntity");
                });

            modelBuilder.Entity("Core.Entities.TableEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("TableEntity");
                });

            modelBuilder.Entity("Core.Entities.Users.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(44)
                        .HasColumnType("nvarchar(44)");

                    b.Property<int>("Salt")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasAlternateKey("Username");

                    b.ToTable("UserEntity");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UserEntity");
                });

            modelBuilder.Entity("Core.Entities.Users.AdminEntity", b =>
                {
                    b.HasBaseType("Core.Entities.Users.UserEntity");

                    b.HasDiscriminator().HasValue("AdminEntity");
                });

            modelBuilder.Entity("Core.Entities.Users.CustomerEntity", b =>
                {
                    b.HasBaseType("Core.Entities.Users.UserEntity");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("CustomerEntity");
                });

            modelBuilder.Entity("Core.Entities.Menu.MenuCategoryEntity", b =>
                {
                    b.HasOne("Core.Entities.Menu.MenuEntity", "Menu")
                        .WithMany("Categories")
                        .HasForeignKey("MenuId");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("Core.Entities.Menu.MenuItemEntity", b =>
                {
                    b.HasOne("Core.Entities.Menu.MenuCategoryEntity", "Category")
                        .WithMany("MenuItems")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Core.Entities.OrderEntity", null)
                        .WithMany("MenuItems")
                        .HasForeignKey("OrderEntityId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Core.Entities.OrderEntity", b =>
                {
                    b.HasOne("Core.Entities.Users.CustomerEntity", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Core.Entities.RestaurantEntity", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId");

                    b.HasOne("Core.Entities.TableEntity", "Table")
                        .WithMany()
                        .HasForeignKey("TableId");

                    b.Navigation("Customer");

                    b.Navigation("Restaurant");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Core.Entities.RestaurantEntity", b =>
                {
                    b.HasOne("Core.Entities.RestaurantOrderOptionsEntity", "OrderOptions")
                        .WithMany()
                        .HasForeignKey("OrderOptionsId");

                    b.Navigation("OrderOptions");
                });

            modelBuilder.Entity("Core.Entities.TableEntity", b =>
                {
                    b.HasOne("Core.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("Tables")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Core.Entities.Menu.MenuCategoryEntity", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("Core.Entities.Menu.MenuEntity", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("Core.Entities.OrderEntity", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("Core.Entities.RestaurantEntity", b =>
                {
                    b.Navigation("Tables");
                });

            modelBuilder.Entity("Core.Entities.Users.CustomerEntity", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
