﻿// <auto-generated />
using System;
using System.Collections.Generic;
using MediportaTask.ContextDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MediportaTask.Migrations
{
    [DbContext(typeof(TagDbContext))]
    [Migration("20240404000813_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MediportaTask.Model.Collective", b =>
                {
                    b.Property<int>("CollectiveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CollectiveId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("TagId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("Tags")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.HasKey("CollectiveId");

                    b.HasIndex("TagId");

                    b.ToTable("Collectives");
                });

            modelBuilder.Entity("MediportaTask.Model.ExternalLink", b =>
                {
                    b.Property<int>("LinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LinkId"));

                    b.Property<int?>("CollectiveId")
                        .HasColumnType("integer");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LinkId");

                    b.HasIndex("CollectiveId");

                    b.ToTable("ExternalLinks");
                });

            modelBuilder.Entity("MediportaTask.Model.StackOverflowResponse", b =>
                {
                    b.Property<int>("ResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ResponseId"));

                    b.Property<bool>("HasMore")
                        .HasColumnType("boolean");

                    b.Property<int>("QuotaMax")
                        .HasColumnType("integer");

                    b.Property<int>("QuotaRemaining")
                        .HasColumnType("integer");

                    b.HasKey("ResponseId");

                    b.ToTable("StackOverflowResponses");
                });

            modelBuilder.Entity("MediportaTask.Model.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TagId"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<bool>("HasSynonyms")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsModeratorOnly")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Percentage")
                        .HasColumnType("double precision");

                    b.Property<int?>("StackOverflowResponseResponseId")
                        .HasColumnType("integer");

                    b.HasKey("TagId");

                    b.HasIndex("StackOverflowResponseResponseId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MediportaTask.Model.Collective", b =>
                {
                    b.HasOne("MediportaTask.Model.Tag", null)
                        .WithMany("Collectives")
                        .HasForeignKey("TagId");
                });

            modelBuilder.Entity("MediportaTask.Model.ExternalLink", b =>
                {
                    b.HasOne("MediportaTask.Model.Collective", null)
                        .WithMany("External_Links")
                        .HasForeignKey("CollectiveId");
                });

            modelBuilder.Entity("MediportaTask.Model.Tag", b =>
                {
                    b.HasOne("MediportaTask.Model.StackOverflowResponse", null)
                        .WithMany("Items")
                        .HasForeignKey("StackOverflowResponseResponseId");
                });

            modelBuilder.Entity("MediportaTask.Model.Collective", b =>
                {
                    b.Navigation("External_Links");
                });

            modelBuilder.Entity("MediportaTask.Model.StackOverflowResponse", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("MediportaTask.Model.Tag", b =>
                {
                    b.Navigation("Collectives");
                });
#pragma warning restore 612, 618
        }
    }
}