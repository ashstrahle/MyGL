#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyGL.Models;

namespace MyGL.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly MyGL.Data.MyGLContext _context;

        public TransactionsModel(MyGL.Data.MyGLContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transactions { get;set; }

        public string LastOrder;

        public void OnGet(string orderField, string lastOrder)
        {
            if (orderField is null)
                Transactions = GetTransactionsAsync("").Result.OrderByDescending(t => t.Date).ToList();
            else
            {
                PropertyInfo prop = typeof(Transaction).GetProperty(orderField);
                if (lastOrder == orderField) // Toggle the order if we sorted on this field previously
                {
                    Transactions = (List<Transaction>)GetTransactionsAsync("").Result.OrderByDescending(x => prop.GetValue(x, null)).ToList();
                    LastOrder = null;
                }
                else
                {
                    Transactions = (List<Transaction>)GetTransactionsAsync("").Result.OrderBy(x => prop.GetValue(x, null)).ToList();
                    LastOrder = orderField;
                }
            }
        }

        public void OnPost(string searchString)
        {
            Transactions = GetTransactionsAsync(searchString).Result.OrderByDescending(t => t.Date).ToList();
        }

        private async Task<List<Transaction>> GetTransactionsAsync(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return await _context.Transactions.Where(t => t.Description.Contains(searchString))
                    .Include(t => t.Account)
                    .Include(t => t.Category)
                    .ToListAsync();
            }
            else
            {
                return await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .ToListAsync();
            }
        }
    }
}
