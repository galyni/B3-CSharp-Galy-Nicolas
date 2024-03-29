﻿// <auto-generated />
using System;
using AppRestaurants.Data.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppRestaurants.Data.Migrations
{
    [DbContext(typeof(RestaurantsContext))]
    partial class RestaurantsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("AppRestaurants.Data.Models.Adresse", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CodePostal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Adresses");
                });

            modelBuilder.Entity("AppRestaurants.Data.Models.Grade", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Commentaire")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateDerniereVisite")
                        .HasColumnType("datetime2");

                    b.Property<int>("Note")
                        .HasColumnType("int");

                    b.Property<int>("RestaurantID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RestaurantID")
                        .IsUnique();

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("AppRestaurants.Data.Models.Restaurant", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AdresseID")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AdresseID");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("AppRestaurants.Data.Models.Grade", b =>
                {
                    b.HasOne("AppRestaurants.Data.Models.Restaurant", "Restaurant")
                        .WithOne("LastGrade")
                        .HasForeignKey("AppRestaurants.Data.Models.Grade", "RestaurantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("AppRestaurants.Data.Models.Restaurant", b =>
                {
                    b.HasOne("AppRestaurants.Data.Models.Adresse", "Adresse")
                        .WithMany()
                        .HasForeignKey("AdresseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Adresse");
                });

            modelBuilder.Entity("AppRestaurants.Data.Models.Restaurant", b =>
                {
                    b.Navigation("LastGrade");
                });
#pragma warning restore 612, 618
        }
    }
}
