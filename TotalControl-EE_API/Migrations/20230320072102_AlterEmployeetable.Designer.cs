﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TotalControl_EE_API.Data;

#nullable disable

namespace TotalControlEEAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230320072102_AlterEmployeetable")]
    partial class AlterEmployeetable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TotalControl_EE_API.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Gender = "M",
                            LastName = "Rodriguez",
                            Name = "Angel",
                            Status = "Unmodified"
                        },
                        new
                        {
                            Id = 2,
                            Gender = "M",
                            LastName = "Rodriguez",
                            Name = "Ramón",
                            Status = "Unmodified"
                        });
                });

            modelBuilder.Entity("TotalControl_EE_API.Models.Register", b =>
                {
                    b.Property<int>("IdRegister")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRegister"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEmployee")
                        .HasColumnType("int");

                    b.Property<string>("businessLocation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("registerType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRegister");

                    b.HasIndex("IdEmployee");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("TotalControl_EE_API.Models.Register", b =>
                {
                    b.HasOne("TotalControl_EE_API.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("IdEmployee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}