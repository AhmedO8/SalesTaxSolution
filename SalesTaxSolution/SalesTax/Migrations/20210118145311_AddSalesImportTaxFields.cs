using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesTax.Migrations
{
    public partial class AddSalesImportTaxFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tax",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "Categories",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "FinalProductPrice",
                table: "Product",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ImportTaxAmount",
                table: "Product",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SalesTaxAmount",
                table: "Product",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "FinalProductPrice",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ImportTaxAmount",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "SalesTaxAmount",
                table: "Product");

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
