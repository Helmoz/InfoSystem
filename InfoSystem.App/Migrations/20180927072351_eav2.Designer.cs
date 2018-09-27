﻿// <auto-generated />
using System;
using InfoSystem.App.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InfoSystem.App.Migrations
{
    [DbContext(typeof(InfoSystemDbContext))]
    [Migration("20180927072351_eav2")]
    partial class eav2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-preview2-35157")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.Entity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Editable");

                    b.Property<string>("EntityId");

                    b.HasKey("Id");

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.Market", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.MarketProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MarketId1");

                    b.Property<int?>("ProductId1");

                    b.HasKey("Id");

                    b.HasIndex("MarketId1");

                    b.HasIndex("ProductId1");

                    b.ToTable("MarketProducts");
                });

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.Properties", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EntityId");

                    b.Property<string>("Name");

                    b.Property<int?>("ValueId");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.HasIndex("ValueId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.Values", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.MarketProduct", b =>
                {
                    b.HasOne("InfoSystem.Infrastructure.Entities.Market")
                        .WithMany("Products")
                        .HasForeignKey("MarketId1");

                    b.HasOne("InfoSystem.Infrastructure.Entities.Product")
                        .WithMany("InMarkets")
                        .HasForeignKey("ProductId1");
                });

            modelBuilder.Entity("InfoSystem.Infrastructure.Entities.Properties", b =>
                {
                    b.HasOne("InfoSystem.Infrastructure.Entities.Entity", "Entity")
                        .WithMany("Properties")
                        .HasForeignKey("EntityId");

                    b.HasOne("InfoSystem.Infrastructure.Entities.Values", "Value")
                        .WithMany()
                        .HasForeignKey("ValueId");
                });
#pragma warning restore 612, 618
        }
    }
}