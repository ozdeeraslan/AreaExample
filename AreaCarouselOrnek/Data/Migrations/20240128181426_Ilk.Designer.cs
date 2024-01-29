﻿// <auto-generated />
using AreaCarouselOrnek.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AreaCarouselOrnek.Data.Migrations
{
    [DbContext(typeof(UygulamaDbContext))]
    [Migration("20240128181426_Ilk")]
    partial class Ilk
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AreaCarouselOrnek.Data.Slayt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Aciklama")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Baslik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResimYolu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sira")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Slaytlar");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Aciklama = "Hepimiz birer yağmur damlasıyız aslında",
                            Baslik = "Yagmur",
                            ResimYolu = "yagmur.jpg",
                            Sira = 1
                        },
                        new
                        {
                            Id = 2,
                            Aciklama = "Eğer sen doğaya zarar vermezsen doğa sana asla zarar vermez",
                            Baslik = "Manzara",
                            ResimYolu = "manzara.jpg",
                            Sira = 2
                        },
                        new
                        {
                            Id = 3,
                            Aciklama = "Rengarenk gökkuşağını görmek için yağmuru doyasıya yaşamak gerekir",
                            Baslik = "Gökkusagi",
                            ResimYolu = "gokkusagi.jpg",
                            Sira = 3
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
