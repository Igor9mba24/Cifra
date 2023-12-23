using System.ComponentModel.DataAnnotations;

namespace Cifra.Data.ViewModels
{
    public class SignupVM
    {
        [Display(Name = "Полное имя")]
        [Required(ErrorMessage = "Полное имя обязательно!")]
        public string FullName { get; set; }

        [Display(Name = "Почтовый адресс")]
        [Required(ErrorMessage = "Почтовый адресс обязателен!")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
