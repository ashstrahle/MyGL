using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyGL.Models
{
    public class LoadTable
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Account Name")]
        public int AccountId { get; set; }
        public virtual Account? Account { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal? Balance { get; set; }
    }
}
