﻿// <auto-generated />
using System;
using BrosBurger.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BrosBurger.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("BrosBurger.Models.Banners", b =>
                {
                    b.Property<int>("BannerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImagemBanner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeBanner")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BannerId");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("BrosBurger.Models.Categoria", b =>
                {
                    b.Property<int>("CategoriaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NomeCategoria")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CategoriaID");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("BrosBurger.Models.Produto", b =>
                {
                    b.Property<int>("ProdutoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BannersBannerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DescricaoProduto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImagemProduto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeProduto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("ValorProduto")
                        .HasColumnType("REAL");

                    b.HasKey("ProdutoId");

                    b.HasIndex("BannersBannerId");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("BrosBurger.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoginUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NomeUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SenhaUsuario")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("BrosBurger.Models.Produto", b =>
                {
                    b.HasOne("BrosBurger.Models.Banners", null)
                        .WithMany("Produtos")
                        .HasForeignKey("BannersBannerId");

                    b.HasOne("BrosBurger.Models.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("BrosBurger.Models.Banners", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("BrosBurger.Models.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}