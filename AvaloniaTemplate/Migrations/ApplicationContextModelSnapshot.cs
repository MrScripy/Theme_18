﻿// <auto-generated />
using AvaloniaTemplate.Desktop.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AvaloniaTemplate.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("AvaloniaTemplate.Models.Amphibian", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LatName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimalTypeId");

                    b.ToTable("Amphibians");
                });

            modelBuilder.Entity("AvaloniaTemplate.Models.AnimalType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("AnimalTypes");
                });

            modelBuilder.Entity("AvaloniaTemplate.Models.Bird", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LatName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimalTypeId");

                    b.ToTable("Birds");
                });

            modelBuilder.Entity("AvaloniaTemplate.Models.Mammal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnimalTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LatName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AnimalTypeId");

                    b.ToTable("Mammals");
                });

            modelBuilder.Entity("AvaloniaTemplate.Models.Amphibian", b =>
                {
                    b.HasOne("AvaloniaTemplate.Models.AnimalType", "AnimalType")
                        .WithMany("Amphibians")
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalType");
                });

            modelBuilder.Entity("AvaloniaTemplate.Models.Bird", b =>
                {
                    b.HasOne("AvaloniaTemplate.Models.AnimalType", "AnimalType")
                        .WithMany("Birds")
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalType");
                });

            modelBuilder.Entity("AvaloniaTemplate.Models.Mammal", b =>
                {
                    b.HasOne("AvaloniaTemplate.Models.AnimalType", "AnimalType")
                        .WithMany("Mammals")
                        .HasForeignKey("AnimalTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnimalType");
                });

            modelBuilder.Entity("AvaloniaTemplate.Models.AnimalType", b =>
                {
                    b.Navigation("Amphibians");

                    b.Navigation("Birds");

                    b.Navigation("Mammals");
                });
#pragma warning restore 612, 618
        }
    }
}