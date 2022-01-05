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
    public class DeleteModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public DeleteModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryCondition = await _context.CategoryCondition.FindAsync(id);

            if (CategoryCondition != null)
            {
                _context.CategoryCondition.Remove(CategoryCondition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
