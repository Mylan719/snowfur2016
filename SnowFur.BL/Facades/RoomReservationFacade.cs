using SnowFur.BL.Dtos;
using SnowFur.BL.Filters;
using SnowFur.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Queries;
using SnowFur.BL.Repositories;

namespace SnowFur.BL.Facades
{
    public class RoomReservationFacade : CrudFacadeBase<RoomReservation, int, RoomReservationListDto, RoomReservationDto, RoomReservationFilter>
    {
        public RoomReservationRepository RoomReservationRepository { get; set; }
        public UserRepository UserRepository { get; set; }

        public int CurrentUserId { get; set; }

        public Func<RoomReservationsQuery> QueryFunc { get; set; }

        public override RoomReservationDto Create()
        {
            using (UnitOfWorkProvider.Create())
            {
                if (UserRepository.GetById(CurrentUserId) == null)
                {
                    throw new UIException("Neplatné Id užívateľa. pri vytváraní rezervácie izby. Thats not good...");
                }

                return new RoomReservationDto
                {
                    Id = (RoomReservationRepository.GetById(CurrentUserId) == null) ? 0 : CurrentUserId,
                    UserId = CurrentUserId
                };
            }
        }

        public string PayReservation( int userId, decimal amountPayed, DateTime datePaied)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var reservation = RoomReservationRepository.GetById(userId);
                var user = UserRepository.GetById(userId);

                if (user == null)
                {
                    throw new UIException("Neexistuje užívateľ korého registráciu sa snažíte zaplatiť");
                }

                if (reservation == null)
                {
                    new NotImplementedException();
                }
                else
                {
                    new NotImplementedException();
                }
                
                uow.Commit();

                return user.UserName;
            }
        }

        protected override IQuery<RoomReservationListDto> CreateQuery(RoomReservationFilter filter)
        {
            var query = QueryFunc();
            return query;
        }

        protected override IRepository<RoomReservation, int> GetRepository()
        {
            return RoomReservationRepository;
        }

    }
}
