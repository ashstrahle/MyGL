#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGL.Controllers;
using MyGL.Data;
using MyGL.Models;
using System.Globalization;
using System.Net.Http.Headers;

namespace MyGL.Pages.Pivot
{
    public class IndexModel : PageModel
    {
        private MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // var data = GetPivotData();
            List<PivotData> data = _context.View_PivotData.ToList();
            ViewData["DataSource"] = data;
            return Page();
        }

       /* public class PivotData
        {
            public string Account { get; set; }
            public string Description { get; set; }
            public decimal Amount { get; set; }
            public string Category { get; set; }
            public string SubCategory { get; set; }
            public DateTime Date { get; set; }
            public string FY { get; set; }
            public string FQ { get; set; }
            public string Month { get; set; }
        } */
       /*
        public List<PivotData> GetPivotData()
        {
            List<PivotData> pivotDataList = new List<PivotData>();
            foreach(Transaction transaction in _context.Transactions)
            {
                PivotData pivotData = new()
                {
                    Account = transaction.Account.AccountName,
                    Description = transaction.Description,
                    Amount = transaction.Amount,
                    Category = (transaction.Category is not null ? transaction.Category.CategoryName : ""),
                    SubCategory = (transaction.Category is not null ? transaction.Category.SubCategory : ""),
                    Date = transaction.Date,
                    FY = _context.DimDates.Where(d => d.Date == transaction.Date).FirstOrDefault().FinancialYear,
                    FQ = _context.DimDates.Where(d => d.Date == transaction.Date).FirstOrDefault().FinancialQuarterFormat,
                    Month = _context.DimDates.Where(d => d.Date == transaction.Date).FirstOrDefault().MonthNameShortFormat
                 };
                pivotDataList.Add(pivotData);   
            }
            return pivotDataList;
        }*/
    }
}

