#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Pages.Uncategorised
{
    public class IndexModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transactions { get; set; }

        public async Task OnGetAsync()
        {
            Transactions = await _context.Transactions.Where(t => t.CategoryId == null).OrderBy(t => t.Date).ToListAsync();
        }
    }
}
