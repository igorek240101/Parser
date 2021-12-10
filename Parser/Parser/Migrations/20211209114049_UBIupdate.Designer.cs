﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parser;

namespace Parser.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211209114049_UBIupdate")]
    partial class UBIupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.12");

            modelBuilder.Entity("Parser.Danger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AccessibilityViolation")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("BreachOfConfidentiality")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IntegrityViolation")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Object")
                        .HasColumnType("TEXT");

                    b.Property<string>("SourceOfThreat")
                        .HasColumnType("TEXT");

                    b.Property<int>("UBID")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Danger");
                });
#pragma warning restore 612, 618
        }
    }
}
