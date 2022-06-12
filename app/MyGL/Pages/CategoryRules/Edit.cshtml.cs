#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGL.Controllers;
using MyGL.Models;

namespace MyGL.Pages.CategoryRules
{
    public class EditModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public EditModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.CategoryName).ThenBy(c => c.SubCategory), "Id", "CategorySubCategory");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CategoryRule).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryRuleExists(CategoryRule.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // Update Category for all Transactions that match this updated CategoryRule
            foreach(Transaction transaction in _context.Transactions.Where(t => t.Description.ToUpper().Contains(CategoryRule.SearchString.ToUpper())))
            {
                transaction.CategoryId = CategoryRule.CategoryId;
                _context.Attach(transaction).State = EntityState.Modified;          
            }
            _context.SaveChanges();

           // ETLController etlController = new ETLController(_context);
           // etlController.Transform();

            return RedirectToPage("./Index");
        }

        private bool CategoryRuleExists(int id)
        {
            return _context.CategoryRules.Any(e => e.Id == id);
        }
    }
}
