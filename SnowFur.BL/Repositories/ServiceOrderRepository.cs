using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Repositories
{
    public class ServiceOrderRepository : EntityFrameworkRepository<ServiceOrder, int>
    {
        public ServiceOrderRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}
