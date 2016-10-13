using Castle.Windsor;
using DotVVM.Framework.Runtime;
using DotVVM.Framework.ViewModel.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.Installers
{
    public class WindsorViewModelLoader : DefaultViewModelLoader
    {
        private readonly WindsorContainer container;

        public WindsorViewModelLoader(IServiceProvider provider, WindsorContainer container)
            :base(provider)
        {
            this.container = container;
        }

        protected override object CreateViewModelInstance(Type viewModelType)
        {
            return container.Resolve(viewModelType);
        }

        public override void DisposeViewModel(object instance)
        {
            container.Release(instance);
            base.DisposeViewModel(instance);
        }
    }
}
