using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddStoragePlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoragePlaces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    ContainerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoragePlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoragePlaces_StoragePlaces_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "StoragePlaces",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoragePlaces_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlaces_ContainerId",
                table: "StoragePlaces",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_StoragePlaces_WarehouseId",
                table: "StoragePlaces",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoragePlaces");
        }
    }
}
