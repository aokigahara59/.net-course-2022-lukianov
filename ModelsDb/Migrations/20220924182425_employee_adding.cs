using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelsDb.Migrations
{
    public partial class employee_adding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    passport_id = table.Column<int>(type: "integer", nullable: false),
                    bonus = table.Column<int>(type: "integer", nullable: false),
                    contract = table.Column<string>(type: "text", nullable: false),
                    salary = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
