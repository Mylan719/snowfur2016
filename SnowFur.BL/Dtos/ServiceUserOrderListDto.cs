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
    public class ServiceUserOrderListDto : IMapperInstaller
    {
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public int ServiceType { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }

        public bool IsActive { get; set; }
        public bool IsOrdered { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ServiceOrder, ServiceUserOrderListDto>();
        }
    }
}
