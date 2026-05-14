using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace facturationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddMFClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatriculeFiscal",
                table: "Clients",
                type: "TEXT",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatriculeFiscal",
                table: "Clients");
        }
    }
}
