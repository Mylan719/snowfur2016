using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Facades;
using SnowFur.BL.Dtos;
using DotVVM.Framework.Controls;

namespace SnowFur.ViewModels
{
	public class Attendees : PageViewModelBase
	{
        private ConventionFacade infoFacade;

        public GridViewDataSet<AttendeeListItemDto> AttendeeDataSet { get; set; }
            = new GridViewDataSet<AttendeeListItemDto>() {
                SortExpression = nameof(AttendeeListItemDto.Count),
                PageSize = 60
            };

        public Attendees(ConventionFacade infoFacade)
        {
            this.infoFacade = infoFacade;

            SubpageTitle = "Účastnici";
            RabitBackgroundCssClass = "sf-bg-ucastnici";
        }

        public override Task PreRender()
        {
            infoFacade.FillAttendees(AttendeeDataSet);

            return base.PreRender();
        }
    }
}

