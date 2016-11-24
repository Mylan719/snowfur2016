using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using System;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using SnowFur.Installers;
using DotVVM.Framework.Runtime;
using SnowFur.DAL.Model;
using SnowFur.BL.Dtos;
using AutoMapper;
using SnowFur.ViewModels.Controls;
using DotVVM.Framework.ViewModel.Serialization;
using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SnowFur.BL.Installers;

[assembly: OwinStartup(typeof(SnowFur.Startup))]
namespace SnowFur
{
    public class Startup
    {
        private WindsorContainer container;

        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/")
            });

            SetupContainer();
            SetupMapper();

            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, true, builder: (b) => {
                b.Services.AddSingleton<IViewModelLoader>((sp) => new WindsorViewModelLoader(sp, container));
            });

            //exceptin... dunno why... well screw that.
            //app.UseWebApi(new MyHttpConfiguration());

#if DEBUG
            dotvvmConfiguration.Debug = true;
#endif

            // use static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)
            });
        }

        

        private static void SetupMapper()
        {
            Installers.AutoMapperInstaller.Install();
        }

        private void SetupContainer()
        {
            container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();
            container.Install(new DataAccessInstaller());
            container.Install(new ServicesInstaller());
            container.Install(new ViewModelInstaller());
        }
    }

    public class MyHttpConfiguration : HttpConfiguration
    {
        public MyHttpConfiguration()
        {
            //ConfigureRoutes();
            ConfigureJsonSerialization();
        }

        //private void ConfigureRoutes()
        //{
        //    Routes.MapHttpRoute(
        //        name: "DefaultApi",
        //        routeTemplate: "api/{controller}/{id}",
        //        defaults: new { id = RouteParameter.Optional }
        //    );
        //}

        private void ConfigureJsonSerialization()
        {
            var jsonSettings = Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
