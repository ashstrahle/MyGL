using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyGL.Models
{
    [Keyless]
    public class PivotData
	{
        [Display(Name = "Account")]
        public string AccountName { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        [Display(Name = "Category")]
        public string ?CategoryName { get; set; }
        [Display(Name = "SubCategory")]
        public string ?SubCategory { get; set; }
        [Display(Name = "Yr")]
        public int Year { get; set; }
        [Display(Name = "Q")]
        public string QuarterFormat { get; set; }
        [Display(Name = "FY")]
        public string FinancialYear { get; set; }
        [Display(Name = "FQ")]
        public string FinancialQuarterFormat { get; set; }
        [Display(Name = "Month")]
        public string MonthNameShortFormat { get; set; }
	}
}