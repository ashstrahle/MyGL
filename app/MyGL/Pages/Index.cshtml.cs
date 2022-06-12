using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public int AccountCount = new();

        public void OnGet()
        {
            AccountCount = _context.Accounts.Count();
            foreach (Account account in _context.Accounts.OrderBy(a => a.AccountName))
            {
                if (_context.Transactions.Where(t => t.Account == account).Count() > 0)
                {
                    try
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
                    catch (Exception e)
                    { }
                }
            }
            if (_context.Accounts.Count() > 0)
            {
                try
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
                catch (Exception e)
                { }
            }
        }
    }
}
