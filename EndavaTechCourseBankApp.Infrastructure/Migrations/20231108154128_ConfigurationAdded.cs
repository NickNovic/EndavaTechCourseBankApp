using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EndavaTechCourseBankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("87079b3c-66b4-447a-89ad-acd13434464b"), null, "Admin", null },
                    { new Guid("d53c4ca7-c6a6-41d5-91ab-1880a9f1cd67"), null, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("87079b3c-66b4-447a-89ad-acd13434464b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d53c4ca7-c6a6-41d5-91ab-1880a9f1cd67"));
        }
    }
}
