using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Services;
using SnowFur.BL.Filters;

namespace SnowFur.BL.Facades
{
    public class ReservationFacade : ApplicationFacadeBase
    {
        private readonly ConventionService conventionService;
        private readonly RoomReservationService roomReservationService;
        private readonly ServiceOrderService serviceOrderService;

        public ReservationFacade(ConventionService conventionService, RoomReservationService roomReservationService, ServiceOrderService serviceOrderService)
        {
            this.conventionService = conventionService;
            this.roomReservationService = roomReservationService;
            this.serviceOrderService = serviceOrderService;
        }

        public int CurrentUserId { get; private set; }
        public int ActiveConventionId { get; private set; }

        public void Init(int currentUserId)
        {
            using (conventionService.UnitOfWorkProvider.Create())
            {
                try
                {
                    var convention = conventionService.Repository.GetActive();
                    ActiveConventionId = convention.Id;
                    CurrentUserId = currentUserId;
                }
                catch (Exception ex)
                {
                    throw new UIException("Nie je možné určiť prebiehajúci con. Kontaktujte administrátora.", ex);
                }
            }
        }

        public List<UserServiceOrderDto> GetServiceOrders()
            => serviceOrderService.GetServiceOrders(new ConventionUserFilter { ConventionId = ActiveConventionId, UserId = CurrentUserId });
        

        public void AddUserToRoom(int roomId)
        {
            if (roomReservationService.IsRoomFull(roomId))
            {
                throw new UIException("Nie je mozne vas ubytovat na tuto izbu, kapacita je vycerpana.");
            }

            roomReservationService.MakeReservation(CurrentUserId, roomId, ActiveConventionId);
        }

        public void RemoveUserFromRoom(int roomId)
        {        
            roomReservationService.CancelReservation(CurrentUserId, roomId);
        }

        public void OrderService(int serviceId)
        {
            serviceOrderService.MakeOrder(CurrentUserId, serviceId);
        }

        public void UnorderService(int serviceId)
        {
            serviceOrderService.CancelOrder(CurrentUserId, serviceId);
        }
    }
}
