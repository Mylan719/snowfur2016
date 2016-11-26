using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.Runtime.Filters;

namespace SnowFur.ViewModels.Admin
{
    [Authorize("admin")]
    public class RoomsAdminViewModel : PageViewModelBase
    {

    }
}