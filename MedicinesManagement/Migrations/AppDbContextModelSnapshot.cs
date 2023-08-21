﻿// <auto-generated />
using System;
using MedicinesManagement.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicinesManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MedicinesManagement.Models.ATCCategory", b =>
                {
                    b.Property<Guid>("ATCCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ATCCategoryDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ATCCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ATCCategoryID");

                    b.ToTable("ATCCategories");
                });

            modelBuilder.Entity("MedicinesManagement.Models.ActiveSubstance", b =>
                {
                    b.Property<Guid>("SubstanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SubstanceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubstanceID");

                    b.ToTable("ActiveSubstances");
                });

            modelBuilder.Entity("MedicinesManagement.Models.Medicine", b =>
                {
                    b.Property<Guid>("MedicineID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Power")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("leafletData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("leafletURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("leafletUpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("MedicineID");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("MedicinesManagement.Models.MedicineATCCategory", b =>
                {
                    b.Property<Guid>("ConnectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ATCCategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MedicineID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ConnectionID");

                    b.HasIndex("ATCCategoryID");

                    b.HasIndex("MedicineID");

                    b.ToTable("MedicineATCCategories");
                });

            modelBuilder.Entity("MedicinesManagement.Models.MedicineActiveSubstance", b =>
                {
                    b.Property<Guid>("ConnectionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ActiveSubstanceSubstanceID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MedicineID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ConnectionID");

                    b.HasIndex("ActiveSubstanceSubstanceID");

                    b.HasIndex("MedicineID");

                    b.ToTable("MedicineActiveSubstances");
                });

            modelBuilder.Entity("MedicinesManagement.Models.MedicineATCCategory", b =>
                {
                    b.HasOne("MedicinesManagement.Models.ATCCategory", "ATCCategory")
                        .WithMany()
                        .HasForeignKey("ATCCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicinesManagement.Models.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ATCCategory");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("MedicinesManagement.Models.MedicineActiveSubstance", b =>
                {
                    b.HasOne("MedicinesManagement.Models.ActiveSubstance", "ActiveSubstance")
                        .WithMany()
                        .HasForeignKey("ActiveSubstanceSubstanceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicinesManagement.Models.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActiveSubstance");

                    b.Navigation("Medicine");
                });
#pragma warning restore 612, 618
        }
    }
}
