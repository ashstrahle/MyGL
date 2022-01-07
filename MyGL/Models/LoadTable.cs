using System.ComponentModel.DataAnnotations;

namespace MyGL.Models
{
    public class LoadTable
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Account Name")]
        public int AccountId { get; set; }
        public virtual Account ?Account { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public float Amount { get; set; }
        public float ?Balance { get; set; }
    }
}
