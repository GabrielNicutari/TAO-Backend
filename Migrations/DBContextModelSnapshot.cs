﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TAO_Backend.Models;

namespace TAO_Backend.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TAO_Backend.Models.DailyReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<int>("Energy")
                        .HasColumnType("int(11)")
                        .HasColumnName("energy");

                    b.Property<int>("Flow")
                        .HasColumnType("int(11)")
                        .HasColumnName("flow");

                    b.Property<int>("HourCounter")
                        .HasColumnType("int(11)")
                        .HasColumnName("hour_counter");

                    b.Property<int>("HouseReadingId")
                        .HasColumnType("int(11)")
                        .HasColumnName("house_reading_id");

                    b.Property<double>("Power")
                        .HasColumnType("double")
                        .HasColumnName("power");

                    b.Property<double>("TempForward")
                        .HasColumnType("double")
                        .HasColumnName("temp_forward");

                    b.Property<double>("TempReturn")
                        .HasColumnType("double")
                        .HasColumnName("temp_return");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("date")
                        .HasColumnName("timestamp");

                    b.Property<double>("Volume")
                        .HasColumnType("double")
                        .HasColumnName("volume");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "HouseReadingId" }, "house_id_idx");

                    b.ToTable("daily_readings");
                });

            modelBuilder.Entity("TAO_Backend.Models.House", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("address");

                    b.Property<int>("Area")
                        .HasColumnType("int(11)")
                        .HasColumnName("area");

                    b.Property<int>("YearBuilt")
                        .HasColumnType("int(11)")
                        .HasColumnName("year_built");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("zip");

                    b.HasKey("Id");

                    b.ToTable("houses");
                });

            modelBuilder.Entity("TAO_Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<int>("HouseId")
                        .HasColumnType("int(11)")
                        .HasColumnName("house_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("phone_number");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "HouseId" }, "house_id_UNIQUE")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("TAO_Backend.Models.DailyReading", b =>
                {
                    b.HasOne("TAO_Backend.Models.House", "HouseReading")
                        .WithMany("DailyReadings")
                        .HasForeignKey("HouseReadingId")
                        .HasConstraintName("house_reading_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HouseReading");
                });

            modelBuilder.Entity("TAO_Backend.Models.User", b =>
                {
                    b.HasOne("TAO_Backend.Models.House", "House")
                        .WithOne("User")
                        .HasForeignKey("TAO_Backend.Models.User", "HouseId")
                        .HasConstraintName("house_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");
                });

            modelBuilder.Entity("TAO_Backend.Models.House", b =>
                {
                    b.Navigation("DailyReadings");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
