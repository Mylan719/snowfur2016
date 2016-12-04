using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.BL.Queries;
using SnowFur.BL.Repositories;
using SnowFur.DAL.Model;
using DotVVM.Framework.Controls;
using SnowFur.DAL.Enums;

namespace SnowFur.BL.Services
{
    public class ServiceOrderService : ApplicationServiceBase
    {
        public Func<UserServiceOrderQuery> QueryFunc { get; set; }
        public ServiceOrderRepository ServiceOrderRepository { get; set; }
        public ConventionRepository ConventionRepository { get; set; }
        public ServiceRepository ServiceRepository { get; set; }
        public UserRepository UserRepository { get; set; }

        public  Func<ServiceType, IServiceOrderStrategy> ServiceOrderStrategyProvider { get; set; }

        public List<UserServiceOrderDto> LoadListForAttendee(ConventionUserFilter filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                var q = QueryFunc();
                q.Filter = filter;
                return q.Execute().ToList();
            }
        }

        public void MakeOrder(int userId, int serviceId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                CheckUserAndService(userId, serviceId);

                if (ServiceOrderRepository.Exists(userId, serviceId))
                {
                    throw new UIException("Složba je už objednaná.");
                }

                var service = ServiceRepository.GetById(serviceId);

                var strategy = ServiceOrderStrategyProvider(service.Type);

                if (!strategy.CanBeOrdered(userId, serviceId))
                {
                    throw new UIException("Službu nie je možné objednať.");
                }

                ServiceOrderRepository.Insert(new ServiceOrder
                {
                    ServiceId = serviceId,
                    UserId = userId
                });
                uow.Commit();
            }
        }

        public void CancelOrder(int userId, int serviceId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                CheckUserAndService(userId, serviceId);
                var serviceOrder = ServiceOrderRepository.GetByUserService(userId, serviceId);
                if (serviceOrder == null)
                {
                    throw new UIException("Neexistuje onjednávka.");
                }
                ServiceOrderRepository.Delete(serviceOrder);
                uow.Commit();
            }

        }

        private void CheckUserAndService(int userId, int serviceId)
        {
            if (ServiceRepository.GetById(serviceId) == null)
            {
                throw new UIException("Neexistuje služba.");
            }
            if (UserRepository.GetById(userId) == null)
            {
                throw new UIException("Neexistuje užívateľ.");
            }
        }
    }
}
