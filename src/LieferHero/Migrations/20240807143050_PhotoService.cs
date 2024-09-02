using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LieferHero.Migrations
{
    /// <inheritdoc />
    public partial class PhotoService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Speisen",
                newName: "Preis");

            migrationBuilder.AddColumn<string>(
                name: "Beschreibung",
                table: "Speisen",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bild",
                table: "Speisen",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beschreibung",
                table: "Speisen");

            migrationBuilder.DropColumn(
                name: "Bild",
                table: "Speisen");

            migrationBuilder.RenameColumn(
                name: "Preis",
                table: "Speisen",
                newName: "Price");
        }
    }
}
