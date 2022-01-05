using System.ComponentModel.DataAnnotations;

namespace MyGL.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Bank Account")]
        public string AccountName { get; set; }

        [Display(Name = "CSV Contains Header")]
        public bool CSVHeader { get; set; }
    }
}
