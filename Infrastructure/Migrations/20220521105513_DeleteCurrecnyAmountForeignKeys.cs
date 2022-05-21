using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class DeleteCurrecnyAmountForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_Movements_ObjectId",
                table: "CurrencyAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_Payments_ObjectId",
                table: "CurrencyAmounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyAmounts_ObjectId",
                table: "CurrencyAmounts");

            migrationBuilder.AddColumn<int>(
                name: "PaymentDbId",
                table: "CurrencyAmounts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductMovementDbId",
                table: "CurrencyAmounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmounts_PaymentDbId",
                table: "CurrencyAmounts",
                column: "PaymentDbId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmounts_ProductMovementDbId",
                table: "CurrencyAmounts",
                column: "ProductMovementDbId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_Movements_ProductMovementDbId",
                table: "CurrencyAmounts",
                column: "ProductMovementDbId",
                principalTable: "Movements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_Payments_PaymentDbId",
                table: "CurrencyAmounts",
                column: "PaymentDbId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_Movements_ProductMovementDbId",
                table: "CurrencyAmounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyAmounts_Payments_PaymentDbId",
                table: "CurrencyAmounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyAmounts_PaymentDbId",
                table: "CurrencyAmounts");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyAmounts_ProductMovementDbId",
                table: "CurrencyAmounts");

            migrationBuilder.DropColumn(
                name: "PaymentDbId",
                table: "CurrencyAmounts");

            migrationBuilder.DropColumn(
                name: "ProductMovementDbId",
                table: "CurrencyAmounts");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyAmounts_ObjectId",
                table: "CurrencyAmounts",
                column: "ObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_Movements_ObjectId",
                table: "CurrencyAmounts",
                column: "ObjectId",
                principalTable: "Movements",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyAmounts_Payments_ObjectId",
                table: "CurrencyAmounts",
                column: "ObjectId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
