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
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int TransCount { get; set; }
            public int UncategorisedCount { get; set; }
        }

        public List<Stat> Stats = new();

        public void OnGet()
        {
            var DefaultCategories = new List<(string, string)>
            {
                ("groceries", "groceries")
            };

            foreach (Account account in _context.Accounts)
            {
                if (_context.Transactions.Where(t => t.Account == account).Count() > 0)
                {
                    Stats.Add(new Stat()
                    {
                        Account = account,
                        TransCount = _context.Transactions.Where(t => t.AccountId == account.Id).Count(),
                        FromDate = _context.Transactions.Where(t => t.AccountId == account.Id)
                            .OrderBy(t => t.Date).FirstOrDefault().Date.ToString("dd/MM/yyyy"),
                        ToDate = _context.Transactions.Where(t => t.AccountId == account.Id)
                            .OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("dd/MM/yyyy"),
                        UncategorisedCount = _context.Transactions.Where(t => t.AccountId == account.Id && t.CategoryId == null).Count()
                    });
                }
            }
            if (_context.Accounts.Count() > 0)
            {
                // Add Total row
                Stats.Add(new Stat()
                {
                    Account = new Account() { AccountName = "Total" },
                    TransCount = _context.Transactions.Count(),
                    FromDate = _context.Transactions
                            .OrderBy(t => t.Date).FirstOrDefault().Date.ToString("dd/MM/yyyy"),
                    ToDate = _context.Transactions
                                .OrderByDescending(t => t.Date).FirstOrDefault().Date.ToString("dd/MM/yyyy"),
                    UncategorisedCount = _context.Transactions.Where(t => t.CategoryId == null).Count()
                });
            }
            else if (_context.Categories.Count() == 0)
            {
                // Add Default Categories to database
                foreach (var defaultcategory in DefaultCategories)
                {
                    Category category = new()
                    {
                        CategoryName = defaultcategory.Item1,
                        SubCategory = defaultcategory.Item2
                    };
                    _context.Categories.Add(category);
                }
                _context.SaveChanges();
            }
        }
    }
}
