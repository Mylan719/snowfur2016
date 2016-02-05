using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.AspNet.Identity;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using SnowFur.BL;
using SnowFur.BL.Queries;
using SnowFur.BL.UnitOfWork;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.Installers
{
    public class DataAccessInstaller : IWindsorInstaller
    {
        public object MappingConfig { get; private set; }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(

                Component.For<Func<DbContext>>()
                    .Instance(() => new ApplicationDbContextContainer())
                    .LifestyleTransient(),

                Component.For<IUnitOfWorkProvider>()
                    .ImplementedBy<ApplicationUnitOfWorkProvider>()
                    .LifestyleSingleton(),

                Component.For<IUnitOfWorkRegistry>()
                    .Instance(new HttpContextUnitOfWorkRegistry(new ThreadLocalUnitOfWorkRegistry()))
                    .LifestyleSingleton(),

                Classes.FromAssemblyContaining<ApplicationUnitOfWork>()
                    .BasedOn(typeof(ApplicationQueryBase<>))
                    .LifestyleTransient(),

                Classes.FromAssemblyContaining<ApplicationUnitOfWork>()
                    .BasedOn(typeof(IRepository<,>))
                    .LifestyleTransient(),

                Component.For(typeof(IRepository<,>))
                    .ImplementedBy(typeof(EntityFrameworkRepository<,>))
                    .LifestyleTransient(),

                Component.For<ApplicationUserManager>()
                    .LifestyleTransient(),

                Component.For<IUserStore<User, int>>()
                    .ImplementedBy<ApplicationUserStore>()
                    .LifestyleTransient()
            );
        }
    }
}
