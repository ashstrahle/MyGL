﻿#nullable disable
using System.ComponentModel.DataAnnotations;
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

        [BindProperty]
        public CategoryCondition CategoryCondition { get; set; }

        [BindProperty]
        [Display(Name = "Update Database")]
        public bool Update { get; set; }

        public IActionResult OnGet(string searchString = "")
        {
            ViewData["SearchString"] = searchString;
            ViewData["CategoryId"] = new SelectList(_context.Categories.OrderBy(c => c.CategoryName).ThenBy(c => c.SubCategory), "Id", "CategorySubCategory");
            ViewData["referer"] = Request.Headers["referer"]; // Get the referring page so we can return to it once done
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string referer = null)
        {
            if (_context.CategoryConditions.Where(c => c.SearchString == CategoryCondition.SearchString).Count() > 0)
            {
                ModelState.AddModelError("Error", "'" + CategoryCondition.SearchString + "' already exists");
                ViewData["SearchString"] = CategoryCondition.SearchString;
                ViewData["referer"] = referer;
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CategoryConditions.Add(CategoryCondition);
            _context.SaveChanges();

            if (Update)
            {
                ETLController etlController = new ETLController(_context);
                etlController.Transform();
            }

            if (referer is not null)
            {
                return Redirect(referer);
            }
            return RedirectToPage("./Index");
        }
    }
}
