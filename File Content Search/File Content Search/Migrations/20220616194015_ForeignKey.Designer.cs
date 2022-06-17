﻿// <auto-generated />
using File_Content_Search.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace File_Content_Search.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20220616194015_ForeignKey")]
    partial class ForeignKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("File_Content_Search.Entities.Library", b =>
                {
                    b.Property<long>("LibraryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LibraryId");

                    b.ToTable("Libraries", (string)null);
                });

            modelBuilder.Entity("File_Content_Search.Entities.LibraryItem", b =>
                {
                    b.Property<long>("LibraryItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("LibraryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LibraryItemId");

                    b.ToTable("LibraryItems", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}