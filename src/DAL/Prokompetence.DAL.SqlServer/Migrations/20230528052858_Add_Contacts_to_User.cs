using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Prokompetence.DAL.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class Add_Contacts_to_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contacts",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contacts",
                table: "Users");
        }
    }
}
