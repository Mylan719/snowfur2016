using Microsoft.AspNet.Identity.EntityFramework;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class UserClaim : IdentityUserClaim<int>, IEntity<int>
    {
    }
}
