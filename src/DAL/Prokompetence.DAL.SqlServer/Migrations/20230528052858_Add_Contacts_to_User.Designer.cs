﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Prokompetence.DAL.SqlServer;

#nullable disable

namespace Prokompetence.DAL.SqlServer.Migrations
{
    [DbContext(typeof(SqlServerProkompetenceDbContext))]
    [Migration("20230528052858_Add_Contacts_to_User")]
    partial class Add_Contacts_to_User
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Prokompetence.DAL.Entities.ApplicationProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationProperties");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Iteration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Iterations");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.KeyTechnology", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KeyTechnologies");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.LifeScenario", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LifeScenarios");
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

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Complexity")
                        .HasColumnType("int");

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

                    b.Property<string>("Stack")
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

            modelBuilder.Entity("Prokompetence.DAL.Entities.StudentInTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsTeamLead")
                        .HasColumnType("bit");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeamId");

                    b.ToTable("StudentsInTeam");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.StudentRatingInProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IterationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("StudentRatingsInProject");
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

                    b.HasIndex("TeamLeadId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.TeamInvitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("TeamInvitations");
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

            modelBuilder.Entity("Prokompetence.DAL.Entities.TeamRatingInProject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IterationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("TeamRatingsInProject");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.TeamRole", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TeamRoles");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Contacts")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
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

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.UserOrganizationAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserOrganizationAccesses");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
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

            modelBuilder.Entity("Prokompetence.DAL.Entities.StudentInTeam", b =>
                {
                    b.HasOne("Prokompetence.DAL.Entities.TeamRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("Prokompetence.DAL.Entities.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prokompetence.DAL.Entities.Team", "Team")
                        .WithMany("Students")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Student");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.Team", b =>
                {
                    b.HasOne("Prokompetence.DAL.Entities.User", "TeamLead")
                        .WithMany()
                        .HasForeignKey("TeamLeadId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TeamLead");
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

            modelBuilder.Entity("Prokompetence.DAL.Entities.UserRole", b =>
                {
                    b.HasOne("Prokompetence.DAL.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Prokompetence.DAL.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
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

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Prokompetence.DAL.Entities.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}