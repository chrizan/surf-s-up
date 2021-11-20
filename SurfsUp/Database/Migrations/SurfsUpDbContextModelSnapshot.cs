﻿// <auto-generated />
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database.Migrations
{
    [DbContext(typeof(SurfsUpDbContext))]
    partial class SurfsUpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Database.Model.BafuSurfSpot", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Outflow")
                        .HasColumnType("REAL");

                    b.HasKey("Url");

                    b.ToTable("BafuSurfSpots");
                });

            modelBuilder.Entity("Database.Model.MswSurfSpot", b =>
                {
                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<int>("BlurredStars")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FullStars")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Url");

                    b.ToTable("MswSurfSpots");
                });
#pragma warning restore 612, 618
        }
    }
}