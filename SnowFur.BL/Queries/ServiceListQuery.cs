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
using SnowFur.BL.Extensions;

namespace SnowFur.BL.Queries
{
    public class ServiceListQuery : ApplicationQueryBase<ServiceDetailDto>
    {
        public ConventionFilter Filter { get; set; }

        public ServiceListQuery(IUnitOfWorkProvider provider) : base (provider)
        {}

        protected override IQueryable<ServiceDetailDto> GetQueryable()
            => Context.Services
                .Where(r => r.DateDeleted == null && r.ConventionId == Filter.ConventionId)
                .ProjectTo<ServiceDetailDto>();

        protected override void PostProcessResults(IList<ServiceDetailDto> results)
        {   
            foreach (var item in results)
            {
                item.IsChargeAfterSet = item.ChargeAfter.HasValue;
                item.ChargeAfterString = item.ChargeAfter.HasValue
                    ? item.ChargeAfter.Value.ToHtmlPickerFormatDate()
                    : "";
                item.TypeName = ServiceTypeHelper.GetName(item.Type);
            }
            base.PostProcessResults(results);
        }
    }
}
