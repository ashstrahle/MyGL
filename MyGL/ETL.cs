#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGL.Data;
using MyGL.Models;

namespace MyGL.Controllers
{
    public class ETLController : Controller
    {
        private MyGLContext _context;

        public ETLController(MyGLContext context)
        {
            _context = context;
        }

        public async Task ExtractLoad()
        {
            List<LoadTable> records = _context.LoadTable.ToList();
            // Break records up into groups of 100
            int GroupSize = 100;
            List<List<LoadTable>> RecordGroup = new();
            while (records.Count > 0)
            {
                int count = records.Count > GroupSize ? GroupSize : records.Count;
                RecordGroup.Add(records.GetRange(0, count));
                records.RemoveRange(0, count);
            }

            // Asynchronously process record groups
            var tasks = RecordGroup.Select(async recgroup =>
            {
                foreach (LoadTable record in recgroup)
                {
                    // Only add transaction if it doesn't already exist
                    if (_context.Transactions.Where(t => t.Date == record.Date && t.Description == record.Description && t.Amount == record.Amount && t.AccountId == record.AccountId && t.Balance == record.Balance).Count() == 0)
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
                        _context.Transactions.Add(transaction);
                    }
                }
            });

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);
            _context.SaveChanges();
        }

        public async Task Transform()
        {
            // Get all uncategorized transactions
            List<Transaction> transactions = _context.Transactions.Where(t => t.CategoryId == null).ToList();

            // Break transactions up into groups of 20
            int GroupSize = 20;
            List<List<Transaction>> TransactionGroup = new();
            while (transactions.Count > 0)
            {
                int count = transactions.Count > GroupSize ? GroupSize : transactions.Count;
                TransactionGroup.Add(transactions.GetRange(0, count));
                transactions.RemoveRange(0, count);
            }

            // Asynchronously process transaction groups
            var tasks = TransactionGroup.Select(async transgroup =>
            {
                Categorise(transgroup);
            });

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);
            _context.SaveChanges();
        }

        public async Task Categorise(List<Transaction> transactions)
        {
            foreach (Transaction transaction in transactions)
            {
                foreach (var category in _context.CategoryCondition)
                {
                    if (transaction.Description.ToUpper().Contains(category.SearchString.ToUpper()))
                    {
                        transaction.CategoryId = category.CategoryId;
                        _context.Attach(transaction).State = EntityState.Modified;
                    }
                }
            }
        }
    }
}