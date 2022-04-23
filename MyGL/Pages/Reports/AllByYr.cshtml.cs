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

namespace MyGL.Pages.All
{
    public class IndexModel : PageModel
    {
        private MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }


        [DataType(DataType.Date)]
        public string FromDate { get; set; }

        [DataType(DataType.Date)]
        public string ToDate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // var data = GetPivotData();
            List<PivotData> data = _context.view_PivotData.ToList();
            if (data.Count() == 0)
            {
                ModelState.AddModelError("Error", "No data");
                return RedirectToPage("./Index");
            }
            ViewData["DataSource"] = data;
            FromDate = _context.Transactions.OrderBy(t => t.Date).FirstOrDefault().Date.ToString("dd/MM/yyyy");
            ToDate = _context.Transactions.OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("dd/MM/yyyy");
            return Page();
        }
    }
}

