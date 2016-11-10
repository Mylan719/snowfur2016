using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Queries
{
    public class ConventionListQuery : ApplicationQueryBase<ConventionListDto>
    {
        public ConventionListQuery(IUnitOfWorkProvider provider) : base(provider)
        { }

        protected override IQueryable<ConventionListDto> GetQueryable()
        {
            var query = Context.Conventions.AsQueryable();

            query = query.Where(c => c.DateDeleted == null);

            return query.ProjectTo<ConventionListDto>();
        }
    }
}
