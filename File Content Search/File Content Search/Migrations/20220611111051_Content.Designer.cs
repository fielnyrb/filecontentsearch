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
    [Migration("20220611111051_Content")]
    partial class Content
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("File_Content_Search.Entities.LibraryItem", b =>
                {
                    b.Property<long>("LibraryItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("LibraryItemId");

                    b.ToTable("LibraryItems", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
