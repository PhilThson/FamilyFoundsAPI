﻿// <auto-generated />
using System;
using FamilyFoundsApi.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FamilyFoundsApi.Persistence.Migrations
{
    [DbContext(typeof(FamilyFoundsDbContext))]
    [Migration("20240122091145_ChangeNumberIndex")]
    partial class ChangeNumberIndex
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("FamilyFoundsApi.Domain.Models.Category", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            Name = "Artykuły spożywcze"
                        },
                        new
                        {
                            Id = (short)2,
                            Name = "Artykuły chemiczne i higieniczne"
                        },
                        new
                        {
                            Id = (short)3,
                            Name = "Ubrania"
                        },
                        new
                        {
                            Id = (short)4,
                            Name = "Rozrywka"
                        },
                        new
                        {
                            Id = (short)5,
                            Name = "Transport"
                        },
                        new
                        {
                            Id = (short)6,
                            Name = "Sprzęt domowy i budowlany"
                        },
                        new
                        {
                            Id = (short)7,
                            Name = "Zdrowie i uroda"
                        },
                        new
                        {
                            Id = (short)8,
                            Name = "Rachunki"
                        },
                        new
                        {
                            Id = (short)9,
                            Name = "Dzieci"
                        },
                        new
                        {
                            Id = (short)10,
                            Name = "Inne"
                        });
                });

            modelBuilder.Entity("FamilyFoundsApi.Domain.Models.ImportSource", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ImportSource");

                    b.HasData(
                        new
                        {
                            Id = (short)1,
                            Name = "ING"
                        },
                        new
                        {
                            Id = (short)2,
                            Name = "MILLENNIUM"
                        });
                });

            modelBuilder.Entity("FamilyFoundsApi.Domain.Models.Transaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Account")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<short?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Contractor")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("ContractorAccountNumber")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ContractorBankName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .HasMaxLength(3)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("TEXT");

                    b.Property<short?>("ImportSourceId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(true);

                    b.Property<string>("Number")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("PostingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ImportSourceId");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("FamilyFoundsApi.Domain.Models.Transaction", b =>
                {
                    b.HasOne("FamilyFoundsApi.Domain.Models.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId");

                    b.HasOne("FamilyFoundsApi.Domain.Models.ImportSource", "ImportSource")
                        .WithMany("Transactions")
                        .HasForeignKey("ImportSourceId");

                    b.Navigation("Category");

                    b.Navigation("ImportSource");
                });

            modelBuilder.Entity("FamilyFoundsApi.Domain.Models.Category", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("FamilyFoundsApi.Domain.Models.ImportSource", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}