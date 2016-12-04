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
    public class UserServiceOrderDto : IMapperInstaller
    {
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsOrdered { get; set; }
        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Service, UserServiceOrderDto>()
                .ForMember(m => m.IsOrdered, a => a.Ignore())
                .ForMember(m => m.ServiceId, a => a.MapFrom(s => s.Id));
        }
    }
}
