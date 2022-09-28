using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelsDb.Migrations
{
    public partial class client_foreign_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_clients_ClientId",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "accounts",
                newName: "client_id");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_ClientId",
                table: "accounts",
                newName: "IX_accounts_client_id");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_clients_client_id",
                table: "accounts",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_accounts_clients_client_id",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "accounts",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_client_id",
                table: "accounts",
                newName: "IX_accounts_ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_accounts_clients_ClientId",
                table: "accounts",
                column: "ClientId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
