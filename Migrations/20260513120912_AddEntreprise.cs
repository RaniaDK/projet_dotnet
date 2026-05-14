using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace facturationApp.Migrations
{
    /// <inheritdoc />
    public partial class AddEntreprise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entreprises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nom = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Adresse = table.Column<string>(type: "TEXT", maxLength: 250, nullable: true),
                    Telephone = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    MatriculeFiscal = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CodePostal = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Ville = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entreprises", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entreprises");
        }
    }
}
