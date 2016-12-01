﻿using System;
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
using SnowFur.BL.Queries;
using SnowFur.ViewModels.Controls;
using SnowFur.BL.Dtos;

namespace SnowFur.ViewModels.Admin
{
    [Authorize("admin")]
    public class RoomReservations : PageViewModelBase
    {
        private ConventionFacade conventionFacade;

        public List<ReservedRoomListDto> Reservations { get; set; } = new List<ReservedRoomListDto>();

        public RoomReservations(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
            conventionFacade.InitializeActiveConvention();

            SubpageTitle = "RezervácieIzieb";
            LogoText = "Convention Admin";
        }

        public override Task PreRender()
        {
            ReportErrors(() =>
            {
                Reservations = conventionFacade.GetAdminRoomReservations();
            });
            return base.PreRender();
        }

        public void RemoveUserFromRoom(int userId, int roomId)
        {
            ReportErrors(() =>
            {
                conventionFacade.RemoveUserFromRoom(userId, roomId);
            });
        }
    }
}
