#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Pages.CategoryRules
{
    public class DetailsModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public DetailsModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public CategoryRule CategoryRule { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryRule = await _context.CategoryRules
                .Include(c => c.Category).FirstOrDefaultAsync(m => m.Id == id);

            if (CategoryRule == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
