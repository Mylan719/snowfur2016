using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class Role : IdentityRole<int, UserRole>
    {
        public const string Admin = nameof(Admin);
    }
}
