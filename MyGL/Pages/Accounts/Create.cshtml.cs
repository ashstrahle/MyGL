#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyGL.Data;
using MyGL.Models;

namespace MyGL.Pages.Accounts
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
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check for duplicates

            List<int> colNos = new List<int>();

            Type type = Account.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(Account, null);
                if (value != null && property.Name.Contains("ColNo"))
                {
                    if (colNos.Contains((int)value))
                    {
                        ModelState.AddModelError("Duplicate", "Duplicate found for " + value);
                        return Page();
                    }
                    colNos.Add((int)value);
                }
            }

                _context.Account.Add(Account);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
