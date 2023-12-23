using Cifra.Data.Base;
using Cifra.Models;
using System;

namespace Cifra.Data.Services
{
    public class ManufacturerService : EntityBaseRepository<Manufacturer>, IManufacturersService
    {
        public ManufacturerService(ApplicationDbContext appDbContext) : base(appDbContext) { }
    }
}
