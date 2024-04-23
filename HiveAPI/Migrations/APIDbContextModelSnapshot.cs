﻿// <auto-generated />
using System;
using HiveAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HiveAPI.Migrations
{
    [DbContext(typeof(APIDbContext))]
    partial class APIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HiveAPI.Modals.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HiveAPI.Models.Collaborator", b =>
                {
                    b.Property<int>("CollaboratorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollaboratorId"));

                    b.Property<int?>("TaskId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkSpaceId")
                        .HasColumnType("int");

                    b.HasKey("CollaboratorId");

                    b.HasIndex("TaskId");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("Collaborators");
                });

            modelBuilder.Entity("HiveAPI.Models.List", b =>
                {
                    b.Property<int>("ListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ListId"));

                    b.Property<string>("ListName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkSpaceId")
                        .HasColumnType("int");

                    b.HasKey("ListId");

                    b.HasIndex("WorkSpaceId");

                    b.ToTable("List");
                });

            modelBuilder.Entity("HiveAPI.Models.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ListId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaskDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskId");

                    b.HasIndex("ListId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("HiveAPI.Models.WorkSpace", b =>
                {
                    b.Property<int>("WId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WId"));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("WorkspaceDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkspaceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WId");

                    b.HasIndex("UserId");

                    b.ToTable("WorkSpaces");
                });

            modelBuilder.Entity("HiveAPI.Models.Collaborator", b =>
                {
                    b.HasOne("HiveAPI.Models.Task", null)
                        .WithMany("Collaborators")
                        .HasForeignKey("TaskId");

                    b.HasOne("HiveAPI.Models.WorkSpace", "WorkSpace")
                        .WithMany()
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkSpace");
                });

            modelBuilder.Entity("HiveAPI.Models.List", b =>
                {
                    b.HasOne("HiveAPI.Models.WorkSpace", "WorkSpace")
                        .WithMany()
                        .HasForeignKey("WorkSpaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WorkSpace");
                });

            modelBuilder.Entity("HiveAPI.Models.Task", b =>
                {
                    b.HasOne("HiveAPI.Models.List", "list")
                        .WithMany()
                        .HasForeignKey("ListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("list");
                });

            modelBuilder.Entity("HiveAPI.Models.WorkSpace", b =>
                {
                    b.HasOne("HiveAPI.Modals.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HiveAPI.Models.Task", b =>
                {
                    b.Navigation("Collaborators");
                });
#pragma warning restore 612, 618
        }
    }
}
