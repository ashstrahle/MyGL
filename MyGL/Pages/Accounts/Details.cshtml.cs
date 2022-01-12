#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Pages.Accounts
{
    public class DetailsModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public DetailsModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == id);

            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
