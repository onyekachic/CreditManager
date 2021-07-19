using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class CreditDataUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credits",
                columns: table => new
                {
                    CreditID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreditNo = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    GTotal = table.Column<decimal>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CustomerID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credits", x => x.CreditID);
                    table.ForeignKey(
                        name: "FK_Credits_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditPayItems",
                columns: table => new
                {
                    CreditPayItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RepayAmt = table.Column<int>(type: "INTEGER", nullable: false),
                    Pension = table.Column<int>(type: "INTEGER", nullable: false),
                    Union = table.Column<int>(type: "INTEGER", nullable: false),
                    School = table.Column<int>(type: "INTEGER", nullable: false),
                    Others = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreditID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditPayItems", x => x.CreditPayItemID);
                    table.ForeignKey(
                        name: "FK_CreditPayItems_Credits_CreditID",
                        column: x => x.CreditID,
                        principalTable: "Credits",
                        principalColumn: "CreditID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditPayItems_CreditID",
                table: "CreditPayItems",
                column: "CreditID");

            migrationBuilder.CreateIndex(
                name: "IX_Credits_CustomerID",
                table: "Credits",
                column: "CustomerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditPayItems");

            migrationBuilder.DropTable(
                name: "Credits");
        }
    }
}
