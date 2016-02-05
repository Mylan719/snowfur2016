using DotVVM.Framework.Controls;
using SnowFur.BL.Dtos;
using SnowFur.BL.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Facades
{
    public class ConventionInfoFacade : ApplicationFacadeBase
    {
        public Func<AttendeesQuery> AttendeesQueryFunc { get; set; }

        public void FillAttendees(GridViewDataSet<AttendeeListItemDto> dataSet)
        {
            using (UnitOfWorkProvider.Create())
            {
                FillDataSet<AttendeeListItemDto>(dataSet, AttendeesQueryFunc());
            }
        }
    }
}
