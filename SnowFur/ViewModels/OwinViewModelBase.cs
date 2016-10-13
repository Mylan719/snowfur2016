using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnowFur.ViewModels
{
    public abstract class OwinViewModelBase : DotvvmViewModelBase
    {
        [Bind(Direction.None)]
        public IOwinContext OwinContext => Context.GetOwinContext();
        [Bind(Direction.None)]
        public IAuthenticationManager Authentication => OwinContext.Authentication;
        [Bind(Direction.None)]
        public IQueryCollection Query => Context.HttpContext.Request.Query;
    }
}