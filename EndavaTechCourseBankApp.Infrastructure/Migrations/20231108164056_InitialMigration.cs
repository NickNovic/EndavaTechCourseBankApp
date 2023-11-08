using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EndavaTechCourseBankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("5fab41a2-d273-49c3-aa17-04ac841d3496"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("caac8ece-6eb7-4824-bb49-f7d0e5c6abe2"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("6ff2137a-fd53-429e-8daa-92fcb7a92c48"), null, "Admin", "ADMIN" },
                    { new Guid("c4775ab7-78d3-4d04-992a-ebbdd1db3936"), null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6ff2137a-fd53-429e-8daa-92fcb7a92c48"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c4775ab7-78d3-4d04-992a-ebbdd1db3936"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("5fab41a2-d273-49c3-aa17-04ac841d3496"), null, "User", "USER" },
                    { new Guid("caac8ece-6eb7-4824-bb49-f7d0e5c6abe2"), null, "Admin", "ADMIN" }
                });
        }
    }
}
