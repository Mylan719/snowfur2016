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

namespace SnowFur.BL.Services
{
    public class RoomService : CrudServiceBase<Room,int,RoomListDto,RoomDetailDto,ConventionFilter>
    {
        public Func<RoomListQuery> QeueryFunc { get; set; }
        public RoomRepository Repository { get; set; }

        public int ActiveConventionId { get; set; }

        protected override IQuery<RoomListDto> CreateQuery(ConventionFilter filter)
        {
            var q = QeueryFunc();
            q.Filter = filter;
            return q;
        }

        protected override IRepository<Room, int> GetRepository() => Repository;

        public override RoomDetailDto Create() => new RoomDetailDto() {ConventionId = ActiveConventionId};
    }
}
