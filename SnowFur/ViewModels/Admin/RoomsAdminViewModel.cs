using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Runtime.Filters;
using SnowFur.BL.Dtos;
using SnowFur.BL.Facades;
using DotVVM.Framework.ViewModel;

namespace SnowFur.ViewModels.Admin
{
    [Authorize("admin")]
    public class RoomsAdminViewModel : PageViewModelBase
    {
        private readonly ConventionFacade conventionFacade;

        public GridViewDataSet<RoomListDto> Rooms { get; set; } = new GridViewDataSet<RoomListDto>()
        {
            SortExpression = nameof(RoomListDto.Name),
            PageSize = 100,
            PrimaryKeyPropertyName = nameof(RoomListDto.Id)
        };

        public int? DeletePropmptId { get; set; }

        public RoomDetailDto Detail { get; set; }

        [Bind(Direction.ServerToClient)]
        public string ConventionName => conventionFacade.ActiveConventionName;

        public RoomsAdminViewModel(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
            LogoText = "Convention Admin";
            SubpageTitle = "Izby";
        }

        public override Task Init()
        {
            conventionFacade.InitializeActiveConvention();
            Detail = GetNewRoom();
            return base.Init();
        }

        public override Task PreRender()
        {
            conventionFacade.FillRooms(Rooms);
            return base.PreRender();
        }

        public void Edit(int roomId)
        {
            ReportErrors(() =>
            {
                Detail = conventionFacade.GetRoomDetail(roomId);
                Rooms.EditRowId = roomId;
            });
        }

        public void Add()
        {
            Detail = GetNewRoom();
        }

       
        public void SaveEdit()
        {
            ReportErrors(() =>
            {
                if (Detail != null)
                {
                    conventionFacade.Save(Detail);
                }
                Detail = GetNewRoom();
                Rooms.EditRowId = null;
            });
        }

        public void CancelEdit()
        {
            Detail = GetNewRoom();
            Rooms.EditRowId = null;
        }

        public void Delete(int roomId, bool force)
        {
            ReportErrors(() =>
            {
                if (!force && conventionFacade.GetReservationCount(roomId) > 0)
                {
                    DeletePropmptId = roomId;
                    return;
                }
                conventionFacade.RemoveRoom(roomId);
                DeletePropmptId = null;
            });
        }


        private RoomDetailDto GetNewRoom() => new RoomDetailDto { ConventionId = conventionFacade.ActiveConventionId };
    }
}