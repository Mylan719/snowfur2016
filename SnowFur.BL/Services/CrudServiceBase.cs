using AutoMapper;
using DotVVM.Framework.Controls;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Services
{
    public abstract class CrudServiceBase<TEntity, TKey, TListDTO, TDetailDTO, TQueryFilter> : ApplicationServiceBase
        where TEntity : IEntity<TKey>
        where TDetailDTO : IEntity<TKey>
    {

        protected abstract IQuery<TListDTO> CreateQuery(TQueryFilter filter);

        protected abstract IRepository<TEntity, TKey> GetRepository();


        public void LoadList(GridViewDataSet<TListDTO> resultDataSet, TQueryFilter filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = CreateQuery(filter);
                FillDataSet(resultDataSet, query);
            }
        }

        public TDetailDTO Get(TKey id)
        {
            using (UnitOfWorkProvider.Create())
            {
                var entity = GetRepository().GetById(id);
                return Mapper.Map<TDetailDTO>(entity);
            }
        }

        public abstract TDetailDTO Create();

        public void Save(TDetailDTO item)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var repository = GetRepository();

                TEntity entity;
                var shallInsert = false;
                if (item.Id.Equals(default(TKey)))
                {
                    entity = repository.InitializeNew();
                    shallInsert = true;
                }
                else
                {
                    entity = repository.GetById(item.Id);
                }

                Mapper.Map(item, entity);

                if (shallInsert)
                {
                    repository.Insert(entity);
                }

                uow.Commit();
            }
        }

        public void Remove(TKey id)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                GetRepository().Delete(id);
                uow.Commit();
            }
        }

        public IList<TListDTO> GetAll(TQueryFilter filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = CreateQuery(filter);
                return query.Execute();
            }
        }

    }
}
