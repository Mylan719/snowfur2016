using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SnowFur.BL.Mailer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnowFur.Controllers;

namespace SnowFur.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<MailerService>().LifestyleSingleton()
            );

            container.Register(
                Component.For<RequiredController>().LifestyleTransient()
            );
        }
    }
}
