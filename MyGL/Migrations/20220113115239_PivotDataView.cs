using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyGL.Migrations
{
    public partial class PivotData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                create view view_PivotData as
                select
                a.AccountName
                , t.Date
                , t.DESCRIPTION
                , t.Amount
                , c.CategoryName
                , c.SubCategory
                , d.FinancialYear
                , d.FinancialQuarterFormat
                , d.MonthNameShortFormat
                from Transactions t
                left join Accounts a on t.AccountId = a.ID 
                left join Categories c on t.CategoryId = c.ID
                left join DimDates d on t.Date = d.Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                drop view view_PivotData");
        }
    }
}
