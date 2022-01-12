using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGL.Migrations
{
    public partial class DimDates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryCondition_Category_CategoryId",
                table: "CategoryCondition");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadTable_Account_AccountId",
                table: "LoadTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Account_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Category_CategoryId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryCondition",
                table: "CategoryCondition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "CategoryCondition",
                newName: "CategoryConditions");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryCondition_CategoryId",
                table: "CategoryConditions",
                newName: "IX_CategoryConditions_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryConditions",
                table: "CategoryConditions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DimDates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthNameShort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekDay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekDayShort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Quarter = table.Column<int>(type: "int", nullable: false),
                    QuarterFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DayOfYear = table.Column<int>(type: "int", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    WeekNumberFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthNameFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonthNameShortFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinancialYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinancialQuarter = table.Column<int>(type: "int", nullable: false),
                    FinancialQuarterFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinancialQuarterMonthFormat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinancialQuarterMonthShortFormat = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimDates", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryConditions_Categories_CategoryId",
                table: "CategoryConditions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadTable_Accounts_AccountId",
                table: "LoadTable",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryConditions_Categories_CategoryId",
                table: "CategoryConditions");

            migrationBuilder.DropForeignKey(
                name: "FK_LoadTable_Accounts_AccountId",
                table: "LoadTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "DimDates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryConditions",
                table: "CategoryConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "CategoryConditions",
                newName: "CategoryCondition");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryConditions_CategoryId",
                table: "CategoryCondition",
                newName: "IX_CategoryCondition_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryCondition",
                table: "CategoryCondition",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryCondition_Category_CategoryId",
                table: "CategoryCondition",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoadTable_Account_AccountId",
                table: "LoadTable",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Account_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Category_CategoryId",
                table: "Transactions",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
