using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RenameInvoiceTypeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductMovementType",
                table: "Movements",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "InvoiceType",
                table: "Invoices",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "InvoiceStatus",
                table: "Invoices",
                newName: "Status");

            migrationBuilder.AlterColumn<int>(
                name: "Key",
                table: "CurrencyAmounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Movements",
                newName: "ProductMovementType");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Invoices",
                newName: "InvoiceType");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Invoices",
                newName: "InvoiceStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "CurrencyAmounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
