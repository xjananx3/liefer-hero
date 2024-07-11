using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LieferHero.Migrations
{
    /// <inheritdoc />
    public partial class InitialMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Bestellungen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BestellZeit = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bestellungen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeiseHinzufuegenAnfragen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SpeiseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Menge = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeiseHinzufuegenAnfragen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Speisen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    ErstelltAm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speisen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeisenInBestellung",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SpeiseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Menge = table.Column<int>(type: "INTEGER", nullable: false),
                    BestellungId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeisenInBestellung", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeisenInBestellung_Bestellungen_BestellungId",
                        column: x => x.BestellungId,
                        principalTable: "Bestellungen",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpeisenInBestellung_Speisen_SpeiseId",
                        column: x => x.SpeiseId,
                        principalTable: "Speisen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpeisenInBestellung_BestellungId",
                table: "SpeisenInBestellung",
                column: "BestellungId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeisenInBestellung_SpeiseId",
                table: "SpeisenInBestellung",
                column: "SpeiseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AufgegebeneBesellungen");

            migrationBuilder.DropTable(
                name: "SpeiseHinzufuegenAnfragen");

            migrationBuilder.DropTable(
                name: "SpeisenInBestellung");

            migrationBuilder.DropTable(
                name: "Bestellungen");

            migrationBuilder.DropTable(
                name: "Speisen");
        }
    }
}
