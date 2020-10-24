﻿// <auto-generated />
using DatabaseManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DatabaseManager.Migrations
{
    [DbContext(typeof(SQLiteDBContext))]
    [Migration("20201024105739_DatabaseCreate")]
    partial class DatabaseCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("DatabaseManager.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}