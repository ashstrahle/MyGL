#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGL.Controllers;
using MyGL.Data;
using MyGL.Models;
using System.Globalization;
using System.Net.Http.Headers;

namespace MyGL.Pages.Import
{
    public class IndexModel : PageModel
    {
        private MyGL.Data.MyGLContext _context;

        public IndexModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        // public IList<Account> Accounts { get; set; }
        // public Account Account { get; set; }

        public IActionResult OnGet()
        {
            ViewData["AccountList"] = new SelectList(_context.Account, "Id", "AccountName");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }
        public List<IFormFile> CSVFiles { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            Account = await _context.Account.FirstOrDefaultAsync(m => m.Id == Account.Id);
            ViewData["AccountList"] = new SelectList(_context.Account, "Id", "AccountName");

            if (Account == null)
            {
                ModelState.AddModelError("Account", "Account not found");
                return Page();
            }

            if (CSVFiles.Count == 0)
            {
                ModelState.AddModelError("CSVFiles", "No files selected");
                return Page();
            }

            // Clear load table
            _context.LoadTable.RemoveRange(_context.LoadTable);
            _context.SaveChanges();

            int linecount = 0;
            foreach (var file in CSVFiles)
            {
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                        {
                            var line = reader.ReadLine().Replace("\"", "").Replace("'", "''");
                            linecount++;
                            if (!(Account.HeaderRow == true && linecount == 1) && line.Length > 0)
                            {
                                LoadTable record = new LoadTable();
                                // Recreate line with quotes
                                var vars = line.Split(',');
                                record.AccountId = Account.Id;
                                record.Date = DateTime.ParseExact(vars[(int)Account.DateColNo - 1], "d/M/yyyy", CultureInfo.InvariantCulture);
                                record.Description = vars[Account.DescriptionColNo - 1];
                                record.Amount = decimal.Parse(vars[Account.AmountColNo - 1]);
                                if (Account.BalanceColNo is not null)
                                    record.Balance = decimal.Parse(vars[(int)Account.BalanceColNo - 1]);
                                _context.Add(record);
                            }
                        }
                    }
                }
            }
            _context.SaveChanges();

            foreach (LoadTable record in _context.LoadTable)
            {
                Transaction transaction = new Transaction()
                {
                    Date = record.Date,
                    Description = record.Description,
                    Amount = record.Amount,
                    AccountId = record.AccountId,
                    Balance = record.Balance,
                    Debit = record.Amount < 0 ? record.Amount : 0,
                    DebitAmount = record.Amount < 0 ? 0 - record.Amount : 0,
                    Credit = record.Amount > 0 ? record.Amount : 0,
                    GST = record.Amount / 11
                };
                if (_context.Transactions.Where(t => t.Date == transaction.Date && t.Description == transaction.Description && t.Amount == transaction.Amount && t.AccountId == transaction.AccountId).Count() == 0)
                {
                    _context.Transactions.Add(transaction);
                }
            }
            _context.SaveChanges();

            ETLController etlController = new ETLController(_context);
            etlController.ExtractLoad();
            //etlController.Transform();

            ViewData["Info"] = "Imported " + linecount + " lines";
            return Page();
        }
    }
}
