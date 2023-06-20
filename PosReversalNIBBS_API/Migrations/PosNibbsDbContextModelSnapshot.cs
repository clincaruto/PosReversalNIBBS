﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PosReversalNIBBS_API.Data;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    [DbContext(typeof(PosNibbsDbContext))]
    partial class PosNibbsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PosReversalNIBBS_API.Models.Domain.ExcelResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ACCOUNT_ID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("AMOUNT")
                        .HasColumnType("float");

                    b.Property<string>("BANK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MERCHANT_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PROCESSOR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RRN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STAN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TERMINAL_ID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TRANSACTION_DATE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExcelResponses");
                });

            modelBuilder.Entity("PosReversalNIBBS_API.Models.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
