using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;

namespace SnowFur.BL.Repositories
{
    public class ConventionRepository : GuardedEntityRepositiryBase<Convention>
    {
        public ConventionRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}
