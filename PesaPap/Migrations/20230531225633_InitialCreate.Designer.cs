﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PesaPap.Data;

#nullable disable

namespace PesaPap.Migrations
{
    [DbContext(typeof(PaymentsDbContext))]
    [Migration("20230531225633_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PesaPap.Entities.IPNSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InstitutionInsititutionId")
                        .HasColumnType("int");

                    b.Property<string>("NotificationChannel")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NotificationUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ValidationUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionInsititutionId");

                    b.ToTable("IPNSettings");
                });

            modelBuilder.Entity("PesaPap.Entities.Institution", b =>
                {
                    b.Property<int>("InsititutionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InsititutionId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("InstitutionName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("InsititutionId");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("PesaPap.Entities.PaymentChannels", b =>
                {
                    b.Property<int>("ChannelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChannelId"));

                    b.Property<string>("ChannelName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ChannelId");

                    b.ToTable("PaymentChannels");
                });

            modelBuilder.Entity("PesaPap.Entities.PaymentNotifications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTimeSent")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IPNSent")
                        .HasColumnType("bit");

                    b.Property<int>("InstitutionInsititutionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastIPNRetryDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("NoOfTries")
                        .HasColumnType("int");

                    b.Property<string>("PayerName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentNarration")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StudentNo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TransactionRef")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("InstitutionInsititutionId");

                    b.ToTable("PaymentNotifications");
                });

            modelBuilder.Entity("PesaPap.Entities.IPNSettings", b =>
                {
                    b.HasOne("PesaPap.Entities.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionInsititutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institution");
                });

            modelBuilder.Entity("PesaPap.Entities.PaymentNotifications", b =>
                {
                    b.HasOne("PesaPap.Entities.PaymentChannels", "Channel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PesaPap.Entities.Institution", "Institution")
                        .WithMany()
                        .HasForeignKey("InstitutionInsititutionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Channel");

                    b.Navigation("Institution");
                });
#pragma warning restore 612, 618
        }
    }
}
