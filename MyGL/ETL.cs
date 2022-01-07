#nullable disable
using Microsoft.AspNetCore.Mvc;
using MyGL.Data;
using MyGL.Models;

namespace MyGL.Controllers
{
    public class ETLController : Controller
    {
        private MyGLContext _context;

        public ETLController(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public async Task ExtractLoad()
        {
            foreach (var record in _context.LoadTable)
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
            foreach (var transaction in _context.Transactions.Where(t => t.CategoryId == null))
            {
                foreach (var category in _context.CategoryCondition)
                {
                    if (transaction.Description.Contains(category.SearchString))
                    {
                        var updateTrans = _context.Transactions.SingleOrDefault(t => t.Id == transaction.Id);
                        updateTrans.CategoryId = category.CategoryId;
                        break;
                    }
                }
            }
            _context.SaveChanges();
        }

        public async Task Transform()
        {
            foreach (var transaction in _context.Transactions.Where(t => t.CategoryId == null))
            {
                foreach (var category in _context.CategoryCondition)
                {
                    if (transaction.Description.Contains(category.SearchString))
                    {
                        var updateTrans = _context.Transactions.SingleOrDefault(t => t.Id == transaction.Id);
                        updateTrans.CategoryId = category.CategoryId;
                        break;
                    }
                }
            }
            _context.SaveChanges();
        }
    }
}