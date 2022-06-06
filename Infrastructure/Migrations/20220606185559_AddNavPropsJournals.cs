using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddNavPropsJournals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JournalDbs_AccountId",
                table: "JournalDbs",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalDbs_CurrencyId",
                table: "JournalDbs",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalDbs_SourceAccountId",
                table: "JournalDbs",
                column: "SourceAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalDbs_Accounts_AccountId",
                table: "JournalDbs",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalDbs_Accounts_SourceAccountId",
                table: "JournalDbs",
                column: "SourceAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalDbs_Currencies_CurrencyId",
                table: "JournalDbs",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalDbs_Accounts_AccountId",
                table: "JournalDbs");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalDbs_Accounts_SourceAccountId",
                table: "JournalDbs");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalDbs_Currencies_CurrencyId",
                table: "JournalDbs");

            migrationBuilder.DropIndex(
                name: "IX_JournalDbs_AccountId",
                table: "JournalDbs");

            migrationBuilder.DropIndex(
                name: "IX_JournalDbs_CurrencyId",
                table: "JournalDbs");

            migrationBuilder.DropIndex(
                name: "IX_JournalDbs_SourceAccountId",
                table: "JournalDbs");
        }
    }
}
