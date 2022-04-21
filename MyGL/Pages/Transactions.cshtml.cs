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

        public async Task OnGetAsync(string OrderField, string lastOrder)
        {
            var trans = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Category)
                .ToListAsync();

            if (OrderField is null)
                Transactions = trans.OrderByDescending(t => t.Date).ToList();
            else
            {
                PropertyInfo prop = typeof(Transaction).GetProperty(OrderField);
                if (lastOrder == OrderField) // Toggle the order if we sorted on this field previously
                {
                    Transactions = (List<Transaction>)trans.OrderByDescending(x => prop.GetValue(x, null)).ToList();
                    LastOrder = null;
                }
                else
                {
                    Transactions = (List<Transaction>)trans.OrderBy(x => prop.GetValue(x, null)).ToList();
                    LastOrder = OrderField;
                }
            }
        }
    }
}
