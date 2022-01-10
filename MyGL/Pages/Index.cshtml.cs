using System.Collections;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyGL.Data;
using MyGL.Models;

namespace MyGL.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, MyGL.Data.MyGLContext context)
        {
            _logger = logger;
            _context = context;
        }

        public class Stat
        {
            public string Account { get; set; }
            public int TransCount { get; set; }
            public string LatestTrans { get; set; }
        }

        public List<Stat> Stats = new();

        public void OnGet()
        {
            foreach (Account account in _context.Account)
            {
                Stats.Add(new Stat()
                {
                    Account = account.AccountName,
                    TransCount = _context.Transactions.Where(t => t.AccountId == account.Id).Count(),
                    LatestTrans = _context.Transactions.Where(t => t.AccountId == account.Id)
                        .OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("d/M/yyyy")
                });
            }
        }
    }
}
