using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Models
{
    [Table("Корзина")]
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        public Product Product { get; set; }
        public int Quantity { get; set; }

        public string CartId { get; set; }
    }
}
