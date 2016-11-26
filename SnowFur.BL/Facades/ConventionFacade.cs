using System;
using AutoMapper;
using DotVVM.Framework.Controls;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Queries;
using SnowFur.BL.Services;

namespace SnowFur.BL.Facades
{
    public class ConventionFacade : ApplicationFacadeBase
    {
        private readonly ConventionService conventionService;
        private readonly RoomService roomService;


        public string ActiveConventionName { get; set; }
        public int ActiveConventionId { get; private set; }

        public RoomService RoomService
        {
            get
            {
                return roomService;
            }
        }

        public ConventionFacade(ConventionService conventionService, RoomService roomService)
        {
            this.conventionService = conventionService;
            this.roomService = roomService;
        }

        public void FillAttendees(GridViewDataSet<AttendeeListItemDto> attendeeDataSet)
        {
            conventionService.FillAttendees(attendeeDataSet, new ConventionFilter {ConventionId = ActiveConventionId});
        }

        public void InitializeActiveConvention()
        {
            using (conventionService.UnitOfWorkProvider.Create())
            {
                try
                {
                    var convention = conventionService.Repository.GetActive();
                    ActiveConventionId = convention.Id;
                    ActiveConventionName = convention.Name;
                }
                catch (Exception ex)
                {
                    throw new UIException("Nie je možné určiť prebiehajúci con. Kontaktujte administrátora.", ex);
                }
            }
        }

        public ConventionLandingPageDto GetLandingPage()
        {
            using (conventionService.UnitOfWorkProvider.Create())
            {
                var convention = conventionService.Repository.GetById(ActiveConventionId);
                if(convention == null) { throw new UIException("Neexistuje požadovaný con. Kontaktujte administrátora.");}
                return Mapper.Map<ConventionLandingPageDto>(convention);
            }
        }

        public ConventionContactPageDto GetContactPage()
        {
            using (conventionService.UnitOfWorkProvider.Create())
            {
                var convention = conventionService.Repository.GetById(ActiveConventionId);
                if (convention == null) { throw new UIException("Neexistuje požadovaný con. Kontaktujte administrátora."); }
                return Mapper.Map<ConventionContactPageDto>(convention);
            }
        }

        public void LoadList(GridViewDataSet<ConventionListDto> resultDataSet)
        {
            conventionService.LoadList(resultDataSet, new DefaultFilter());
        }

        public void FillAttendees(GridViewDataSet<AttendeeListItemDto> attendeeDataSet, ConventionFilter filter)
        {
            conventionService.FillAttendees(attendeeDataSet, filter);
        }


        public void Save(ConventionDetailDto item)
        {
            conventionService.Save(item);
        }

        public ConventionDetailDto GetConventionDetail(int id)
        {
            return conventionService.Get(id);
        }

        public void ActivateConvention(int conventionId)
        {
            using (var uow = conventionService.UnitOfWorkProvider.Create())
            {
                conventionService.Repository.SetActive(conventionId);
                uow.Commit();
            }
        }

        public void FillAdminAtendees(GridViewDataSet<AttendeeAdminListDto> attendeeDataSet)
        {
            conventionService.FillAdminAtendees(attendeeDataSet, new ConventionFilter { ConventionId = ActiveConventionId});
        }

        public void FillRooms(GridViewDataSet<RoomListDto> roomsDataSet)
        {
            roomService.LoadList(roomsDataSet, new ConventionFilter { ConventionId = ActiveConventionId });
        }

        public void Save(RoomDetailDto roomDetil)
        {
            roomService.Save(roomDetil);
        }

        public int GetReservationCount(int roomId)
        {
            using (conventionService.UnitOfWorkProvider.Create())
            {
                return roomService.Repository.GetRoomReservationCount(roomId);
            }
        }

        public RoomDetailDto GetRoomDetail(int id) => roomService.Get(id);

        public void RemoveRoom(int roomId) => roomService.Remove(roomId);
    }
}
