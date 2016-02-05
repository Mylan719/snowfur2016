using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Facades;
using SnowFur.BL.Dtos;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.ViewModels.Controls;
using SnowFur.BL.Repositories;

namespace SnowFur.ViewModels.Admin
{
    [Authorize("admin")]
    public class Reservations : PageViewModelBase
    {
        public PaimentConfirmationForm PaimentConfirmationForm { get; set; }

        public GridViewDataSet<RoomReservationListDto> Attendees { get; set; } = new GridViewDataSet<RoomReservationListDto>() { SortExpression = nameof(RoomReservationListDto.UserName), PageSize = 60 };

        private RoomReservationFacade roomReservationFacade;

        public Reservations(RoomReservationFacade roomReservationFacade) : base()
        {
            this.roomReservationFacade = roomReservationFacade;

            SubpageTitle = "Rezervácie";
        }

        public override Task Init()
        {
            PaimentConfirmationForm.Context = Context;
            PaimentConfirmationForm.ParentViewModel = this;

            return base.Init();
        }

        public override Task PreRender()
        {
            roomReservationFacade.LoadList(Attendees, new BL.Filters.RoomReservationFilter());

            return base.PreRender();
        }

        public void ConfirmPayment(int userId)
        {
            PaimentConfirmationForm.Show(userId);
        }
    }
}
