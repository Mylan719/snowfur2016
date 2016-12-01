using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Installers;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Dtos
{
    public class ReservedRoomListDto : IMapperInstaller
    {
        public int Id { get; set; }  
        public string Name { get; set; }
        public List<UserBasicInfoDto> AtendeeNames { get; set; }
        public int Capacity { get; set; }
        public int FreeBeds { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Room, ReservedRoomListDto>()
                .ForMember(m => m.AtendeeNames, a => a.MapFrom(s => s.Reservations.Select(r => r.User).ToList()))
                .ForMember(m => m.FreeBeds, a => a.MapFrom(s=> s.Capacity - s.Reservations.Count));
        }
    }
}
