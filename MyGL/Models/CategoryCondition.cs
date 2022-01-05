using System.ComponentModel.DataAnnotations;

namespace MyGL.Models
{
    public class CategoryCondition
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Search String")]
        public string SearchString { get; set; }
        [Required, Display(Name = "Category")]
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
