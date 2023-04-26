﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prokompetence.DAL.SqlServer;

#nullable disable

namespace Prokompetence.DAL.SqlServer.Migrations
{
    [DbContext(typeof(SqlServerProkompetenceDbContext))]
    partial class SqlServerProkompetenceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Prokompetence.DAL.Entities.KeyTechnology", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KeyTechnology");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.LifeScenario", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LifeScenario");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organization");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CuratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinalProject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOpened")
                        .HasColumnType("bit");

                    b.Property<int>("KeyTechnologyId")
                        .HasColumnType("int");

                    b.Property<int>("LifeScenarioId")
                        .HasColumnType("int");

                    b.Property<int>("MaxStudentsCountInTeam")
                        .HasColumnType("int");

                    b.Property<int>("MaxTeamsCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.HasIndex("KeyTechnologyId");

                    b.HasIndex("LifeScenarioId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Student"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Customer"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TeamLeadId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.TeamProjectRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamId");

                    b.ToTable("TeamProjectRecords");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Project", b =>
                {
                    b.HasOne("Prokompetence.DAL.Entities.User", "Curator")
                        .WithMany()
                        .HasForeignKey("CuratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prokompetence.DAL.Entities.KeyTechnology", "KeyTechnology")
                        .WithMany("Projects")
                        .HasForeignKey("KeyTechnologyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prokompetence.DAL.Entities.LifeScenario", "LifeScenario")
                        .WithMany("Projects")
                        .HasForeignKey("LifeScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prokompetence.DAL.Entities.Organization", "Organization")
                        .WithMany("Projects")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curator");

                    b.Navigation("KeyTechnology");

                    b.Navigation("LifeScenario");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.TeamProjectRecord", b =>
                {
                    b.HasOne("Prokompetence.DAL.Entities.Project", "Project")
                        .WithMany("Records")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prokompetence.DAL.Entities.Team", "Team")
                        .WithMany("Records")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.User", b =>
                {
                    b.HasOne("Prokompetence.DAL.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.KeyTechnology", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.LifeScenario", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Organization", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Project", b =>
                {
                    b.Navigation("Records");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Team", b =>
                {
                    b.Navigation("Records");
                });
#pragma warning restore 612, 618
        }
    }
}
