using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prokompetence.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Make_team_role_not_required : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentsInTeam_TeamRoles_RoleId",
                table: "StudentsInTeam");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "StudentsInTeam",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "StudentsInTeam",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
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
