﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repositories;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240408120015_startpoint")]
    partial class startpoint
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Advert", b =>
                {
                    b.Property<int>("AdvertId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdvertId"));

                    b.Property<DateTime?>("AdvertDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("AdvertDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("AdvertKilo")
                        .HasColumnType("integer");

                    b.Property<string>("AdvertName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.HasKey("AdvertId");

                    b.ToTable("Adverts");
                });

            modelBuilder.Entity("Entities.Cso", b =>
                {
                    b.Property<int>("CsoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CsoId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CsoId");

                    b.ToTable("Csos");
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PersonId"));

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("text");

                    b.Property<string>("PersonEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PersonNickName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PersonPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PersonPhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PersonSurname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Entities.Restaurant", b =>
                {
                    b.Property<int>("restaurantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("restaurantId"));

                    b.Property<string>("PasswordSalt")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("restaurantAdress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("restaurantMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("restaurantName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("restaurantNickname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("restaurantNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("restaurantPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("restaurantId");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
