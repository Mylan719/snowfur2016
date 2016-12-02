using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Queries
{
    public class UnacomodatedUsersQuery : ApplicationQueryBase<UserBasicInfoDto>
    {
        public ConventionFilter Filter { get; set; }

        public UnacomodatedUsersQuery(IUnitOfWorkProvider provider) : base (provider)
        {}
        protected override IQueryable<UserBasicInfoDto> GetQueryable()
        {
            var query = Context.Users
                .Include("RoomReservation")
                .Include("Room");

            query = query.Where(u=> u.DateDeleted == null && !u.RoomReservations.Any(rr=> rr.Room.ConventionId == Filter.ConventionId));

            return query.ProjectTo<UserBasicInfoDto>();
        }
    }
}
