using AutoMapper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SnowFur.BL.Dtos;

namespace SnowFur.BL.Installers
{
    public static class AutoMapperInstaller
    {
        public static MapperConfigurationExpression Configuration { get; private set; } =
            new MapperConfigurationExpression();

        public static void InstallCore()
        {
            Configuration.CreateMaps(Assembly.GetExecutingAssembly());

            Mapper.Initialize(Configuration);
        }

        public static void CreateMaps(this MapperConfigurationExpression configuration, Assembly assembly)
        {
            var instances = assembly.GetExportedTypes()
                .Where(
                    type =>
                        !type.IsAbstract && !type.IsInterface &&
                        type.GetInterfaces().Any(interfaces => typeof(IMapperInstaller).IsAssignableFrom(type)))
                .Select(type => (IMapperInstaller) Activator.CreateInstance(type));

            foreach (var instance in instances)
            {
                instance.InstallMapping(Configuration);
            }
        }
    }
}
