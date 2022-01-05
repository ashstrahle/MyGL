#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Data;
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

        public IList<CategoryCondition> CategoryCondition { get;set; }

        public async Task OnGetAsync()
        {
            CategoryCondition = await _context.CategoryCondition
                .Include(c => c.Category).ToListAsync();
        }
    }
}
