using System.ComponentModel.DataAnnotations;

namespace MyGL.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Account")]
        public string AccountName { get; set; }
        [Display(Name = "Header Row")]
        public bool HeaderRow { get; set; }
        [Required, Display(Name = "Date Column Number"), Range(1, 10)]
        public int DateColNo { get; set; }
        [Required, Display(Name = "Description Column Number"), Range(1, 10)]
        public int DescriptionColNo { get; set; }
        [Display(Name = "Amount Column Number"), Range(1, 10)]
        public int? AmountColNo { get; set; }
        [Display(Name = "Credit Column Number"), Range(1, 10)]
        public int? CreditColNo { get; set; }
        [Display(Name = "Debit Column Number"), Range(1, 10)]
        public int? DebitColNo { get; set; }
        [Display(Name = "Balance Column Number"), Range(1, 10)]
        public int? BalanceColNo { get; set; }
    }
}