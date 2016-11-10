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
    public class PaymentInfoDto : IMapperInstaller
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ConventionPayment, PaymentInfoDto>()
                .ForMember(m => m.Date, a => a.MapFrom(s => s.DateUpdated ?? s.DateCreated));
        }
    }
}
