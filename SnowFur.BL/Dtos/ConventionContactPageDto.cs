using AutoMapper;
using SnowFur.BL.Installers;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Dtos
{
    public class ConventionContactPageDto : IMapperInstaller
    {
        public  string Name { get; set; }
        public string ContactInfo { get; set; }
        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Convention, ConventionContactPageDto>();
        }
    }
}