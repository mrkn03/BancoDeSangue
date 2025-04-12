﻿// <auto-generated />
using System;
using BancoDeSangue.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BancoDeSangue.Migrations
{
    [DbContext(typeof(BancoDeSangueContext))]
    [Migration("20250411211346_AdicionaEstoque")]
    partial class AdicionaEstoque
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BancoDeSangue.Models.Doacao", b =>
                {
                    b.Property<int>("DoacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DoadorId")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeML")
                        .HasColumnType("int");

                    b.HasKey("DoacaoId");

                    b.HasIndex("DoadorId");

                    b.ToTable("Doacao");
                });

            modelBuilder.Entity("BancoDeSangue.Models.Doador", b =>
                {
                    b.Property<int>("DoadorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CpfDoador")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("TipoSanguineo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("UltimaDoacao")
                        .HasColumnType("datetime(6)");

                    b.HasKey("DoadorId");

                    b.ToTable("Doador");
                });

            modelBuilder.Entity("BancoDeSangue.Models.EstoqueDeSangue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("TotalABNegativo")
                        .HasColumnType("int");

                    b.Property<int>("TotalABPositivo")
                        .HasColumnType("int");

                    b.Property<int>("TotalANegativo")
                        .HasColumnType("int");

                    b.Property<int>("TotalAPositivo")
                        .HasColumnType("int");

                    b.Property<int>("TotalBNegativo")
                        .HasColumnType("int");

                    b.Property<int>("TotalBPositivo")
                        .HasColumnType("int");

                    b.Property<int>("TotalEstoque")
                        .HasColumnType("int");

                    b.Property<int>("TotalONegativo")
                        .HasColumnType("int");

                    b.Property<int>("TotalOPositivo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Estoques");
                });

            modelBuilder.Entity("BancoDeSangue.Models.Doacao", b =>
                {
                    b.HasOne("BancoDeSangue.Models.Doador", "Doador")
                        .WithMany()
                        .HasForeignKey("DoadorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doador");
                });
#pragma warning restore 612, 618
        }
    }
}
