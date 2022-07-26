using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddConversions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Conversions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromWarehouseId = table.Column<int>(type: "int", nullable: false),
                    ToWarehouseId = table.Column<int>(type: "int", nullable: false),
                    FromProductId = table.Column<int>(type: "int", nullable: false),
                    ToProductId = table.Column<int>(type: "int", nullable: false),
                    FromPlaceId = table.Column<int>(type: "int", nullable: false),
                    ToPlaceId = table.Column<int>(type: "int", nullable: false),
                    FromQuantity = table.Column<int>(type: "int", nullable: false),
                    ToQuantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExportInvoiceId = table.Column<int>(type: "int", nullable: false),
                    ImportInvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversions_Invoices_ExportInvoiceId",
                        column: x => x.ExportInvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversions_Invoices_ImportInvoiceId",
                        column: x => x.ImportInvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversions_Products_FromProductId",
                        column: x => x.FromProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversions_Products_ToProductId",
                        column: x => x.ToProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversions_StoragePlaces_FromPlaceId",
                        column: x => x.FromPlaceId,
                        principalTable: "StoragePlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversions_StoragePlaces_ToPlaceId",
                        column: x => x.ToPlaceId,
                        principalTable: "StoragePlaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversions_Warehouses_FromWarehouseId",
                        column: x => x.FromWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Conversions_Warehouses_ToWarehouseId",
                        column: x => x.ToWarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_ExportInvoiceId",
                table: "Conversions",
                column: "ExportInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_FromPlaceId",
                table: "Conversions",
                column: "FromPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_FromProductId",
                table: "Conversions",
                column: "FromProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_FromWarehouseId",
                table: "Conversions",
                column: "FromWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_ImportInvoiceId",
                table: "Conversions",
                column: "ImportInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_ToPlaceId",
                table: "Conversions",
                column: "ToPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_ToProductId",
                table: "Conversions",
                column: "ToProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversions_ToWarehouseId",
                table: "Conversions",
                column: "ToWarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Conversions");

            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "Invoices");
        }
    }
}
