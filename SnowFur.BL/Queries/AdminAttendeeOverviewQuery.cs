using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;

namespace SnowFur.BL.Queries
{
    public class AdminAttendeeOverviewQuery : ApplicationQueryBase<AttendeeAdminListDto>
    {
        public ConventionFilter Filter { get; set; }

        public AdminAttendeeOverviewQuery(IUnitOfWorkProvider provider)
            : base(provider)
        { }

        protected override IQueryable<AttendeeAdminListDto> GetQueryable()
            => Context.Users
                .Include("ServiceOrders")
                .Include("Services")
                .Where(u => u.ServiceOrders.Any( o=> o.Service.DateDeleted == null && o.Service.ConventionId == Filter.ConventionId))
                .ProjectTo<AttendeeAdminListDto>();

        protected override void PostProcessResults(IList<AttendeeAdminListDto> results)
        {
            foreach (var attendeeAdminListDto in results)
            {
                var payment =Context.ConventionPaymens.SingleOrDefault(
                    c => c.DateDeleted == null && Filter.ConventionId == c.Id && c.UserId == attendeeAdminListDto.Id);

                attendeeAdminListDto.AmountPayed = payment?.Amount ?? 0;
                attendeeAdminListDto.DatePaidFormated = $"{payment?.DateCreated:dd. mm. yyyy}";
            }

            base.PostProcessResults(results);
        }
    }
}