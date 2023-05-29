using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Prokompetence.DAL.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Sqlserver_to_postgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsInTeam_TeamRoles_RoleId",
                table: "StudentsInTeam");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Projects",
                newName: "Target");

            migrationBuilder.RenameColumn(
                name: "FinalProject",
                table: "Projects",
                newName: "Stack");

            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "StudentsInTeam",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "ExpectedResults",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicationProperties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationProperties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Iterations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iterations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentRatingsInProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IterationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRatingsInProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamInvitations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamInvitations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamRatingsInProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    IterationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRatingsInProject", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsInTeam_TeamRoles_RoleId",
                table: "StudentsInTeam",
                column: "RoleId",
                principalTable: "TeamRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsInTeam_TeamRoles_RoleId",
                table: "StudentsInTeam");

            migrationBuilder.DropTable(
                name: "ApplicationProperties");

            migrationBuilder.DropTable(
                name: "Iterations");

            migrationBuilder.DropTable(
                name: "StudentRatingsInProject");

            migrationBuilder.DropTable(
                name: "TeamInvitations");

            migrationBuilder.DropTable(
                name: "TeamRatingsInProject");

            migrationBuilder.DropColumn(
                name: "Contacts",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExpectedResults",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Target",
                table: "Projects",
                newName: "ShortDescription");

            migrationBuilder.RenameColumn(
                name: "Stack",
                table: "Projects",
                newName: "FinalProject");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "StudentsInTeam",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentsInTeam_TeamRoles_RoleId",
                table: "StudentsInTeam",
                column: "RoleId",
                principalTable: "TeamRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
