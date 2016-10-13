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
    public class PersonalProfileRepository : GuardedEntityRepositiryBase<PersonalProfile>
    {
        public PersonalProfileRepository(IUnitOfWorkProvider provider) : base(provider)
        { }
       
        public PersonalProfile GetByUserId (int userId)
        {
            return Context.Set<PersonalProfile>().SingleOrDefault(profile => profile.Id == userId);
        }
    }
}


