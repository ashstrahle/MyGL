#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Pages.CategoryRules
{
    public class IndexModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public IList<CategoryRule> CategoryRule { get; set; }

        public async Task OnGetAsync()
        {
            CategoryRule = await _context.CategoryRules.OrderBy(c => c.SearchString)
                .Include(c => c.Category).ToListAsync();
        }
    }
}
