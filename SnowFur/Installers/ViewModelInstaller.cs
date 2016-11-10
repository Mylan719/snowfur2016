using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using SnowFur.BL.Services;

namespace SnowFur.Installers
{
    public class ViewModelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(

                Classes.FromAssemblyContaining<ApplicationServiceBase>()
                    .BasedOn<ApplicationServiceBase>()
                    .LifestyleTransient(),

                Classes.FromAssemblyContaining<ApplicationFacadeBase>()
                    .BasedOn<ApplicationFacadeBase>()
                    .LifestyleTransient(),

                Classes.FromThisAssembly()
                    .BasedOn<IDotvvmViewModel>()
                    .LifestyleTransient()
            );
        }
    }
}
