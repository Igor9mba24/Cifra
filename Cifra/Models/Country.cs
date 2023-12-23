using Cifra.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Models
{
    public class Country : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Картинка")]
        [Required(ErrorMessage = "Картинка обязательна!")]
        public string Logo { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Название обязательно!")]
        public string Name { get; set; }

        // Relationships
        public List<Product>? Products { get; set; }
    }
}

