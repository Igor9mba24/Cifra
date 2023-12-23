using Cifra.Data.Base;
using Cifra.Models;
using System;

namespace Cifra.Data.Services
{
    public class ViewService : EntityBaseRepository<View>, IViewsService
    {
        public ViewService(ApplicationDbContext appDbContext) : base(appDbContext) { }
    }
}
