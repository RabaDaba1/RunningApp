using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RunningApp.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class Initial_with_data_test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Athletes",
                columns: new[] { "Id", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arek" },
                    { 2, new DateTime(1992, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kiptum" },
                    { 3, new DateTime(1994, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maciek" },
                    { 4, new DateTime(1996, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kacper" },
                    { 5, new DateTime(1998, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Przemek" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
