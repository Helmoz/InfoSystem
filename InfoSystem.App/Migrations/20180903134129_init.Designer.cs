﻿// <auto-generated />
using InfoSystem.App.DataBase.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InfoSystem.App.Migrations
{
    [DbContext(typeof(InfoSystemDbContext))]
    [Migration("20180903134129_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("InfoSystem.App.DataBase.Entities.Market", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Markets");
                });

            modelBuilder.Entity("InfoSystem.App.DataBase.Entities.MarketProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MarketId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("MarketId");

                    b.HasIndex("ProductId");

                    b.ToTable("MarketProduct");
                });

            modelBuilder.Entity("InfoSystem.App.DataBase.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cost");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("InfoSystem.App.DataBase.Entities.MarketProduct", b =>
                {
                    b.HasOne("InfoSystem.App.DataBase.Entities.Market", "Market")
                        .WithMany("Products")
                        .HasForeignKey("MarketId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InfoSystem.App.DataBase.Entities.Product", "Product")
                        .WithMany("InMarkets")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
