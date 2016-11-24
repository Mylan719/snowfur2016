using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Queries;
using SnowFur.BL.Repositories;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Services
{
    public class ConventionService : CrudServiceBase<Convention,int,ConventionListDto,ConventionDetailDto, DefaultFilter>
    {
        public Func<ConventionListQuery> ListQueryFunc { get; set; }
        public Func<AttendeesQuery> AttendeesQueryFunc { get; set; }
        public Func<AdminAttendeeOverviewQuery> AdminAttendeesQueryFunc { get; set; }

        public ConventionRepository Repository { get; set; }

        protected override IQuery<ConventionListDto> CreateQuery(DefaultFilter filter)
            => ListQueryFunc();

        protected override IRepository<Convention, int> GetRepository()
            => Repository;

        public override ConventionDetailDto Create()
            => new ConventionDetailDto();

        public void FillAttendees(GridViewDataSet<AttendeeListItemDto> attendeeDataSet, ConventionFilter filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                var q = AttendeesQueryFunc();
                q.Filter = filter;

                FillDataSet(attendeeDataSet, q);
            }
        }

        public void FillAdminAtendees(GridViewDataSet<AttendeeAdminListDto> attendeeDataSet, ConventionFilter filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                var q = AdminAttendeesQueryFunc();
                q.Filter = filter;

                FillDataSet(attendeeDataSet, q);
            }
        }
    }
}
