using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard.Models
{
    public class ProductDetails
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int Products_Id { get; set; }

        public string Images { get; set; }

        public double Price { get; set; }

        public int Qty { get; set; }

        public string Color { get; set; }

         public Product? Product { get; set; }
    }
}
