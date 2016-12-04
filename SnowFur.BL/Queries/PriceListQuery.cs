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
    public class PriceListQuery : ApplicationQueryBase<PriceListSectionDto>
    {
        public ConventionFilter Filter { get; set; }

        public PriceListQuery(IUnitOfWorkProvider provider) : base(provider)
        { }

        protected override IQueryable<PriceListSectionDto> GetQueryable()
            => Context.Services
                .Where(r => r.DateDeleted == null && r.ConventionId == Filter.ConventionId)
                .GroupBy(k => k.Type, e => new PriceListItemDto
                {
                    ChargeAfter = e.ChargeAfter,
                    Name = e.Name,
                    Price = e.Price
                })
                .OrderByDescending(k=>k.Key)
                .Select(g => new PriceListSectionDto { Type = (int)g.Key, Items = g.ToList() });

        protected override void PostProcessResults(IList<PriceListSectionDto> results)
        {
            foreach (var section in results)
            {
                section.Name = ServiceTypeHelper.GetName(section.Type);

                foreach (var item in section.Items)
                {
                    item.PriceFormated = $"{item.Price:n2}";
                    item.ChargedAfterFormated = $"{(item.ChargeAfter.HasValue ? $"{item.ChargeAfter.Value:dd. MM. yyyy}" : " - ")}";
                }
            }
            base.PostProcessResults(results);
        }
    }
}
