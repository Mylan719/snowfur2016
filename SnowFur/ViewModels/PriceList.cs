using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace SnowFur.ViewModels
{
    public class PriceList : PageViewModelBase
    {
        public PriceList()
        {
            SubpageTitle = "Cenník";
            RabitBackgroundCssClass = "sf-bg-cennik";
        }
    }
}
