using AutoMapper;

namespace SnowFur.BL.Installers
{
    public interface IMapperInstaller
    {
        void InstallMapping(IMapperConfigurationExpression configuration);
    }
}