using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Queries
{
    public abstract class ApplicationQueryBase<TResult> : EntityFrameworkQuery<TResult>
    {
        public new ApplicationDbContextContainer Context => (ApplicationDbContextContainer)base.Context;

        public ApplicationQueryBase(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}
