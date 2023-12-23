using Cifra.Data.Base;
using Cifra.Models;
using System;

namespace Cifra.Data.Services
{
    public class CountryService : EntityBaseRepository<Country>, ICountryService
    {
        public CountryService(ApplicationDbContext appDbContext) : base(appDbContext) { }
    }
}
