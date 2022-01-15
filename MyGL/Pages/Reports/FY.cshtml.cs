#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGL.Controllers;
using MyGL.Data;
using MyGL.Models;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Net.Http.Headers;

namespace MyGL.Pages.FY
{
    public class IndexModel : PageModel
    {
        private MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string SelectedFY { get; set; }

        [Display(Name = "Financial Years")]
        public SelectList FYs { get; set; }

        public IActionResult OnGet()
        {
            List<PivotData> data = _context.View_PivotData.ToList();
            List<string> FYList = data.OrderByDescending(pd => pd.FinancialYear).Select(pd => pd.FinancialYear).Distinct().ToList();
            FYs = new SelectList(FYList);
            SelectedFY = FYList.First();
            ViewData["DataSource"] = data.Where(pd => pd.FinancialYear == FYList.First());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string Selected = SelectedFY;
            List<PivotData> data = _context.View_PivotData.ToList();
            List<string> FYList = data.OrderByDescending(pd => pd.FinancialYear).Select(pd => pd.FinancialYear).Distinct().ToList();
            FYs = new SelectList(FYList);
            ViewData["DataSource"] = data.Where(pd => pd.FinancialYear == SelectedFY);
            return Page();
        }
    }
}

