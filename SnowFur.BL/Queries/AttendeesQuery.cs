using SnowFur.BL.Dtos;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;

namespace SnowFur.BL.Queries
{
    public class AttendeesQuery : ApplicationQueryBase<AttendeeListItemDto>
    {
        public AttendeesQuery(IUnitOfWorkProvider provider)
            :base (provider)
        { }

        protected override IQueryable<AttendeeListItemDto> GetQueryable()
        {
            throw new NotImplementedException();
        }

        protected override void PostProcessResults(IList<AttendeeListItemDto> results)
        {
            int ordinalNumber = 1;
            foreach (var attendee in results)
            {
                attendee.Count = ordinalNumber++;
            }
        }
    }
}
