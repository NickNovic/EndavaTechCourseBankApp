using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EndavaTechCourseBankApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChangeRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeRate",
                table: "wallets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ChangeRate",
                table: "wallets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
