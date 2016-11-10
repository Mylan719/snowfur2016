using Riganti.Utils.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Repositories
{
    public abstract class GuardedEntityRepositiryBase<TEntity> : EntityFrameworkRepository<TEntity, int>
       where TEntity : class, IGuardedEntity, IEntity<int>, new()
    {
        public GuardedEntityRepositiryBase(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public override IList<TEntity> GetByIds(IEnumerable<int> ids, params Expression<Func<TEntity, object>>[] includes)
        {
            return base.GetByIds(ids, includes).Where(e=> e.DateDeleted == null).ToList();
        }

        public override void Delete(TEntity entity)
        {
            entity.DateDeleted = DateTime.UtcNow;
        }

        public override void Insert(TEntity entity)
        {
            entity.DateCreated = DateTime.UtcNow;
            base.Insert(entity);
        }

        public override void Update(TEntity entity)
        {
            entity.DateUpdated = DateTime.UtcNow;
            base.Update(entity);
        }
    }
}
