using System.ComponentModel.DataAnnotations;

namespace Cifra.Data.Enums
{
    public enum ProductCategory
    {
        [Display(Name = "Чехлы для телефонов")]
        Phone_Case,
        [Display(Name = "Батареи")]
        Battery,
        [Display(Name = "Зарядное устройство")]
        Charging_device
    }
}
