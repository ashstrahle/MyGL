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

namespace MyGL.Pages.All
{
    public class IndexModel : PageModel
    {
        private MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // var data = GetPivotData();
            List<PivotData> data = _context.View_PivotData.ToList();
            ViewData["DataSource"] = data;
            return Page();
        }
    }
}

