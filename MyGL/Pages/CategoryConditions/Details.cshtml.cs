#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Data;
using MyGL.Models;

namespace MyGL.Pages.CategoryConditions
{
    public class DetailsModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public DetailsModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public CategoryCondition CategoryCondition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryCondition = await _context.CategoryCondition
                .Include(c => c.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (CategoryCondition == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
