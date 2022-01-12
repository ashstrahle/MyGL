#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Pages.CategoryConditions
{
    public class IndexModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public IList<CategoryCondition> CategoryCondition { get; set; }

        public async Task OnGetAsync()
        {
            CategoryCondition = await _context.CategoryConditions.OrderBy(c => c.SearchString)
                .Include(c => c.Category).ToListAsync();
        }
    }
}
