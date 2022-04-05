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
            ViewData["AccountList"] = new SelectList(_context.Accounts, "Id", "AccountName");
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateButton()
        {
            ETLController etlController = new ETLController(_context);
            etlController.Transform();

            ViewData["Info"] = "Database update complete";

            ViewData["AccountList"] = new SelectList(_context.Accounts, "Id", "AccountName");
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; }
        public List<IFormFile> CSVFiles { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            int totalLineCount = 0;
            Account = await _context.Accounts.FirstOrDefaultAsync(m => m.Id == Account.Id);
            ViewData["AccountList"] = new SelectList(_context.Accounts, "Id", "AccountName");

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

            ETLController etlController = new(_context);

            foreach (var file in CSVFiles)
            {
                int lineCount = 0;
                // Clear load table
                _context.LoadTable.RemoveRange(_context.LoadTable);
                _context.SaveChanges();

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                        {
                            var line = reader.ReadLine().Replace("\"", "");
                            lineCount++;
                            if (!(Account.HeaderRow == true && lineCount == 1) && line.Length > 0)
                            {
                                LoadTable record = new();
                                // Recreate line with quotes
                                var vars = line.Split(',');
                                try
                                {
                                    record.AccountId = Account.Id;

                                    // Remove time if it exists and replace "-" with "/"
                                    string inDate = vars[(int)Account.DateColNo - 1].Split(" ")[0].Replace("-", "/");
                                    record.Date = DateTime.ParseExact(inDate, Account.DateFormat, CultureInfo.InvariantCulture);

                                    record.Description = vars[Account.DescriptionColNo - 1];

                                    if (Account.AmountColNo is not null)
                                    {
                                        record.Amount = decimal.Parse(vars[(int)Account.AmountColNo - 1]);
                                    }
                                    else
                                    {
                                        if (vars[(int)Account.CreditColNo - 1] != "")
                                            record.Amount = decimal.Parse(vars[(int)Account.CreditColNo - 1]);
                                        else
                                            record.Amount = decimal.Parse(vars[(int)Account.DebitColNo - 1]);
                                    }
                                    if (Account.BalanceColNo is not null)
                                        record.Balance = decimal.Parse(vars[(int)Account.BalanceColNo - 1]);
                                    _context.Add(record);
                                }
                                catch(Exception e)
                                {
                                    ModelState.AddModelError("Error", "Error: " + e.Message + " File: " + fileName + ". Line: [ " + line + " ]");
                                    return Page();
                                }
                            }
                        }
                    }
                    totalLineCount += lineCount;
                    _context.SaveChanges();
                    etlController.ExtractLoad();
                }
            }

            etlController.Transform();

            ViewData["Info"] = "Read " + totalLineCount + " lines";
            return Page();
        }
    }
}
