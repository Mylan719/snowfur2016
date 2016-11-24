using System;
using AutoMapper;
using SnowFur.BL.Installers;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Dtos
{
    public class ConventionLandingPageDto : IMapperInstaller
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Activities { get; set; }
        public string PlaceName { get; set; }
        public decimal GpsLatitude { get; set; }
        public decimal GpsLongitude { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Convention, ConventionLandingPageDto>();
        }
    }
}