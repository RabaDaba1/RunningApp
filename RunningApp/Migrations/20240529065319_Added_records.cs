using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RunningApp.Migrations
{
    /// <inheritdoc />
    public partial class Added_records : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Id", "EventId", "Time", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new TimeSpan(0, 0, 25, 30, 0), 1 },
                    { 2, 1, new TimeSpan(0, 0, 24, 10, 0), 2 },
                    { 3, 2, new TimeSpan(0, 0, 30, 45, 0), 3 },
                    { 4, 2, new TimeSpan(0, 0, 27, 15, 0), 4 },
                    { 5, 3, new TimeSpan(0, 0, 29, 50, 0), 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
