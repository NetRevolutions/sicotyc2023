﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository;

#nullable disable

namespace Sicotyc.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20240102190536_UpdateFieldsUser")]
    partial class UpdateFieldsUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.LookupCode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LookupCodeId");

                    b.Property<DateTime>("CreateDtm")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime?>("DeleteDtm")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOn");

                    b.Property<Guid>("LookupCodeGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LookupCodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LookupCodeOrder")
                        .HasColumnType("int");

                    b.Property<string>("LookupCodeValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateDtm")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastUpdatedOn");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("LookupCodeGroupId");

                    b.ToTable("LOOKUP_CODE", "SCT");

                    b.HasData(
                        new
                        {
                            Id = new Guid("867c1549-7132-4e8e-174a-08da70ae983a"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(574),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                            LookupCodeName = "Por Eje",
                            LookupCodeOrder = 1,
                            LookupCodeValue = "ByAxis"
                        },
                        new
                        {
                            Id = new Guid("7e603067-a1ed-4b52-174b-08da70ae983a"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(579),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                            LookupCodeName = "Por Eje2",
                            LookupCodeOrder = 2,
                            LookupCodeValue = "ByAxis2"
                        },
                        new
                        {
                            Id = new Guid("1a011e51-2471-4ccd-174c-08da70ae983a"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(582),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                            LookupCodeName = "Por Eje3",
                            LookupCodeOrder = 3,
                            LookupCodeValue = "ByAxis3"
                        },
                        new
                        {
                            Id = new Guid("23078793-cd0a-4718-2aa4-08da71da4714"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(584),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                            LookupCodeName = "Por Eje4",
                            LookupCodeOrder = 4,
                            LookupCodeValue = "ByAxis4"
                        },
                        new
                        {
                            Id = new Guid("47b84a27-c75a-44d3-174d-08da70ae983a"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(586),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                            LookupCodeName = "Por Eje5",
                            LookupCodeOrder = 5,
                            LookupCodeValue = "ByAxis5"
                        },
                        new
                        {
                            Id = new Guid("2d253e01-afa1-4a59-bc6a-26526f0d8498"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(589),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                            LookupCodeName = "Documento Nacional de Identidad",
                            LookupCodeOrder = 1,
                            LookupCodeValue = "DNI"
                        },
                        new
                        {
                            Id = new Guid("8dc0180a-2ffc-4807-803a-37aab6ecaab2"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(591),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                            LookupCodeName = "Carnet de Extranjería",
                            LookupCodeOrder = 2,
                            LookupCodeValue = "CEX"
                        },
                        new
                        {
                            Id = new Guid("de0cc597-ad66-4497-acab-33617eb077bd"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(594),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                            LookupCodeName = "Pasaporte",
                            LookupCodeOrder = 3,
                            LookupCodeValue = "PASS"
                        },
                        new
                        {
                            Id = new Guid("792f255c-2b8b-42e6-9968-2855373e5c86"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(596),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                            LookupCodeName = "Partida de Nacimiento",
                            LookupCodeOrder = 4,
                            LookupCodeValue = "PNAC"
                        },
                        new
                        {
                            Id = new Guid("b2a7d680-b5dc-41d1-9792-695602fc2954"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(598),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                            LookupCodeName = "Carnet de FFAA",
                            LookupCodeOrder = 5,
                            LookupCodeValue = "CFFAA"
                        },
                        new
                        {
                            Id = new Guid("fe8b2536-5a20-4680-8dfe-526000df87e1"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(600),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                            LookupCodeName = "Pasaporte Diplomatico",
                            LookupCodeOrder = 6,
                            LookupCodeValue = "PASSD"
                        },
                        new
                        {
                            Id = new Guid("eaf628ee-9413-472e-a5b7-3c9d45f10cf0"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(603),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"),
                            LookupCodeName = "Empresa de Transporte",
                            LookupCodeOrder = 1,
                            LookupCodeValue = "ET"
                        },
                        new
                        {
                            Id = new Guid("58250d62-975a-4883-81f7-946c91cf2dec"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(605),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"),
                            LookupCodeName = "Otros",
                            LookupCodeOrder = 2,
                            LookupCodeValue = "OT"
                        },
                        new
                        {
                            Id = new Guid("6963984f-c5e0-4ed9-9647-46ac7054e344"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(607),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                            LookupCodeName = "IMPORTACION",
                            LookupCodeOrder = 1,
                            LookupCodeValue = "IMPO"
                        },
                        new
                        {
                            Id = new Guid("e83581fc-e05c-4c80-b5c2-e381fd7765d7"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(613),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                            LookupCodeName = "EXPORTACION",
                            LookupCodeOrder = 2,
                            LookupCodeValue = "EXPO"
                        },
                        new
                        {
                            Id = new Guid("5f38d3fd-f34e-45eb-aebf-512f5ebd94ee"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(1546),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                            LookupCodeName = "CARGA SUELTA",
                            LookupCodeOrder = 3,
                            LookupCodeValue = "CS"
                        },
                        new
                        {
                            Id = new Guid("fdc11a23-1dc7-4160-bb9d-019579c56e46"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(1553),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                            LookupCodeName = "DEVOLUCIÓN DE VACÍO",
                            LookupCodeOrder = 4,
                            LookupCodeValue = "DV"
                        },
                        new
                        {
                            Id = new Guid("e5c70df3-cf54-477f-881d-7d142f0b51aa"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(1556),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                            LookupCodeName = "TRACCIÓN",
                            LookupCodeOrder = 5,
                            LookupCodeValue = "TX"
                        },
                        new
                        {
                            Id = new Guid("8bd83659-b611-488d-aaac-e5d418bac06c"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(1559),
                            CreatedBy = "SYSTEM",
                            LookupCodeGroupId = new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                            LookupCodeName = "CAMA BAJA",
                            LookupCodeOrder = 6,
                            LookupCodeValue = "CB"
                        });
                });

            modelBuilder.Entity("Entities.Models.LookupCodeGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("LookupCodeGroupId");

                    b.Property<DateTime>("CreateDtm")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime?>("DeleteDtm")
                        .HasColumnType("datetime2")
                        .HasColumnName("DeletedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("LookupCodeGroupName");

                    b.Property<DateTime?>("UpdateDtm")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastUpdatedOn");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.ToTable("LOOKUP_CODE_GROUP", "SCT");

                    b.HasData(
                        new
                        {
                            Id = new Guid("71b0316a-9831-499a-b9bb-08da70ae70ed"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(248),
                            CreatedBy = "SYSTEM",
                            Name = "TIPO DE PAGO PEAJE"
                        },
                        new
                        {
                            Id = new Guid("86d227dc-e0ca-4a78-85f4-83a6eb30cbc7"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(267),
                            CreatedBy = "SYSTEM",
                            Name = "TIPO DE DOC. IDENTIDAD"
                        },
                        new
                        {
                            Id = new Guid("e4d10bc8-a160-4a9d-bc87-c94cf849e14c"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(269),
                            CreatedBy = "SYSTEM",
                            Name = "TIPO DE EMPRESA"
                        },
                        new
                        {
                            Id = new Guid("c6ed82d5-4a24-464b-bebd-f33c0b7f7d80"),
                            CreateDtm = new DateTime(2024, 1, 2, 19, 5, 35, 888, DateTimeKind.Utc).AddTicks(271),
                            CreatedBy = "SYSTEM",
                            Name = "TIPO DE SERVICIO"
                        });
                });

            modelBuilder.Entity("Entities.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "34a3be24-087e-4296-bef7-428872e0f99d",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "dc05c348-c103-4552-ae72-cd1a80552c43",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "b6b6a75d-86f3-431f-9548-7f64aab0f28c",
                            Name = "Member",
                            NormalizedName = "MEMBER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Entities.Models.LookupCode", b =>
                {
                    b.HasOne("Entities.Models.LookupCodeGroup", "LookupCodeGroup")
                        .WithMany("LookupCodes")
                        .HasForeignKey("LookupCodeGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LookupCodeGroup");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Entities.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Entities.Models.LookupCodeGroup", b =>
                {
                    b.Navigation("LookupCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
