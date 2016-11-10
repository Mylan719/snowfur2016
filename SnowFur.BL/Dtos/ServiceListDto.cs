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
    public class ServiceListDto : IMapperInstaller
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public decimal Price { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Service, ServiceListDto>();
        }
    }
}
