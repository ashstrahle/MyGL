#nullable disable
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get; set; }

        public async Task OnGetAsync()
        {
            Account = await _context.Accounts.ToListAsync();
        }
    }
}
