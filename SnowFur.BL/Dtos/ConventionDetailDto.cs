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
    public class ConventionDetailDto : IMapperInstaller, IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nights { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }
        public string MapCoordinates { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Convention, ConventionDetailDto>().ReverseMap();
        }
    }
}
