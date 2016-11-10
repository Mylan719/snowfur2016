using SnowFur.BL.Installers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SnowFur.Installers
{
    public static class AutoMapperInstaller
    {
        public static void Install()
        {
            SnowFur.BL.Installers.AutoMapperInstaller.Configuration.CreateMaps(Assembly.GetExecutingAssembly());

            SnowFur.BL.Installers.AutoMapperInstaller.InstallCore();
        }
    }
}