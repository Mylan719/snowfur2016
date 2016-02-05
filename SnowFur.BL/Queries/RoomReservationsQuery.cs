using AutoMapper;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Queries
{
    public class RoomReservationsQuery : ApplicationQueryBase<RoomReservationListDto>
    {
        public RoomReservationsQuery(IUnitOfWorkProvider provider) : base (provider)
        {}
        protected override IQueryable<RoomReservationListDto> GetQueryable()
        {
            return Context.Users.Select(user => new RoomReservationListDto
            {
                Id = user.Id,
                FirstName = (user.PersonalProfile != null) ? user.PersonalProfile.FirstName : "nezadané",
                LastName = (user.PersonalProfile != null) ? user.PersonalProfile.LastName : "nezadané",
                UserName = user.UserName,
                Night1 = (user.RoomReservation != null) ? user.RoomReservation.Night1 : false,
                Night2 = (user.RoomReservation != null) ? user.RoomReservation.Night2 : false,
                Night3 = (user.RoomReservation != null) ? user.RoomReservation.Night3 : false,
                IsDogAttending = (user.RoomReservation != null) ? user.RoomReservation.IsDogAttending : false,
                IsVegetarian = (user.RoomReservation != null) ? user.RoomReservation.IsVegetarian : false,
                IsSponsor = (user.RoomReservation != null) ? user.RoomReservation.IsSponsor : false,
                AmountPayed = (user.RoomReservation != null) ? user.RoomReservation.AmountPayed : 0,
                Note = (user.RoomReservation != null) ? user.RoomReservation.Note : "",
                DatePaid = (user.RoomReservation != null) ? user.RoomReservation.DatePaid : null,
            });
        }
    }
}
