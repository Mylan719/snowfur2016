using System;
using AutoMapper;
using DotVVM.Framework.Controls;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Queries;
using SnowFur.BL.Services;
using System.Collections.Generic;

namespace SnowFur.BL.Facades
{
    public class ConventionFacade : ApplicationFacadeBase
    {
        private readonly ConventionService conventionService;
        private readonly RoomService roomService;
        private readonly RoomReservationService roomReservationService;
        private readonly ServiceService serviceService;
        private readonly PaymentService paymentService;

        public string ActiveConventionName { get; set; }
        public int ActiveConventionId { get; private set; }

        public RoomService RoomService
        {
            get
            {
                return roomService;
            }
        }

        public ConventionFacade(ConventionService conventionService, RoomService roomService, RoomReservationService roomReservationService, ServiceService serviceService, PaymentService paymentService)
        {
            this.conventionService = conventionService;
            this.roomService = roomService;
            this.roomReservationService = roomReservationService;
            this.serviceService = serviceService;
            this.paymentService = paymentService;
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

        public void FillConventions(GridViewDataSet<ConventionListDto> resultDataSet)
        {
            conventionService.LoadList(resultDataSet, new DefaultFilter());
        }

        public void FillAttendees(GridViewDataSet<AttendeeListItemDto> attendeeDataSet)
        {
            conventionService.FillAttendees(attendeeDataSet, new ConventionFilter { ConventionId = ActiveConventionId });
        }

       
        public void FillAdminAtendees(GridViewDataSet<AttendeeAdminListDto> attendeeDataSet)
        {
            conventionService.FillAdminAtendees(attendeeDataSet, new ConventionFilter { ConventionId = ActiveConventionId});
        }

        public void FillServices(GridViewDataSet<ServiceDetailDto> serviceDataSet)
        {
            serviceService.LoadList(serviceDataSet, new ConventionFilter { ConventionId = ActiveConventionId });
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

        public int GetOrderCount(int serviceId)
            => serviceService.GetOrdersCount(serviceId);

        public RoomDetailDto GetRoomDetail(int id) => roomService.Get(id);

        public void RemoveRoom(int roomId) => roomService.Remove(roomId);

        public List<ReservedRoomListDto> GetAdminRoomReservations()
        {
            return roomReservationService.GetReservedRooms(new ConventionFilter { ConventionId = ActiveConventionId });
        }

        public void RemoveUserFromRoom(int userId, int roomId)
        {
            roomReservationService.CancelReservation(userId, roomId);
        }

        public void AddUserToRoom(int userId, int roomId)
        {
            roomReservationService.MakeReservation(userId, roomId, ActiveConventionId);
        }

        public List<UserBasicInfoDto> GetUnacomodatedUsers()
        {
            return roomReservationService.GetUnacomodatedUsers(new ConventionFilter { ConventionId = ActiveConventionId });
        }

        public void Save(ServiceDetailDto service)
        {
            service.ConventionId = ActiveConventionId;
            serviceService.Save(service);
        }

        public List<ServiceTypeDto> GetServiceTypes()
            => serviceService.GetServiceTypes();

        public void RemoveService(int serviceId) => serviceService.Remove(serviceId);

        public List<PriceListSectionDto> GetPriceList() 
            => serviceService.GetPriceList(new ConventionFilter { ConventionId = ActiveConventionId });

        public void SetUserPayment(int userId, decimal amount)
        {
            paymentService.SetPayment(userId, ActiveConventionId, amount);
        }

        public PaymentInfoDto GetUserPayment(int userId)
        {
            return paymentService.GetPayment(userId, ActiveConventionId);
        }
    }
}
