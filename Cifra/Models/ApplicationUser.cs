using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cifra.Models
{    
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Полное имя")]
        public string FullName { get; set; }
    }

}
