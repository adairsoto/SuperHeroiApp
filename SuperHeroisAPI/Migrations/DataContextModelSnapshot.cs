﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SuperHeroisAPI.Data;

#nullable disable

namespace SuperHeroisAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SuperHeroisAPI.Models.Heroi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("Altura")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("NomeHeroi")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<double>("Peso")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Herois");
                });

            modelBuilder.Entity("SuperHeroisAPI.Models.HeroiSuperPoderes", b =>
                {
                    b.Property<int>("HeroiId")
                        .HasColumnType("int");

                    b.Property<int>("SuperPoderesId")
                        .HasColumnType("int");

                    b.HasKey("HeroiId", "SuperPoderesId");

                    b.HasIndex("SuperPoderesId");

                    b.ToTable("HeroiSuperPoderes");
                });

            modelBuilder.Entity("SuperHeroisAPI.Models.SuperPoderes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("SuperPoder")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SuperPoderes");
                });

            modelBuilder.Entity("SuperHeroisAPI.Models.HeroiSuperPoderes", b =>
                {
                    b.HasOne("SuperHeroisAPI.Models.Heroi", "Heroi")
                        .WithMany("HeroiSuperPoderes")
                        .HasForeignKey("HeroiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuperHeroisAPI.Models.SuperPoderes", "SuperPoderes")
                        .WithMany("HeroiSuperPoderes")
                        .HasForeignKey("SuperPoderesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Heroi");

                    b.Navigation("SuperPoderes");
                });

            modelBuilder.Entity("SuperHeroisAPI.Models.Heroi", b =>
                {
                    b.Navigation("HeroiSuperPoderes");
                });

            modelBuilder.Entity("SuperHeroisAPI.Models.SuperPoderes", b =>
                {
                    b.Navigation("HeroiSuperPoderes");
                });
#pragma warning restore 612, 618
        }
    }
}