﻿// <auto-generated />
using System;
using DatabaseManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseManager.Migrations
{
    [DbContext(typeof(SQLiteDbContext))]
    [Migration("20201209193404_Added flags for Book and BookBundle to Soft Delete")]
    partial class AddedflagsforBookandBookBundletoSoftDelete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DatabaseManager.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Catalogs");
                });

            modelBuilder.Entity("DatabaseManager.Models.BookBundle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("DatabaseManager.Models.BookOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookBundleId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookBundleId");

                    b.HasIndex("EventId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DatabaseManager.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventType")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PlacedDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("DatabaseManager.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AdminPermission")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DatabaseManager.Models.BookBundle", b =>
                {
                    b.HasOne("DatabaseManager.Models.Book", "Book")
                        .WithMany("BookBundles")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("DatabaseManager.Models.BookOrder", b =>
                {
                    b.HasOne("DatabaseManager.Models.BookBundle", "BookBundle")
                        .WithMany()
                        .HasForeignKey("BookBundleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseManager.Models.Event", null)
                        .WithMany("OrderedBooks")
                        .HasForeignKey("EventId");

                    b.Navigation("BookBundle");
                });

            modelBuilder.Entity("DatabaseManager.Models.Event", b =>
                {
                    b.HasOne("DatabaseManager.Models.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DatabaseManager.Models.Book", b =>
                {
                    b.Navigation("BookBundles");
                });

            modelBuilder.Entity("DatabaseManager.Models.Event", b =>
                {
                    b.Navigation("OrderedBooks");
                });

            modelBuilder.Entity("DatabaseManager.Models.User", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
