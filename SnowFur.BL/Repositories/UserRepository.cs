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
    public class UserRepository : EntityFrameworkRepository<User, int>
    {
        public UserRepository(IUnitOfWorkProvider provider) : base(provider)
        { }
        public override void Delete(User entity)
        {
            entity.DateDeleted = DateTime.UtcNow;
        }

        public override void Delete(int id)
        {
            Delete(GetById(id));
        }

        public User GetByUserName ( string userName )
        {
            return Context.Set<User>().SingleOrDefault(user => user.UserName == userName);
        }
    }
}


