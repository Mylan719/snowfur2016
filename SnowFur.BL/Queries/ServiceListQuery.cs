using AutoMapper;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnowFur.BL.Filters;

namespace SnowFur.BL.Queries
{
    public class ServiceListQuery : ApplicationQueryBase<ServiceListDto>
    {
        public ConventionFilter Filter { get; set; }

        public ServiceListQuery(IUnitOfWorkProvider provider) : base (provider)
        {}

        protected override IQueryable<ServiceListDto> GetQueryable()
            => Context.Services
                .Where(r => r.DateDeleted == null && r.ConventionId == Filter.ConventionId)
                .ProjectTo<ServiceListDto>();
    }
}
