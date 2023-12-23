using Cifra.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Data.ViewModels
{
    public class NewProductVM
    {
        public int Id { get; set; }

        [Display(Name = "URL изображения товара")]
        [Required(ErrorMessage = "URL изображение обязательно!")]
        public string ImageURL { get; set; }

        [Display(Name = "Название товара")]
        [Required(ErrorMessage = "Название обязательно!")]
        public string Name { get; set; }

        [Display(Name = "Описание товара")]
        [Required(ErrorMessage = "Описание обязательно!")]
        public string Description { get; set; }

        [Display(Name = "Цена в рублях")]
        [Required(ErrorMessage = "Цена обязательна!")]
        public double Price { get; set; }

        [Display(Name = "Дата выпуска")]
        [Required(ErrorMessage = "Дата обязательна!")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Выбор категории")]
        [Required(ErrorMessage = "Категория обязательна!")]
        public ProductCategory Category { get; set; }

        // Relationships
        [Display(Name = "Выберите производителя")]
        [Required(ErrorMessage = "Производитель товара обязателен!")]
        public List<int> ManufacturerId { get; set; }

        [Display(Name = "Выберите старну товара")]
        [Required(ErrorMessage = "Страна обязательна!")]
        public int CountryId { get; set; }

        [Display(Name = "Выберите вид товара")]
        [Required(ErrorMessage = "Вид обязателен!")]
        public int ViewId { get; set; }
    }
}
