﻿// <auto-generated />
using System;
using CompanyManagmentApplication.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyManagmentApplication.Migrations
{
    [DbContext(typeof(Data))]
    [Migration("20240909193313_migra12")]
    partial class migra12
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyManagmentApplication.Models.Message_Replay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Replay_text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("message_id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("message_id");

                    b.ToTable("message_Replays");
                });

            modelBuilder.Entity("CompanyManagmentApplication.Models.Messages", b =>
                {
                    b.Property<int>("message_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("message_id"));

                    b.Property<string>("from_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("message_object")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("to_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("message_id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("CompanyManagmentApplication.Models.Products", b =>
                {
                    b.Property<int>("product_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("product_id"));

                    b.Property<string>("product_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("product_verify")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("product_id");

                    b.HasIndex("user_id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CompanyManagmentApplication.Models.Role", b =>
                {
                    b.Property<int>("role_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("role_id"));

                    b.Property<string>("role_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("role_id");

                    b.ToTable("roles");
                });

            modelBuilder.Entity("CompanyManagmentApplication.Models.Users", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"));

                    b.Property<int?>("role_id")
                        .HasColumnType("int");

                    b.Property<string>("user_address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.HasIndex("role_id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("CompanyManagmentApplication.Models.Message_Replay", b =>
                {
                    b.HasOne("CompanyManagmentApplication.Models.Messages", "Messages")
                        .WithMany()
                        .HasForeignKey("message_id");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("CompanyManagmentApplication.Models.Products", b =>
                {
                    b.HasOne("CompanyManagmentApplication.Models.Users", "Users")
                        .WithMany()
                        .HasForeignKey("user_id");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("CompanyManagmentApplication.Models.Users", b =>
                {
                    b.HasOne("CompanyManagmentApplication.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("role_id");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
