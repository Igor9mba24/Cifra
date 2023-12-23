namespace Cifra.Models
{
    public class Manufacturer_Product
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
