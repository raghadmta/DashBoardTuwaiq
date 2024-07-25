using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DashBoard.Models
{
    public class Damagedproducts
    {
        [Key]
        public int Id { get; set; }
 
        public int Qty { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

    }
}
