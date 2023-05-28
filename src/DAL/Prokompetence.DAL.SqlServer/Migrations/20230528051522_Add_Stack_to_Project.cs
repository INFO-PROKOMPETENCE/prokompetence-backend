using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prokompetence.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Add_Stack_to_Project : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Projects",
                newName: "Stack");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stack",
                table: "Projects",
                newName: "ShortDescription");
        }
    }
}
