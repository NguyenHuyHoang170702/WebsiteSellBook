using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SellBook.Models
{
    public class Category
    {
        [Key]
        public int Category_ID { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Please enter Category Name")]
        public string Category_Name { get; set; }


        [DisplayName("Display Order")]
        [Required(ErrorMessage = "Please enter Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be between 1 and 100 only!!!")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
