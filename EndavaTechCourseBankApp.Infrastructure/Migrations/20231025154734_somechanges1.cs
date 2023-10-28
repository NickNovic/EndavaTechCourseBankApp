using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndavaTechCourseBankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class somechanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceBuy",
                table: "currencies");

            migrationBuilder.RenameColumn(
                name: "PriceSell",
                table: "currencies",
                newName: "ChangeRate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChangeRate",
                table: "currencies",
                newName: "PriceSell");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceBuy",
                table: "currencies",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
