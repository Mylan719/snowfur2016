using AutoMapper;
using AutoMapper.QueryableExtensions;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnowFur.BL.Filters;

namespace SnowFur.BL.Queries
{
    public class RoomListQuery : ApplicationQueryBase<RoomListDto>
    {
        public ConventionFilter Filter { get; set; }

        public RoomListQuery(IUnitOfWorkProvider provider) : base (provider)
        {}

        protected override IQueryable<RoomListDto> GetQueryable()
            => Context.Rooms
                .Where(r => r.DateDeleted == null && r.ConventionId == Filter.ConventionId)
                .ProjectTo<RoomListDto>();
    }
}
