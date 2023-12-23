using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Diagnostics.Metrics;
using Cifra.Data.Enums;
using Cifra.Data.Base;

namespace Cifra.Models
{
    [Table("Товары")]
    public class Product : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Изображение")]
        public string ImageURL { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        public double Price { get; set; }
        [Display(Name = "Дата выпуска")]
        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Категория")]
        public ProductCategory Category { get; set; }

        // Relationships
        public List<Manufacturer_Product> Manufacturer_Product { get; set; }

        // Country
        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public Country country { get; set; }

        //View
        public int ViewId { get; set; }
        [ForeignKey("ViewId")]
        public View View { get; set; }
    }
}