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
    public class RoomQuery : ApplicationQueryBase<RoomDto>
    {
        public string ConventionName { get; set; }

        public RoomQuery(IUnitOfWorkProvider provider) : base (provider)
        {}
        protected override IQueryable<RoomDto> GetQueryable()
            => Context.Rooms
            .Where(r => r.Convention.Name == ConventionName)
            .Select(r => new RoomDto { Name = r.Name, BedCount = r.Capacity });
        
    }
}
