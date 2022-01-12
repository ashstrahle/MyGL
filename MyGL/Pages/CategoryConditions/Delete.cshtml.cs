#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

            CategoryCondition = await _context.CategoryConditions
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

            CategoryCondition = await _context.CategoryConditions.FindAsync(id);

            if (CategoryCondition != null)
            {
                _context.CategoryConditions.Remove(CategoryCondition);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
