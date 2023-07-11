﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PosReversalNIBBS_API.Data;

#nullable disable

namespace PosReversalNIBBS_API.Migrations
{
    [DbContext(typeof(PosNibbsDbContext))]
    [Migration("20230711220105_add reversal coloum")]
    partial class addreversalcoloum
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Reversal")
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

                    b.Property<Guid?>("UploadedExcelDetailBatchId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UploadedExcelDetailBatchId");

                    b.ToTable("ExcelResponses");
                });

            modelBuilder.Entity("PosReversalNIBBS_API.Models.Domain.UploadedExcelDetail", b =>
                {
                    b.Property<Guid>("BatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateUploaded")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("FileSizeInBytes")
                        .HasColumnType("bigint");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<double?>("TotalTransaction")
                        .HasColumnType("float");

                    b.HasKey("BatchId");

                    b.ToTable("UploadedExcelDetails");
                });

            modelBuilder.Entity("PosReversalNIBBS_API.Models.Domain.ExcelResponse", b =>
                {
                    b.HasOne("PosReversalNIBBS_API.Models.Domain.UploadedExcelDetail", "uploadedExcelDetail")
                        .WithMany()
                        .HasForeignKey("UploadedExcelDetailBatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("uploadedExcelDetail");
                });
#pragma warning restore 612, 618
        }
    }
}