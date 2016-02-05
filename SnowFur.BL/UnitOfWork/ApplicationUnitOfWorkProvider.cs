using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.UnitOfWork
{
    public class ApplicationUnitOfWorkProvider : EntityFrameworkUnitOfWorkProvider
    {
        public ApplicationUnitOfWorkProvider(IUnitOfWorkRegistry registry, Func<DbContext> factory) : base(registry, factory)
        {

        }

        protected override EntityFrameworkUnitOfWork CreateUnitOfWork(Func<DbContext> dbContextFactory, DbContextOptions options)
        {
            return new ApplicationUnitOfWork(this, dbContextFactory, options);
        }
    }
}
