using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prokompetence.DAL.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Add_AcademicGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AcademicGroup",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcademicGroup",
                table: "Users");
        }
    }
}
