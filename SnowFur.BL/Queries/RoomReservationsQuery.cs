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
            throw new NotImplementedException();
        }
    }
}
