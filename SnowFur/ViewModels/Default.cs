using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Facades;

namespace SnowFur.ViewModels
{
    public class Default : PageViewModelBase
    {
        private readonly ConventionFacade conventionFacade;

        [Bind(Direction.ServerToClient)]
        public ConventionLandingPageDto Page { get; set; }

        public Default(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
            this.conventionFacade.InitializeActiveConvention();

        }

        public override Task PreRender()
        {
            ReportErrors(() =>
            { 
                Page = conventionFacade.GetLandingPage();
                LogoText = Page.Name;
            });

            return base.PreRender();
        }
    }
}
