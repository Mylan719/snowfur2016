using Riganti.Utils.Infrastructure.Core;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Repositories
{
    public class ConventionPaymentRepository : GuardedEntityRepositiryBase<ConventionPayment>
    {
        public ConventionPaymentRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public ConventionPayment GetByUserConvention(int userId, int conventionId) => 
            ((ApplicationDbContextContainer) Context).ConventionPaymens
            .SingleOrDefault(p 
                => p.DateDeleted != null 
                && p.UserId == userId
                && p.ConventionId == conventionId);
    }
}
