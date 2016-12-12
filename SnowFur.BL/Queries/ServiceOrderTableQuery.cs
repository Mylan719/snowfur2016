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
    public class ServiceOrderTableQuery : ApplicationQueryBase<UserServiceOrderDto>
    {
        public ConventionUserFilter Filter { get; set; }
      
        public ServiceOrderTableQuery(IUnitOfWorkProvider provider)
            :base (provider)
        { }

        protected override IQueryable<UserServiceOrderDto> GetQueryable()
            => Context.Services
            .Include("ServiceOrders")
            .Where(s => s.DateDeleted == null && s.ConventionId == Filter.ConventionId)
            .ProjectTo<UserServiceOrderDto>();

        protected override void PostProcessResults(IList<UserServiceOrderDto> results)
        {
            foreach(var r in results)
            {
                var order = Context.ServiceOrders.FirstOrDefault(so => so.ServiceId == r.ServiceId && so.UserId == Filter.UserId);
                r.IsOrdered = order != null;
            }

            base.PostProcessResults(results);
        }
    }
}
