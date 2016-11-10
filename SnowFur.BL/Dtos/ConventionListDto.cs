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
    public class ConventionListDto : IMapperInstaller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Convention, ConventionListDto>();
        }
    }
}
