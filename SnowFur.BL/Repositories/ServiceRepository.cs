using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using DotVVM.Framework.Utils;

namespace SnowFur.BL.Repositories
{
    public class ServiceRepository : GuardedEntityRepositiryBase<Service>
    {
        public ServiceRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
        
        public int GetOrdersCount(int serviceId)
        {
            return Context.CastTo<ApplicationDbContextContainer>().ServiceOrders.Where(so => so.ServiceId == serviceId).Count();
        }
    }
}
