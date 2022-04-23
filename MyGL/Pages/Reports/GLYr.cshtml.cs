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

namespace MyGL.Pages.Yr
{
    public class IndexModel : PageModel
    {
        private MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string SelectedYr { get; set; }

        public List<string>YrList { get; set; }

        [Display(Name = "Year")]
        public SelectList Yrs { get; set; }

        [DataType(DataType.Date)]
        public string LatestTrans{ get; set; }

        public async Task<IActionResult>OnGetAsync()
        {
            List<PivotData> data = _context.view_PivotData.ToList();
            YrList = data.OrderByDescending(pd => pd.Year.ToString()).Select(pd => pd.Year.ToString()).Distinct().ToList();
            if (YrList.Count() == 0)
            {
                ModelState.AddModelError("Error", "No data");
                return RedirectToPage("./Index");
            }
            Yrs = new SelectList(YrList);
            SelectedYr = YrList.FirstOrDefault();
            ViewData["DataSource"] = data.Where(pd => pd.Year.ToString() == YrList.FirstOrDefault());
            ViewData["DrilledMembers"] = data.Select(pd => pd.QuarterFormat).Distinct().ToArray();
            LatestTrans = _context.Transactions.OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("d/MM/yyyy"); 
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string Selected = SelectedYr;
            List<PivotData> data = _context.view_PivotData.ToList();
            YrList = data.OrderByDescending(pd => pd.Year.ToString()).Select(pd => pd.Year.ToString()).Distinct().ToList();
            Yrs = new SelectList(YrList);
            ViewData["DataSource"] = data.Where(pd => pd.Year.ToString() == SelectedYr);
            ViewData["DrilledMembers"] = data.Select(pd => pd.QuarterFormat).Distinct().ToArray();
            LatestTrans = _context.Transactions.OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("d/MM/yyyy");
            return Page();
        }
    }
}

