using Cifra.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cifra.Models
{
    [Table("Производитель")]
    public class Manufacturer : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Изображение")]
        [Required(ErrorMessage = "Изображение обязательно!")]
        public string ProfilePictureURL { get; set; }

        [Display(Name = "Полное название")]
        [Required(ErrorMessage = "Полное  назввание обязательно!")]
        public string FullName { get; set; }

        // Relationships
        public List<Manufacturer_Product>? Manufacturer_Product { get; set; }
    }
}
