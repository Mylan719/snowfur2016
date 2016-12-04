using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Facades;
using SnowFur.BL.Dtos;

namespace SnowFur.ViewModels
{
    public class PriceList : PageViewModelBase
    {
        private readonly ConventionFacade conventionFacade;

        [Bind(Direction.ServerToClient)]
        public List<PriceListSectionDto> Sections { get; set; }

        public PriceList(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;

            SubpageTitle = "Cenník";
            RabitBackgroundCssClass = "sf-bg-cennik";
        }

        public override Task Init()
        {
            ReportErrors(() =>
            {
                conventionFacade.InitializeActiveConvention();
            });

            return base.Init();

        }
        public override Task PreRender()
        {
            ReportErrors(() =>
            {
                Sections = conventionFacade.GetPriceList();
            });
            return base.PreRender();
        }
    }
}
