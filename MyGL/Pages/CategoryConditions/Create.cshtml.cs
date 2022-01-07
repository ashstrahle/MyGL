#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyGL.Models;

namespace MyGL.Pages.CategoryConditions
{
    public class CreateModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public CreateModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategorySubCategory");
            return Page();
        }

        [BindProperty]
        public CategoryCondition CategoryCondition { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CategoryCondition.Add(CategoryCondition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
