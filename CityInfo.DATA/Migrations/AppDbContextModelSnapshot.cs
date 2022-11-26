﻿// <auto-generated />
using CityInfo.DATA.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CityInfo.DATA.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("CityInfo.DOMAIN.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("City");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Cidade das acacias",
                            Name = "Maputo"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Privincia de Maputo",
                            Name = "Matola"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Cidade das Cabras 🐐",
                            Name = "Boane"
                        });
                });

            modelBuilder.Entity("CityInfo.DOMAIN.Models.PointsOfInterest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CityId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("PointsOfInterest");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            CityId = 1,
                            Description = "Maior Shopping do pais até 2010",
                            Name = "Shoping Center"
                        },
                        new
                        {
                            Id = 1,
                            CityId = 2,
                            Description = "Jardim da Matola",
                            Name = "Cinema Lusumundo"
                        },
                        new
                        {
                            Id = 2,
                            CityId = 2,
                            Description = "Restourante e Bar",
                            Name = "Santorine"
                        },
                        new
                        {
                            Id = 4,
                            CityId = 3,
                            Description = "Pomar de Banana",
                            Name = "Banana Landia"
                        });
                });

            modelBuilder.Entity("CityInfo.DOMAIN.Models.PointsOfInterest", b =>
                {
                    b.HasOne("CityInfo.DOMAIN.Models.City", "City")
                        .WithMany("PointsOfInterests")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("CityInfo.DOMAIN.Models.City", b =>
                {
                    b.Navigation("PointsOfInterests");
                });
#pragma warning restore 612, 618
        }
    }
}