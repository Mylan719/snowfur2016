using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Queries;
using SnowFur.BL.Repositories;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Services
{
    public class ServiceService : CrudServiceBase<Service, int, ServiceDetailDto, ServiceDetailDto, ConventionFilter>
    {
        public Func<ServiceListQuery> QueryFunc { get; set; }
        public ServiceRepository Repository { get; set; }

        public int ActiveConventionId { get; set; }

        protected override IQuery<ServiceDetailDto> CreateQuery(ConventionFilter filter)
        {
            var q = QueryFunc();
            q.Filter = filter;
            return q;
        }

        protected override IRepository<Service, int> GetRepository() => Repository;

        public override ServiceDetailDto Create() => new ServiceDetailDto { ConventionId = ActiveConventionId};

        public List<ServiceTypeDto> GetServiceTypes() => ServiceTypeHelper.GetServiceTypes().ToList();

        public int GetOrdersCount(int serviceId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return Repository.GetOrdersCount(serviceId);
            }
        }
    }
}
