﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPFINALLAB4MVC.Models;

#nullable disable

namespace TPFINALLAB4MVC.Migrations
{
    [DbContext(typeof(AppDbContexto))]
    [Migration("20231027194703_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TPFINALLAB4MVC.Models.Estado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("estados");
                });

            modelBuilder.Entity("TPFINALLAB4MVC.Models.Jugador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("edad")
                        .HasColumnType("int");

                    b.Property<string>("nickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("jugadores");
                });

            modelBuilder.Entity("TPFINALLAB4MVC.Models.Partido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdEstado")
                        .HasColumnType("int");

                    b.Property<int>("IdJugador")
                        .HasColumnType("int");

                    b.Property<string>("NickNameRival")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("estadoId")
                        .HasColumnType("int");

                    b.Property<string>("fecha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("jugadorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("estadoId");

                    b.HasIndex("jugadorId");

                    b.ToTable("partidos");
                });

            modelBuilder.Entity("TPFINALLAB4MVC.Models.PartidoDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdPartido")
                        .HasColumnType("int");

                    b.Property<int?>("PartidoId")
                        .HasColumnType("int");

                    b.Property<int>("cantAmarillas")
                        .HasColumnType("int");

                    b.Property<int>("cantRojas")
                        .HasColumnType("int");

                    b.Property<int>("golesJugador")
                        .HasColumnType("int");

                    b.Property<int>("golesRival")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PartidoId");

                    b.ToTable("partidosDetalles");
                });

            modelBuilder.Entity("TPFINALLAB4MVC.Models.Partido", b =>
                {
                    b.HasOne("TPFINALLAB4MVC.Models.Estado", "estado")
                        .WithMany()
                        .HasForeignKey("estadoId");

                    b.HasOne("TPFINALLAB4MVC.Models.Jugador", "jugador")
                        .WithMany()
                        .HasForeignKey("jugadorId");

                    b.Navigation("estado");

                    b.Navigation("jugador");
                });

            modelBuilder.Entity("TPFINALLAB4MVC.Models.PartidoDetalle", b =>
                {
                    b.HasOne("TPFINALLAB4MVC.Models.Partido", "Partido")
                        .WithMany()
                        .HasForeignKey("PartidoId");

                    b.Navigation("Partido");
                });
#pragma warning restore 612, 618
        }
    }
}
