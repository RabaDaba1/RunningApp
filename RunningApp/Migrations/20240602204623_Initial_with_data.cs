using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RunningApp.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class Initial_with_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Results",
                newName: "AthleteId");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Date", "Distance", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "19.08.2023", "10 km", "Warszawa", "Warszawska Dycha" },
                    { 2, "29.08.2023", "21 km", "Warszawa", "Półmaraton Warszawski" },
                    { 3, "31.06.2022", "42 km", "Warszawa", "Maraton Krakowski" },
                    { 4, "09.07.2021", "5 km", "Warszawa", "Zabawna Piątka" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Results_AthleteId",
                table: "Results",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_EventId",
                table: "Results",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Athletes_AthleteId",
                table: "Results",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Events_EventId",
                table: "Results",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Athletes_AthleteId",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Events_EventId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_AthleteId",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Results_EventId",
                table: "Results");

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "AthleteId",
                table: "Results",
                newName: "UserId");
        }
    }
}
