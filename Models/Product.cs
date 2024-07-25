using System.ComponentModel.DataAnnotations;

namespace DashBoard.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "يرجى ادخال الاسم")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "يرجى إدخال الوصف")]
        [StringLength(50)]
        public string Description { get; set; }

        public ProductDetails? ProductDetails { get; set; }

    }
}
