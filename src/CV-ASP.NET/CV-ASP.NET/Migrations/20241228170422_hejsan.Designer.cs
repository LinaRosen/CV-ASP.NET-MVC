﻿// <auto-generated />
using System;
using CV_ASP.NET.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CV_ASP.NET.Migrations
{
    [DbContext(typeof(TestDataContext))]
    [Migration("20241228170422_hejsan")]
    partial class hejsan
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CV_ASP.NET.Models.Adress", b =>
                {
                    b.Property<int>("Aid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Aid"));

                    b.Property<string>("Anvid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Gatunamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Postnummer")
                        .HasColumnType("int");

                    b.Property<string>("Stad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Aid");

                    b.HasIndex("Anvid")
                        .IsUnique();

                    b.ToTable("Adresser");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.AnvProjekt", b =>
                {
                    b.Property<string>("Anvid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Pid")
                        .HasColumnType("int");

                    b.HasKey("Anvid", "Pid");

                    b.HasIndex("Pid");

                    b.ToTable("AnvProjekt");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Anvandare", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("Aktiverad")
                        .HasColumnType("bit");

                    b.Property<string>("Anvandarnamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Efternamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Fornamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ListadStartsida")
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

                    b.Property<bool>("PrivatProfil")
                        .HasColumnType("bit");

                    b.Property<string>("Profilbild")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("CV_ASP.NET.Models.CV", b =>
                {
                    b.Property<int>("Cvid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cvid"));

                    b.Property<int?>("AntalBesokare")
                        .HasColumnType("int");

                    b.Property<string>("AnvandarNamn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Beskrivning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profilbild")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cvid");

                    b.HasIndex("AnvandarNamn")
                        .IsUnique()
                        .HasFilter("[AnvandarNamn] IS NOT NULL");

                    b.ToTable("CV");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.CV_Erfarenhet", b =>
                {
                    b.Property<int>("Cvid")
                        .HasColumnType("int");

                    b.Property<int>("Eid")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Slutdatum")
                        .HasColumnType("date");

                    b.Property<DateOnly>("Startdatum")
                        .HasColumnType("date");

                    b.HasKey("Cvid", "Eid");

                    b.HasIndex("Eid");

                    b.ToTable("CV_Erfarenhet");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.CV_Utbildning", b =>
                {
                    b.Property<int>("CVid")
                        .HasColumnType("int");

                    b.Property<int>("Uid")
                        .HasColumnType("int");

                    b.Property<DateOnly>("Slutdatum")
                        .HasColumnType("date");

                    b.Property<DateOnly>("Startdatum")
                        .HasColumnType("date");

                    b.HasKey("CVid", "Uid");

                    b.HasIndex("Uid");

                    b.ToTable("CV_Utbildning");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.CV_kompetenser", b =>
                {
                    b.Property<int>("Cvid")
                        .HasColumnType("int");

                    b.Property<int>("Kid")
                        .HasColumnType("int");

                    b.HasKey("Cvid", "Kid");

                    b.HasIndex("Kid");

                    b.ToTable("CV_Kompetenser");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Erfarenhet", b =>
                {
                    b.Property<int>("Eid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Eid"));

                    b.Property<string>("Arbetsgivare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Beskrivning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Eid");

                    b.ToTable("Erfarenhet");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Kompetenser", b =>
                {
                    b.Property<int>("Kid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Kid"));

                    b.Property<string>("Beskrivning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KompetensNamn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Kid");

                    b.ToTable("Kompetenser");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Meddelande", b =>
                {
                    b.Property<int>("Mid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Mid"));

                    b.Property<string>("AnonymAnvandare")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FranAnvandareId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Innehall")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Last")
                        .HasColumnType("bit");

                    b.Property<string>("TillAnvandareId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Mid");

                    b.HasIndex("FranAnvandareId");

                    b.HasIndex("TillAnvandareId");

                    b.ToTable("Meddelande");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Projekt", b =>
                {
                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pid"));

                    b.Property<string>("Beskrivning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("DatumSkapad")
                        .HasColumnType("date");

                    b.Property<string>("Namn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkapadAv")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Pid");

                    b.HasIndex("SkapadAv");

                    b.ToTable("Projekt");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Utbildning", b =>
                {
                    b.Property<int>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Uid"));

                    b.Property<string>("Beskrivning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instutition")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kurs_program")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Uid");

                    b.ToTable("Utbildning");
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

            modelBuilder.Entity("CV_ASP.NET.Models.Adress", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.Anvandare", "anvandare")
                        .WithOne("Adress")
                        .HasForeignKey("CV_ASP.NET.Models.Adress", "Anvid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("anvandare");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.AnvProjekt", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.Anvandare", "Anvandare")
                        .WithMany()
                        .HasForeignKey("Anvid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CV_ASP.NET.Models.Projekt", "Projekt")
                        .WithMany("AnvProjekt")
                        .HasForeignKey("Pid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anvandare");

                    b.Navigation("Projekt");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.CV", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.Anvandare", "anvandare")
                        .WithOne("CV")
                        .HasForeignKey("CV_ASP.NET.Models.CV", "AnvandarNamn");

                    b.Navigation("anvandare");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.CV_Erfarenhet", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.CV", "cv")
                        .WithMany()
                        .HasForeignKey("Cvid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CV_ASP.NET.Models.Erfarenhet", "erfarenhet")
                        .WithMany("cv_Erfarenhet")
                        .HasForeignKey("Eid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cv");

                    b.Navigation("erfarenhet");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.CV_Utbildning", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.CV", "cv")
                        .WithMany()
                        .HasForeignKey("CVid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CV_ASP.NET.Models.Utbildning", "utbildning")
                        .WithMany("cv_Utbildning")
                        .HasForeignKey("Uid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("cv");

                    b.Navigation("utbildning");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.CV_kompetenser", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.CV", "CV")
                        .WithMany()
                        .HasForeignKey("Cvid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CV_ASP.NET.Models.Kompetenser", "Kompetenser")
                        .WithMany("CV_kompetenser")
                        .HasForeignKey("Kid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CV");

                    b.Navigation("Kompetenser");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Meddelande", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.Anvandare", "Frananvandare")
                        .WithMany("skickatMeddelande")
                        .HasForeignKey("FranAnvandareId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("CV_ASP.NET.Models.Anvandare", "Tillanvandare")
                        .WithMany("TagitEmotMeddelande")
                        .HasForeignKey("TillAnvandareId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Frananvandare");

                    b.Navigation("Tillanvandare");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Projekt", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.Anvandare", "Anvandare")
                        .WithMany()
                        .HasForeignKey("SkapadAv");

                    b.Navigation("Anvandare");
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
                    b.HasOne("CV_ASP.NET.Models.Anvandare", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.Anvandare", null)
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

                    b.HasOne("CV_ASP.NET.Models.Anvandare", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CV_ASP.NET.Models.Anvandare", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Anvandare", b =>
                {
                    b.Navigation("Adress");

                    b.Navigation("CV");

                    b.Navigation("TagitEmotMeddelande");

                    b.Navigation("skickatMeddelande");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Erfarenhet", b =>
                {
                    b.Navigation("cv_Erfarenhet");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Kompetenser", b =>
                {
                    b.Navigation("CV_kompetenser");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Projekt", b =>
                {
                    b.Navigation("AnvProjekt");
                });

            modelBuilder.Entity("CV_ASP.NET.Models.Utbildning", b =>
                {
                    b.Navigation("cv_Utbildning");
                });
#pragma warning restore 612, 618
        }
    }
}
