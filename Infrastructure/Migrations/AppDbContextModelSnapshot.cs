﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.8");

            modelBuilder.Entity("Domain.Entities.Energy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("CurrentValue")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Energy");
                });

            modelBuilder.Entity("Domain.Entities.EnergyMeter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("CurrentValueDay")
                        .HasColumnType("REAL");

                    b.Property<double>("CurrentValueNight")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("EnergyMeter");
                });

            modelBuilder.Entity("Domain.Entities.GVS", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("CurrentValue")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("GVS");
                });

            modelBuilder.Entity("Domain.Entities.HVS", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<double>("CurrentValue")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("HVS");
                });

            modelBuilder.Entity("Domain.Entities.Statistic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EnergyId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EnergyMeterId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("GVSId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("HVSId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EnergyId")
                        .IsUnique();

                    b.HasIndex("EnergyMeterId")
                        .IsUnique();

                    b.HasIndex("GVSId")
                        .IsUnique();

                    b.HasIndex("HVSId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Statistics");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasEnergyMeter")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasGvsMeter")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasHvsMeter")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Statistic", b =>
                {
                    b.HasOne("Domain.Entities.Energy", "Energy")
                        .WithOne("Statistic")
                        .HasForeignKey("Domain.Entities.Statistic", "EnergyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.EnergyMeter", "EnergyMeter")
                        .WithOne("Statistic")
                        .HasForeignKey("Domain.Entities.Statistic", "EnergyMeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.GVS", "GVS")
                        .WithOne("Statistic")
                        .HasForeignKey("Domain.Entities.Statistic", "GVSId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.HVS", "HVS")
                        .WithOne("Statistic")
                        .HasForeignKey("Domain.Entities.Statistic", "HVSId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Statistic")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Energy");

                    b.Navigation("EnergyMeter");

                    b.Navigation("GVS");

                    b.Navigation("HVS");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Energy", b =>
                {
                    b.Navigation("Statistic");
                });

            modelBuilder.Entity("Domain.Entities.EnergyMeter", b =>
                {
                    b.Navigation("Statistic");
                });

            modelBuilder.Entity("Domain.Entities.GVS", b =>
                {
                    b.Navigation("Statistic");
                });

            modelBuilder.Entity("Domain.Entities.HVS", b =>
                {
                    b.Navigation("Statistic");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Statistic");
                });
#pragma warning restore 612, 618
        }
    }
}
