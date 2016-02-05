using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;

namespace SnowFur.ViewModels
{
    public class Contact : PageViewModelBase
    {
        public Contact()
        {
            SubpageTitle = "Kontakt";
            RabitBackgroundCssClass = "sf-bg-contact";
        }
    }
}
