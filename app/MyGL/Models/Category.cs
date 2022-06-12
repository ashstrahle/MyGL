using System.ComponentModel.DataAnnotations;

namespace MyGL.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required, Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Required, Display(Name = "Sub-Category")]
        public string SubCategory { get; set; }
        public string CategorySubCategory { get { return CategoryName + "|" + SubCategory; } }
    }
}
