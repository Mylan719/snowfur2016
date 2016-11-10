using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Installers;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Dtos
{
    public class ServiceDetailDto : IEntity<int>, IMapperInstaller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Type { get; set; }
        public DateTime? ChargeAfter { get; set; }
        public int ConventionId { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Service, ServiceDetailDto>().ReverseMap();
        }
    }
}
