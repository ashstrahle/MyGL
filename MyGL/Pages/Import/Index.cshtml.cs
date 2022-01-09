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

        public IActionResult OnGet()
        {
            ViewData["AccountList"] = new SelectList(_context.Account, "Id", "AccountName");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateButton()
        {
            ETLController etlController = new ETLController(_context);
            etlController.Transform();

            ViewData["Info"] = "Database update complete";

            ViewData["AccountList"] = new SelectList(_context.Account, "Id", "AccountName");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }
        public List<IFormFile> CSVFiles { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            int linecount = 0;
            int newrecordcount = 0;
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
            /*
            List<LoadTable> records = _context.LoadTable.ToList();
            // Break transactions up into groups of 100
            int GroupSize = 100;
            List<List<LoadTable>> RecordGroup = new List<List<LoadTable>>();
            while (records.Count > 0)
            {
                int count = records.Count > GroupSize ? GroupSize : records.Count;
                RecordGroup.Add(records.GetRange(0, count));
                records.RemoveRange(0, count);
            }

            // Asynchronously process transaction groups
            var tasks = RecordGroup.Select(async recgroup =>
            {
                foreach (LoadTable record in recgroup)
                {
                    if (_context.Transactions.Where(t => t.Date == record.Date && t.Description == record.Description && t.Amount == record.Amount && t.AccountId == record.AccountId && t.Balance == record.Balance).Count() == 0)
                    {
                        newrecordcount++;
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
                        _context.Transactions.Add(transaction);
                    }
                }
            });

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);
            _context.SaveChanges();
            */
            ETLController etlController = new(_context);
            etlController.ExtractLoad();
            etlController.Transform();

            ViewData["Info"] = "Read " + linecount + " lines, created " + newrecordcount + " new records";
            return Page();
        }
    }
}
