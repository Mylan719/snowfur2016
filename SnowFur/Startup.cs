using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using System;
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
using DotVVM.Framework.Runtime.Compilation.JavascriptCompilation;
using SnowFur.ViewModels;

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

            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;

            // use DotVVM
            DotvvmConfiguration dotvvmConfiguration = app.UseDotVVM(applicationPhysicalPath, true);
            dotvvmConfiguration.ServiceLocator.RegisterSingleton<IViewModelLoader>(() => new WindsorViewModelLoader(container));

            RegisterResources(dotvvmConfiguration);

            AddRoutes(dotvvmConfiguration);

            RegisterMappings();

            RegisterJsCompilables();

            // use static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)
            });
        }

        private static void RegisterResources(DotvvmConfiguration dotvvmConfiguration)
        {
            dotvvmConfiguration.Resources.Register("Site", new StylesheetResource
            {
                Dependencies = { },
                Url = "~/Content/Site.css"
            });
            dotvvmConfiguration.Resources.Register("Bootstrap.Celurean", new StylesheetResource
            {
                Dependencies = { },
                Url = "~/Content/bootstrap.cerulean.css"
            });

            dotvvmConfiguration.Resources.Register("bootstrap", new StylesheetResource
            {
                Dependencies = { },
                Url = "~/Content/bootstrap.css"
            });

            dotvvmConfiguration.Resources.Register("jquery", new ScriptResource
            {
                Dependencies = { },
                Url = "~/Scripts/jquery-2.1.4.js"
            });

            dotvvmConfiguration.Resources.Register("bootstrap-js", new ScriptResource
            {
                Dependencies = { },
                Url = "~/Scripts/bootstrap.js"
            });

        }

        private static void RegisterMappings()
        {
            Mapper.CreateMap<PersonalProfile, PersonalProfileDto>();
            Mapper.CreateMap<PersonalProfileDto, PersonalProfile>();
            Mapper.CreateMap<PersonalProfileForm, PersonalProfileDto>();
            Mapper.CreateMap<PersonalProfileDto, PersonalProfileForm>();
            Mapper.CreateMap<PasswordChangeForm, ChangePasswordDto>();

            Mapper.CreateMap<ReservationForm, RoomReservationDto>();
            Mapper.CreateMap<RoomReservationDto, ReservationForm>();
            
            Mapper.CreateMap<User, UserEmailListDto>();

        }

        private static void RegisterJsCompilables()
        {
            JavascriptTranslator.AddMethodTranslator(typeof(MyProfile), nameof(MyProfile.GetClassForId), new StringJsMethodCompiler("(({0}.ActiveTabId() == {1}) ? \"active\" : \"\")"));
        }

        private void AddRoutes(DotvvmConfiguration dotvvmConfiguration)
        {
            dotvvmConfiguration.RouteTable.Add("Default", "", "Views/default.dothtml");
            dotvvmConfiguration.RouteTable.Add("Register", "register", "Views/register.dothtml");
            dotvvmConfiguration.RouteTable.Add("Attendees", "attendees", "Views/attendees.dothtml");
            dotvvmConfiguration.RouteTable.Add("RegisterFinish", "registerFinish", "Views/registerFinish.dothtml");
            dotvvmConfiguration.RouteTable.Add("PasswordRecoveryStep1", "passwordRecoveryStep1", "Views/passwordRecoveryStep1.dothtml");
            dotvvmConfiguration.RouteTable.Add("PasswordRecoveryStep2", "passwordRecoveryStep2", "Views/passwordRecoveryStep2.dothtml");
            dotvvmConfiguration.RouteTable.Add("MyProfile", "myProfile", "Views/myProfile.dothtml");
            dotvvmConfiguration.RouteTable.Add("PriceList", "priceList", "Views/priceList.dothtml");
            dotvvmConfiguration.RouteTable.Add("Contact", "contact", "Views/contact.dothtml");

            dotvvmConfiguration.RouteTable.Add("AdminReservations", "admin/reservations", "Views/Admin/reservations.dothtml");
            dotvvmConfiguration.RouteTable.Add("AdminBroadcastMail", "admin/broadcastMail", "Views/Admin/broadcastMail.dothtml");

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
}
