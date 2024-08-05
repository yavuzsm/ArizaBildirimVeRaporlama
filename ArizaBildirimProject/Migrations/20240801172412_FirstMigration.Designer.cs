﻿// <auto-generated />
using System;
using ArizaBildirimProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ArizaBildirimProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240801172412_FirstMigration")]
    partial class FirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArizaBildirimProject.Models.Aktivite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RaporId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RaporId");

                    b.ToTable("Aktiviteler");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.ArizaKisaTanim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArizaTurId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArizaTurId");

                    b.ToTable("ArizaKisaTanimlar");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.ArizaTur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ArizaTurler");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Bolum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Bolum");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Cihaz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BolumId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("BolumId");

                    b.ToTable("Cihaz");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Departman", b =>
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

                    b.ToTable("Departman");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Rapor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArizaKisaTanimId")
                        .HasColumnType("int");

                    b.Property<int>("ArizaTurId")
                        .HasColumnType("int");

                    b.Property<int>("BolumId")
                        .HasColumnType("int");

                    b.Property<int>("CihazId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArizaKisaTanimId");

                    b.HasIndex("ArizaTurId");

                    b.HasIndex("BolumId");

                    b.HasIndex("CihazId");

                    b.HasIndex("DepartmanId");

                    b.ToTable("Rapor");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roller");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Statu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RaporId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RaporId");

                    b.ToTable("Statuler");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Teshis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RaporId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RaporId");

                    b.ToTable("Teshisler");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmanId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmanId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Aktivite", b =>
                {
                    b.HasOne("ArizaBildirimProject.Models.Rapor", "Rapor")
                        .WithMany()
                        .HasForeignKey("RaporId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rapor");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.ArizaKisaTanim", b =>
                {
                    b.HasOne("ArizaBildirimProject.Models.ArizaTur", "ArizaTur")
                        .WithMany("ArizaKisaTanimlar")
                        .HasForeignKey("ArizaTurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArizaTur");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Bolum", b =>
                {
                    b.HasOne("ArizaBildirimProject.Models.Departman", "Departman")
                        .WithMany("Bolumler")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departman");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Cihaz", b =>
                {
                    b.HasOne("ArizaBildirimProject.Models.Bolum", "Bolum")
                        .WithMany("Cihazs")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bolum");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Rapor", b =>
                {
                    b.HasOne("ArizaBildirimProject.Models.ArizaKisaTanim", "ArizaKisaTanim")
                        .WithMany("Raporlar")
                        .HasForeignKey("ArizaKisaTanimId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ArizaBildirimProject.Models.ArizaTur", "ArizaTur")
                        .WithMany("Raporlar")
                        .HasForeignKey("ArizaTurId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ArizaBildirimProject.Models.Bolum", "Bolum")
                        .WithMany("Raporlar")
                        .HasForeignKey("BolumId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ArizaBildirimProject.Models.Cihaz", "Cihaz")
                        .WithMany("Raporlar")
                        .HasForeignKey("CihazId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ArizaBildirimProject.Models.Departman", "Departman")
                        .WithMany("Raporlar")
                        .HasForeignKey("DepartmanId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ArizaKisaTanim");

                    b.Navigation("ArizaTur");

                    b.Navigation("Bolum");

                    b.Navigation("Cihaz");

                    b.Navigation("Departman");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Statu", b =>
                {
                    b.HasOne("ArizaBildirimProject.Models.Rapor", "Rapor")
                        .WithMany()
                        .HasForeignKey("RaporId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rapor");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Teshis", b =>
                {
                    b.HasOne("ArizaBildirimProject.Models.Rapor", "Rapor")
                        .WithMany()
                        .HasForeignKey("RaporId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rapor");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.User", b =>
                {
                    b.HasOne("ArizaBildirimProject.Models.Departman", "Departman")
                        .WithMany("Users")
                        .HasForeignKey("DepartmanId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ArizaBildirimProject.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Departman");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.ArizaKisaTanim", b =>
                {
                    b.Navigation("Raporlar");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.ArizaTur", b =>
                {
                    b.Navigation("ArizaKisaTanimlar");

                    b.Navigation("Raporlar");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Bolum", b =>
                {
                    b.Navigation("Cihazs");

                    b.Navigation("Raporlar");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Cihaz", b =>
                {
                    b.Navigation("Raporlar");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Departman", b =>
                {
                    b.Navigation("Bolumler");

                    b.Navigation("Raporlar");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ArizaBildirimProject.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
