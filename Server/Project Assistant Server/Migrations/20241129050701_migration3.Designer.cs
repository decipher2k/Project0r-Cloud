﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_Assistant_Server;

#nullable disable

namespace Project_Assistant_Server.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241129050701_migration3")]
    partial class migration3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Project_Assistant_Server.Models.Calendar", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("caption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("from")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("handled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("to")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("calendars");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.File", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("caption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("fileName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("startOnce")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("files");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.ItemPush", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("IsAccepted")
                        .HasColumnType("int");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("Project")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("ReceiverId")
                        .HasColumnType("bigint");

                    b.Property<long>("SenderId")
                        .HasColumnType("bigint");

                    b.Property<string>("SenderName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("itemPush");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Log", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("caption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Note", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("caption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("notes");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Program", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("caption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("executaleFile")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("startOnce")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("program");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("projects");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.ToDo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<string>("caption")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("priority")
                        .HasColumnType("int");

                    b.Property<int>("weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("toDo");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("CurrentSession")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Calendar", b =>
                {
                    b.HasOne("Project_Assistant_Server.Models.Project", null)
                        .WithMany("Calendars")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.File", b =>
                {
                    b.HasOne("Project_Assistant_Server.Models.Project", null)
                        .WithMany("Files")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Log", b =>
                {
                    b.HasOne("Project_Assistant_Server.Models.Project", null)
                        .WithMany("Logs")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Note", b =>
                {
                    b.HasOne("Project_Assistant_Server.Models.Project", null)
                        .WithMany("Notes")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Program", b =>
                {
                    b.HasOne("Project_Assistant_Server.Models.Project", null)
                        .WithMany("Programs")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Project", b =>
                {
                    b.HasOne("Project_Assistant_Server.Models.User", null)
                        .WithMany("Projects")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.ToDo", b =>
                {
                    b.HasOne("Project_Assistant_Server.Models.Project", null)
                        .WithMany("ToDo")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.Project", b =>
                {
                    b.Navigation("Calendars");

                    b.Navigation("Files");

                    b.Navigation("Logs");

                    b.Navigation("Notes");

                    b.Navigation("Programs");

                    b.Navigation("ToDo");
                });

            modelBuilder.Entity("Project_Assistant_Server.Models.User", b =>
                {
                    b.Navigation("Projects");
                });
#pragma warning restore 612, 618
        }
    }
}
