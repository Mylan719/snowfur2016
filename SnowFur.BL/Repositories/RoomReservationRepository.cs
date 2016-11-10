using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Repositories
{
    public class RoomReservationRepository : EntityFrameworkRepository<RoomReservation, int>
    {
        public RoomReservationRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public bool Exists(int userId, int roomId) => GetByUserRoom(userId, roomId) != null;

        public RoomReservation GetByUserConvention(int userId, int conventionId) => ((ApplicationDbContextContainer) Context)
           .RoomReservations.SingleOrDefault(rr => rr.UserId == userId && rr.Room.ConventionId == conventionId);

        public RoomReservation GetByUserRoom(int userId, int roomId) => ((ApplicationDbContextContainer) Context)
           .RoomReservations.SingleOrDefault(rr => rr.UserId == userId && rr.RoomId == roomId);
    }
}

