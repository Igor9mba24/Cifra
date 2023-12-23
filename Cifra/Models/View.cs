using Cifra.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Models
{
    public class View : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Название обязательно!")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Описание обязательно!")]
        public string Description { get; set; }

        // Relationships
        public List<Product>? Products { get; set; }
    }
}
