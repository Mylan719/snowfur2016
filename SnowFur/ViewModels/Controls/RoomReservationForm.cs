using System;
using System.Collections.Generic;
using System.Linq;
using SnowFur.BL.Dtos;
using System.Web;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Facades;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using DotVVM.Framework.Runtime.Filters;

namespace SnowFur.ViewModels.Controls
{
    [Authorize]
    public class RoomReservationForm : OwinViewModelBase
    {
        private readonly ReservationFacade reservationFacade;
        private readonly ConventionFacade conventionFacade;
        private MyProfile parent;

        public List<ReservedRoomListDto> Reservations { get; set; }
        public int CurrentUserId => Authentication.User?.Identity?.GetUserId<int>() ?? 0;

        public RoomReservationForm(ReservationFacade reservationFacade,ConventionFacade conventionFacade)
        {
            this.reservationFacade = reservationFacade;
            this.conventionFacade = conventionFacade;
        }

        public void SetParent(MyProfile p)
        {
            parent = p;
        }

        public override Task Init()
        {
            reservationFacade.Init(CurrentUserId);
            conventionFacade.InitializeActiveConvention();
            return base.Init();
        }

        public override Task PreRender()
        {
            parent.ReportErrors(() =>
            {
                Reservations = conventionFacade.GetAdminRoomReservations();
            });

            return base.PreRender();
        }

        public void AddToRoom(int roomId)
        {
            parent.ReportErrors(() =>
            {
                reservationFacade.AddUserToRoom(roomId);
                parent.ActiveTabId = 4;
                parent.SuccessMessage = "Izba rezervovana.";
            });
        }

        public void RemoveFromRoom (int roomId)
        {
            parent.ReportErrors(() =>
            {
                reservationFacade.RemoveUserFromRoom(roomId);
            });
        }
    }
}