﻿#nullable disable
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

        public List<string>FYList { get; set; }

        [Display(Name = "Financial Years")]
        public SelectList FYs { get; set; }

        [DataType(DataType.Date)]
        public string LatestTrans{ get; set; }

        public async Task<IActionResult>OnGetAsync()
        {
            List<PivotData> data = _context.View_PivotData.ToList();
            FYList = data.OrderByDescending(pd => pd.FinancialYear).Select(pd => pd.FinancialYear).Distinct().ToList();
            FYs = new SelectList(FYList);
            SelectedFY = FYList.First();
            ViewData["DataSource"] = data.Where(pd => pd.FinancialYear == FYList.First());
            ViewData["DrilledMembers"] = data.Select(pd => pd.FinancialQuarterFormat).Distinct().ToArray();
            LatestTrans = _context.Transactions.OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("d/M/yyyy"); 
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
            FYList = data.OrderByDescending(pd => pd.FinancialYear).Select(pd => pd.FinancialYear).Distinct().ToList();
            FYs = new SelectList(FYList);
            ViewData["DataSource"] = data.Where(pd => pd.FinancialYear == SelectedFY);
            ViewData["DrilledMembers"] = data.Select(pd => pd.FinancialQuarterFormat).Distinct().ToArray();
            LatestTrans = _context.Transactions.OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("d/M/yyyy");
            return Page();
        }
    }
}

