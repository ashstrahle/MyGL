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
            public Account Account { get; set; }           
            public string LatestTrans { get; set; }
            public int TransCount { get; set; }
            public int UncategorisedCount { get; set; }
        }

        public List<Stat> Stats = new();

        public void OnGet()
        {
            foreach (Account account in _context.Accounts)
            {
                if (_context.Transactions.Where(t => t.Account == account).Count() > 0)
                {
                    Stats.Add(new Stat()
                    {
                        Account = account,
                        TransCount = _context.Transactions.Where(t => t.AccountId == account.Id).Count(),
                        LatestTrans = _context.Transactions.Where(t => t.AccountId == account.Id)
                            .OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("d/M/yyyy"),
                        UncategorisedCount = _context.Transactions.Where(t => t.AccountId == account.Id && t.CategoryId == null).Count()
                    });                  
                }
            }
            // Add Total row
            Stats.Add(new Stat()
            {
                Account = new Account() { AccountName = "Total" },
                TransCount = _context.Transactions.Count(),
                LatestTrans = _context.Transactions
                            .OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("d/M/yyyy"),
                UncategorisedCount = _context.Transactions.Where(t => t.CategoryId == null).Count()
            });
        }
    }
}
