﻿// <auto-generated />
using System;
using HotelRestaurantAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelRestaurantAPI.Migrations
{
    [DbContext(typeof(HotelDataContext))]
    partial class HotelDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HotelRestaurantAPI.Models.CheckIn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("GuestId")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("RoomNumber");

                    b.ToTable("CheckIns");
                });

            modelBuilder.Entity("HotelRestaurantAPI.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomNumber");

                    b.ToTable("Guest");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Guest");
                });

            modelBuilder.Entity("HotelRestaurantAPI.Models.Room", b =>
                {
                    b.Property<int>("RoomNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomNumber"), 1L, 1);

                    b.HasKey("RoomNumber");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelRestaurantAPI.Models.GuestAdult", b =>
                {
                    b.HasBaseType("HotelRestaurantAPI.Models.Guest");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("GuestAdult");
                });

            modelBuilder.Entity("HotelRestaurantAPI.Models.GuestChild", b =>
                {
                    b.HasBaseType("HotelRestaurantAPI.Models.Guest");

                    b.HasDiscriminator().HasValue("GuestChild");
                });

            modelBuilder.Entity("HotelRestaurantAPI.Models.CheckIn", b =>
                {
                    b.HasOne("HotelRestaurantAPI.Models.Guest", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelRestaurantAPI.Models.Room", "Room")
                        .WithMany("CheckIns")
                        .HasForeignKey("RoomNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guest");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelRestaurantAPI.Models.Guest", b =>
                {
                    b.HasOne("HotelRestaurantAPI.Models.Room", "Room")
                        .WithMany("Guests")
                        .HasForeignKey("RoomNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelRestaurantAPI.Models.Room", b =>
                {
                    b.Navigation("CheckIns");

                    b.Navigation("Guests");
                });
#pragma warning restore 612, 618
        }
    }
}
