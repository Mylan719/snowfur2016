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
using SnowFur.DAL.Enums;
using DotVVM.Framework.ViewModel;

namespace SnowFur.BL.Dtos
{
    public class ServiceDetailDto : IEntity<int>, IMapperInstaller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }

        public bool IsChargeAfterSet { get; set; }
        public string ChargeAfterString { get; set; }

        [Bind(Direction.None)]
        public DateTime? ChargeAfter { get; set; }

        public int ConventionId { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Service, ServiceDetailDto>()
                .ForMember(m => m.Type, a => a.MapFrom(s => (int)s.Type));

            configuration.CreateMap<ServiceDetailDto, Service>()
               .ForMember(m => m.Type, a => a.MapFrom(s => (ServiceType)s.Type));
        }
    }
}
