using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prokompetence.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Add_students_to_team : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_KeyTechnology_KeyTechnologyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_LifeScenario_LifeScenarioId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organization_OrganizationId",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organization",
                table: "Organization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LifeScenario",
                table: "LifeScenario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeyTechnology",
                table: "KeyTechnology");

            migrationBuilder.RenameTable(
                name: "Organization",
                newName: "Organizations");

            migrationBuilder.RenameTable(
                name: "LifeScenario",
                newName: "LifeScenarios");

            migrationBuilder.RenameTable(
                name: "KeyTechnology",
                newName: "KeyTechnologies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LifeScenarios",
                table: "LifeScenarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeyTechnologies",
                table: "KeyTechnologies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TeamRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentsInTeam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsTeamLead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsInTeam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsInTeam_TeamRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TeamRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsInTeam_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsInTeam_Users_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsInTeam_RoleId",
                table: "StudentsInTeam",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsInTeam_StudentId",
                table: "StudentsInTeam",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsInTeam_TeamId",
                table: "StudentsInTeam",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_KeyTechnologies_KeyTechnologyId",
                table: "Projects",
                column: "KeyTechnologyId",
                principalTable: "KeyTechnologies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_LifeScenarios_LifeScenarioId",
                table: "Projects",
                column: "LifeScenarioId",
                principalTable: "LifeScenarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_KeyTechnologies_KeyTechnologyId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_LifeScenarios_LifeScenarioId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Organizations_OrganizationId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "StudentsInTeam");

            migrationBuilder.DropTable(
                name: "TeamRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Organizations",
                table: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LifeScenarios",
                table: "LifeScenarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeyTechnologies",
                table: "KeyTechnologies");

            migrationBuilder.RenameTable(
                name: "Organizations",
                newName: "Organization");

            migrationBuilder.RenameTable(
                name: "LifeScenarios",
                newName: "LifeScenario");

            migrationBuilder.RenameTable(
                name: "KeyTechnologies",
                newName: "KeyTechnology");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Organization",
                table: "Organization",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LifeScenario",
                table: "LifeScenario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeyTechnology",
                table: "KeyTechnology",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_KeyTechnology_KeyTechnologyId",
                table: "Projects",
                column: "KeyTechnologyId",
                principalTable: "KeyTechnology",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_LifeScenario_LifeScenarioId",
                table: "Projects",
                column: "LifeScenarioId",
                principalTable: "LifeScenario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Organization_OrganizationId",
                table: "Projects",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
