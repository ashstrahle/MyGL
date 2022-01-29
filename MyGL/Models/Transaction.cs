using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGL.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        //[Display(Name = "Days Since Last")]
        // public int ?DaysSinceLast { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Debit { get; set; }
        [Display(Name = "Debit Amount")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal DebitAmount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Credit { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal GST { get; set; }
        [Required, Display(Name = "Account Name")]
        public int AccountId { get; set; }
        public virtual Account? Account { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Balance { get; set; }
    }
}
