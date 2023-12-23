using Cifra.Models;
using Microsoft.CodeAnalysis;
using System.Security.Policy;

namespace Cifra.Data.ViewModels
{
    public class NewProductDropdownsVM
    {
        public List<View> Views { get; set; }
        public List<Country> Countrys { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }

        public NewProductDropdownsVM()
        {
            Views = new List<View>();
            Countrys = new List<Country>();
            Manufacturers = new List<Manufacturer>();
        }
    }
}
