using SnowFur.BL.Dtos;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Filters;

namespace SnowFur.BL.Queries
{
    public class ReservedRoomsQuery : ApplicationQueryBase<ReservedRoomListDto>
    {
        public ConventionFilter Filter { get; set; }

        public ReservedRoomsQuery(IUnitOfWorkProvider provider)
            :base (provider)
        { }

        protected override IQueryable<ReservedRoomListDto> GetQueryable()
            => Context.Rooms
            .Where(r => r.DateDeleted == null && r.ConventionId == Filter.ConventionId)
            .ProjectTo<ReservedRoomListDto>();
    }
}
