using SnowFur.BL.Dtos;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Filters;
using SnowFur.BL.Services;
using SnowFur.DAL.Enums;

namespace SnowFur.BL.Queries
{
    public class ServiceUserOrderQuery : ApplicationQueryBase<ServiceUserOrderListDto>
    {
        public ConventionUserFilter Filter { get; set; }
        public Func<ServiceType, IServiceOrderStrategy> ServiceOrderStrategyProvider { get; set; }

        public ServiceUserOrderQuery(IUnitOfWorkProvider provider)
            :base (provider)
        { }

        protected override IQueryable<ServiceUserOrderListDto> GetQueryable()
            => Context.ServiceOrders
            .Include("User")
            .Include("Service")
            .Where(s => s.Service.DateDeleted == null && s.Service.ConventionId == Filter.ConventionId && s.UserId == Filter.UserId)
            .ProjectTo<ServiceUserOrderListDto>();

        protected override void PostProcessResults(IList<ServiceUserOrderListDto> results)
        {
            foreach (var result in results)
            {
                var strategy = ServiceOrderStrategyProvider((ServiceType) result.ServiceType);
                result.IsActive = strategy.CanBeOrdered(Filter.UserId, result.ServiceId);
            }

            base.PostProcessResults(results);
        }
    }
}
