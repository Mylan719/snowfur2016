using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Utils;
using Riganti.Utils.Infrastructure.Core;

namespace SnowFur.BL.Repositories
{
    public class ConventionRepository : GuardedEntityRepositiryBase<Convention>
    {
        public ConventionRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public void SetActive(int conventionId)
        {
            //there can be only one
            var activeCons = Context.CastTo<ApplicationDbContextContainer>().Conventions.Where(c => c.IsActive).ToList();
            activeCons.ForEach(c =>
            {
                c.IsActive = false;
            });

            var convention = GetById(conventionId);
            convention.IsActive = true;
        }

        public Convention GetActive() => 
            Context.CastTo<ApplicationDbContextContainer>()
            .Conventions
            .SingleOrDefault(c => c.IsActive);
    }
}
