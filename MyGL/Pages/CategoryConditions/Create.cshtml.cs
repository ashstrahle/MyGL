#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyGL.Controllers;
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

        public IActionResult OnGet(string SearchString = "")
        {

            ViewData["SearchString"] = SearchString;
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.CategoryName).ThenBy(c => c.SubCategory), "Id", "CategorySubCategory");
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

            _context.CategoryConditions.Add(CategoryCondition);
            _context.SaveChanges();

            // ETLController etlController = new ETLController(_context);
            // etlController.Transform();

            return RedirectToPage("./Index");
        }
    }
}
