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
      
        //Todo: Atendees here

        private RoomReservationFacade roomReservationFacade;

        public Reservations(RoomReservationFacade roomReservationFacade)
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
            //TODO: fill Atendees 

            return base.PreRender();
        }

        public void ConfirmPayment(int userId)
        {
            PaimentConfirmationForm.Show(userId);
        }
    }
}
