using System.ComponentModel.DataAnnotations;

namespace MyGL.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Header Row")]
        public bool HeaderRow { get; set; }
        [Display(Name = "Date Column Number"), Range(1, 10)]
        public int? DateColNo { get; set; }
        [Required, Display(Name = "Description Column Number"), Range(1, 10)]
        public int DescriptionColNo { get; set; }
        [Required, Display(Name = "Amount Column Number"), Range(1, 10)]
        public int AmountColNo { get; set; }
        [Display(Name = "Balance Column Number"), Range(1, 10)]
        public int? BalanceColNo { get; set; }
    }
}
