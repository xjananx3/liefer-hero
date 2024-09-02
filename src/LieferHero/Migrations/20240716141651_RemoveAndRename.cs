using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LieferHero.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAndRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AufgegebeneBesellungen");

            migrationBuilder.DropTable(
                name: "SpeiseHinzufuegenAnfragen");

            migrationBuilder.CreateTable(
                name: "AufgegebeneBestellungen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GesamtKosten = table.Column<decimal>(type: "TEXT", nullable: false),
                    Nachricht = table.Column<string>(type: "TEXT", nullable: false),
                    BestellungId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AufgegebeneBestellungen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AufgegebeneBestellungen_Bestellungen_BestellungId",
                        column: x => x.BestellungId,
                        principalTable: "Bestellungen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AufgegebeneBestellungen_BestellungId",
                table: "AufgegebeneBestellungen",
                column: "BestellungId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AufgegebeneBestellungen");

            migrationBuilder.CreateTable(
                name: "AufgegebeneBesellungen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GesamtKosten = table.Column<decimal>(type: "TEXT", nullable: false),
                    Nachricht = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AufgegebeneBesellungen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeiseHinzufuegenAnfragen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Menge = table.Column<int>(type: "INTEGER", nullable: false),
                    SpeiseId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeiseHinzufuegenAnfragen", x => x.Id);
                });
        }
    }
}
