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
    public class ApplicationUnitOfWork : EntityFrameworkUnitOfWork
    {
        public ApplicationUnitOfWork(IUnitOfWorkProvider provider, Func<DbContext> dbContextFactory, DbContextOptions options) : base(provider, dbContextFactory, options)
        {
        }
        public new ApplicationDbContextContainer Context
        {
            get { return (ApplicationDbContextContainer)base.Context; }
        }
    }
}
