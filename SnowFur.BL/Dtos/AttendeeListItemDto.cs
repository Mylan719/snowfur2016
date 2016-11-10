using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SnowFur.BL.Installers;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Dtos
{
    public class AttendeeListItemDto : IMapperInstaller
    {
        public int Count { get; set; }
        public string NickName { get; set; }
        public string Color { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, AttendeeListItemDto>()
                .ForMember(m => m.NickName, a => a.MapFrom(s => s.UserName));
        }
    }
}
