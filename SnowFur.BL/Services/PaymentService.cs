using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Dtos;
using SnowFur.BL.Repositories;
using SnowFur.DAL.Model;

namespace SnowFur.BL.Services
{
    public class PaymentService : ApplicationServiceBase
    {
        public ConventionPaymentRepository PaymentRepository { get; set; }
        public UserRepository UserRepository { get; set; }

        public int ActiveConventionId { get; set; }

        public void SetPayment(int userId, decimal amount)
        {
            using(var uow = UnitOfWorkProvider.Create())
            {
                if (UserRepository.GetById(userId) == null)
                {
                    throw new UIException("Neexistuje užívateľ.");   
                }

                var payment = PaymentRepository.GetByUserConvention(userId, ActiveConventionId);

                if (payment != null)
                {
                    payment.Amount = amount;
                    PaymentRepository.Update(payment);
                }
                else
                {
                    payment = new ConventionPayment
                    {
                        ConventionId = ActiveConventionId,
                        UserId = userId,
                        Amount = amount
                    };
                    PaymentRepository.Insert(payment);
                }
                uow.Commit();
            }
        }

        public PaymentInfoDto GetPayment(int userId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if (UserRepository.GetById(userId) == null)
                {
                    throw new UIException("Neexistuje užívateľ.");
                }

                var payment = PaymentRepository.GetByUserConvention(userId, ActiveConventionId);

                return Mapper.Map<PaymentInfoDto>(payment);
            }
        }
    }
}
