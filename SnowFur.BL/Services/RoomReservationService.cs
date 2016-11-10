using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Queries;
using SnowFur.BL.Repositories;
using SnowFur.DAL.Model;
using DotVVM.Framework.Controls;

namespace SnowFur.BL.Services
{
    public class RoomReservationService : ApplicationServiceBase
    {
        public Func<ReservedRoomsQuery> ReservedRoomsQuery { get; set; }
        public RoomReservationRepository RoomReservationRepository { get; set; }
        public ConventionRepository ConventionRepository { get; set; }
        public RoomRepository RoomRepository { get; set; }
        public UserRepository UserRepository { get; set; }

        public void LoadListForAttendee(GridViewDataSet<ReservedRoomListDto> resultDataSet, ConventionFilter filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                var q = ReservedRoomsQuery();
                q.Filter = filter;
                FillDataSet(resultDataSet, q);
            }
        }

        public void MakeReservation(int userId, int roomId, int conventionId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                CheckUserAndRoom(userId, roomId);

                if (ConventionRepository.GetById(conventionId) == null)
                {
                    throw new UIException("Neexistuje con.");
                }

                if (RoomReservationRepository.Exists(userId, roomId))
                {
                    throw new UIException("Uzba je už rezervovaná.");
                }

                var currentConventionRoom = RoomReservationRepository.GetByUserConvention(userId, conventionId);
                if (currentConventionRoom != null)
                {
                    RoomReservationRepository.Delete(currentConventionRoom);
                }

                RoomReservationRepository.Insert(new RoomReservation
                {
                    UserId = userId,
                    RoomId = roomId
                });
                uow.Commit();
            }
        }

        public void CancelReservation(int userId, int roomId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                CheckUserAndRoom(userId, roomId);
                var reservation = RoomReservationRepository.GetByUserRoom(userId, roomId);
                if (reservation == null)
                {
                    throw new UIException("Neexistuje rezervácia.");
                }
                RoomReservationRepository.Delete(reservation);
                uow.Commit();
            }

        }

        private void CheckUserAndRoom(int userId, int roomId)
        {
            if (RoomRepository.GetById(roomId) == null)
            {
                throw new UIException("Neexistuje izba.");
            }
            if (UserRepository.GetById(userId) == null)
            {
                throw new UIException("Neexistuje užívateľ.");
            }
        }
    }
}
