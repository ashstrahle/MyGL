#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGL.Data;
using MyGL.Models;

namespace MyGL.Pages.CategoryConditions
{
    public class EditModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public EditModel(MyGL.Data.MyGLContext context)
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
           ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "CategorySubCategory");
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

            _context.Attach(CategoryCondition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryConditionExists(CategoryCondition.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryConditionExists(int id)
        {
            return _context.CategoryCondition.Any(e => e.Id == id);
        }
    }
}
