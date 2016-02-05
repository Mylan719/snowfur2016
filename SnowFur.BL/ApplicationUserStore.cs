using Microsoft.AspNet.Identity.EntityFramework;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.UnitOfWork;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL
{
    public class ApplicationUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {

        public ApplicationUserStore(IUnitOfWorkProvider unitOfWorkProvider) : base((unitOfWorkProvider.GetCurrent() as ApplicationUnitOfWork).Context)
        {
        }
    }
}
