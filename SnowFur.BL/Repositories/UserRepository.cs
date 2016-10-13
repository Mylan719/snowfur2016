using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Repositories
{
    public class UserRepository : GuardedEntityRepositiryBase<User>
    {
        public UserRepository(IUnitOfWorkProvider provider) : base(provider)
        { }

        public User GetByUserName ( string userName )
        {
            return Context.Set<User>().SingleOrDefault(user => user.UserName == userName);
        }
    }
}


