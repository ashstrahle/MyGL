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
        }

        public async Task Transform()
        {
            foreach (var transaction in _context.Transactions.Where(t => t.CategoryId == null))
            {
                foreach (var category in _context.CategoryCondition)
                {
                    if (transaction.Description.Contains(category.SearchString))
                    {
                        Transaction update_trans = _context.Transactions.SingleOrDefault(t => t.Id == transaction.Id);
                        update_trans.CategoryId = category.CategoryId;
                        _context.Update(update_trans);
                        _context.Attach(update_trans).State = EntityState.Modified;
                    }
                }
            }
            _context.SaveChanges();
        }
    }
}