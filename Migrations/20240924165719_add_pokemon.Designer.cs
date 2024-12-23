﻿// <auto-generated />
using System;
using BulbaClone.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BulbaClone.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240924165719_add_pokemon")]
    partial class add_pokemon
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BulbaClone.Models.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PokemonId")
                        .HasColumnType("int");

                    b.Property<int?>("PrevoFormId")
                        .HasColumnType("int");

                    b.Property<int>("Type1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Type2Id")
                        .HasColumnType("int");

                    b.Property<string>("ability0")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ability1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("atk")
                        .HasColumnType("int");

                    b.Property<string>("canGigantamax")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("def")
                        .HasColumnType("int");

                    b.Property<string>("eggGroup1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("eggGroup2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("evoCondition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("evoItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("evoLevel")
                        .HasColumnType("int");

                    b.Property<string>("evoMove")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("evoRegion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("evoType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("forme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("genderRatio")
                        .HasColumnType("float");

                    b.Property<int>("generation")
                        .HasColumnType("int");

                    b.Property<double>("heightm")
                        .HasColumnType("float");

                    b.Property<string>("hiddenAbility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("hp")
                        .HasColumnType("int");

                    b.Property<bool>("isBase")
                        .HasColumnType("bit");

                    b.Property<bool>("isDisplayed")
                        .HasColumnType("bit");

                    b.Property<string>("requiredItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("spa")
                        .HasColumnType("int");

                    b.Property<int>("spd")
                        .HasColumnType("int");

                    b.Property<int>("spe")
                        .HasColumnType("int");

                    b.Property<string>("specialAbility")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("weightkg")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("PokemonId");

                    b.HasIndex("PrevoFormId");

                    b.HasIndex("Type1Id");

                    b.HasIndex("Type2Id");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("BulbaClone.Models.PkmnType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BaseHexColour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComplementaryHexColour")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PkmnType");
                });

            modelBuilder.Entity("BulbaClone.Models.Pokemon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PkmnTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("generation")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PkmnTypeId");

                    b.ToTable("Pokemon");
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

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
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BulbaClone.Models.Form", b =>
                {
                    b.HasOne("BulbaClone.Models.Pokemon", "Pokemon")
                        .WithMany("Forms")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BulbaClone.Models.Form", "PrevoForm")
                        .WithMany("PostEvoForms")
                        .HasForeignKey("PrevoFormId");

                    b.HasOne("BulbaClone.Models.PkmnType", "Type1")
                        .WithMany()
                        .HasForeignKey("Type1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BulbaClone.Models.PkmnType", "Type2")
                        .WithMany()
                        .HasForeignKey("Type2Id");

                    b.Navigation("Pokemon");

                    b.Navigation("PrevoForm");

                    b.Navigation("Type1");

                    b.Navigation("Type2");
                });

            modelBuilder.Entity("BulbaClone.Models.Pokemon", b =>
                {
                    b.HasOne("BulbaClone.Models.PkmnType", null)
                        .WithMany("Pokemons")
                        .HasForeignKey("PkmnTypeId");
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
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

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BulbaClone.Models.Form", b =>
                {
                    b.Navigation("PostEvoForms");
                });

            modelBuilder.Entity("BulbaClone.Models.PkmnType", b =>
                {
                    b.Navigation("Pokemons");
                });

            modelBuilder.Entity("BulbaClone.Models.Pokemon", b =>
                {
                    b.Navigation("Forms");
                });
#pragma warning restore 612, 618
        }
    }
}
