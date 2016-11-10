using System;
using DotVVM.Framework.Controls;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Services;

namespace SnowFur.BL.Facades
{
    public class ConventionFacade : ApplicationFacadeBase
    {
        private readonly ConventionService conventionService;

        public int ActiveConventionId { get; set; }

        public ConventionFacade(ConventionService conventionService)
        {
            this.conventionService = conventionService;
        }

        public void FillAttendees(GridViewDataSet<AttendeeListItemDto> attendeeDataSet)
        {
            conventionService.FillAttendees(attendeeDataSet, new ConventionFilter {ConventionId = ActiveConventionId});
        }
    }
}
