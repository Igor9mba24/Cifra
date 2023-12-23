using System.ComponentModel.DataAnnotations;

namespace Cifra.Data.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Почтовый адресс")]
        [Required(ErrorMessage = "Почтовый адресс обязателен!")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
