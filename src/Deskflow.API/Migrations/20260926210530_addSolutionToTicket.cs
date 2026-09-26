using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deskflow.API.Migrations
{
    /// <inheritdoc />
    public partial class addSolutionToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Solution",
                table: "Tickets",
                type: "nvarchar(500)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Solution",
                table: "Tickets");
        }
    }
}
