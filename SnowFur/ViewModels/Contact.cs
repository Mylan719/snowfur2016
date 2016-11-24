using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Dtos;
using SnowFur.BL.Facades;

namespace SnowFur.ViewModels
{
    public class Contact : PageViewModelBase
    {
        private readonly ConventionFacade conventionFacade;

        [Bind(Direction.ServerToClient)]
        public ConventionContactPageDto Page { get; set; }

        public Contact(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
            conventionFacade.InitializeActiveConvention();
            SubpageTitle = "Kontakt";
            RabitBackgroundCssClass = "sf-bg-contact";
        }

        public override Task Init()
        {
            ReportErrors(() =>
            {
                Page = conventionFacade.GetContactPage();
                LogoText = Page.Name;
            });
            return base.Init();
        }
    }
}
