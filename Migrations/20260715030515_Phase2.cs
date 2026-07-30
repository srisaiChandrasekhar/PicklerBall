using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Picklr.Migrations
{
    /// <inheritdoc />
    public partial class Phase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableDays",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClubID",
                table: "Programs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProgramID = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalTable: "Programs",
                        principalColumn: "ProgramID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 1,
                columns: new[] { "AvailableDays", "ClubID" },
                values: new object[] { 21, 1 });

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 2,
                columns: new[] { "AvailableDays", "ClubID" },
                values: new object[] { 10, 1 });

            migrationBuilder.UpdateData(
                table: "Programs",
                keyColumn: "ProgramID",
                keyValue: 3,
                columns: new[] { "AvailableDays", "ClubID" },
                values: new object[] { 96, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ClubID",
                table: "Programs",
                column: "ClubID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ProgramID",
                table: "Reservations",
                column: "ProgramID");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Clubs_ClubID",
                table: "Programs",
                column: "ClubID",
                principalTable: "Clubs",
                principalColumn: "ClubID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Clubs_ClubID",
                table: "Programs");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ClubID",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "AvailableDays",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ClubID",
                table: "Programs");
        }
    }
}
