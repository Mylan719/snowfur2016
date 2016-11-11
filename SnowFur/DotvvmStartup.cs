using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using DotVVM.Framework.ResourceManagement;
using DotVVM.Framework.Compilation.Javascript;
using SnowFur.ViewModels;
using System;

namespace SnowFur
{
    public class DotvvmStartup : IDotvvmStartup
    {
        // For more information about this class, visit https://dotvvm.com/docs/tutorials/basics-project-structure
        public void Configure(DotvvmConfiguration config, string applicationPath)
        {
            RegisterResources(config);
            AddRoutes(config);
            RegisterControls(config);
            RegisterJsCompilables();
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

        private void RegisterControls(DotvvmConfiguration config)
        {
            config.Markup.Controls.Add(new DotvvmControlConfiguration() { TagPrefix = "cc", TagName = "LoginPanel", Src= "Views/Controls/LoginPanel.dotcontrol" });
            config.Markup.Controls.Add(new DotvvmControlConfiguration() { TagPrefix = "cc", TagName = "PasswordChangeForm", Src = "Views/Controls/PasswordChangeForm.dotcontrol" });
            config.Markup.Controls.Add(new DotvvmControlConfiguration() { TagPrefix = "cc", TagName = "PaimentConfirmationForm", Src = "Views/Controls/PaimentConfirmationForm.dotcontrol" });
            config.Markup.Controls.Add(new DotvvmControlConfiguration() { TagPrefix = "cc", TagName = "PersonalProfileForm", Src = "Views/Controls/PersonalProfileForm.dotcontrol" });
            config.Markup.Controls.Add(new DotvvmControlConfiguration() { TagPrefix = "cc", TagName = "ReservationForm", Src = "Views/Controls/ReservationForm.dotcontrol" });
        }

        private static void RegisterResources(DotvvmConfiguration dotvvmConfiguration)
        {
            dotvvmConfiguration.Resources.Register("Site", new StylesheetResource { Url = "~/Content/Site.css" });
            dotvvmConfiguration.Resources.Register("Bootstrap.Celurean", new StylesheetResource { Url = "~/Content/bootstrap.cerulean.css" });
            dotvvmConfiguration.Resources.Register("bootstrap", new StylesheetResource { Url = "~/Content/bootstrap.css" });

            dotvvmConfiguration.Resources.Register("jquery", new ScriptResource { Url = "~/Scripts/jquery-2.1.4.js" });
            dotvvmConfiguration.Resources.Register("bootstrap-js", new ScriptResource { Url = "~/Scripts/bootstrap.js" });
        }

        private static void RegisterJsCompilables()
        {
            JavascriptTranslator.AddMethodTranslator(typeof(MyProfile), nameof(MyProfile.GetClassForId), new StringJsMethodCompiler("(({0}.ActiveTabId() == {1}) ? \"active\" : \"\")"));
        }
    }
}
