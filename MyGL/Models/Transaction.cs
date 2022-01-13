using System.ComponentModel.DataAnnotations;

namespace MyGL.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Display(Name = "Month As Date")]
        public DateTime MonthAsDate { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        //[Display(Name = "Days Since Last")]
       // public int ?DaysSinceLast { get; set; }
        public decimal Amount { get; set; }
        public decimal Debit { get; set; }
        [Display(Name = "Debit Amount")]
        public decimal DebitAmount { get; set; }
        public decimal Credit { get; set; }
        public decimal GST { get; set; }
        [Required, Display(Name = "Account Name")]
        public int AccountId { get; set; }
        public virtual Account? Account { get; set; }
        public decimal? Balance { get; set; }
    }
}
