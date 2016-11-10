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
    public class UserBasicInfoDto : IMapperInstaller
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public void InstallMapping(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserBasicInfoDto>();
        }
    }
}
