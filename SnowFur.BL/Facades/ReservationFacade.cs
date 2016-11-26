using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Services;

namespace SnowFur.BL.Facades
{
    public class ReservationFacade : ApplicationFacadeBase
    {
        private readonly ConventionService conventionService;

        public ReservationFacade(ConventionService conventionService)
        {
            this.conventionService = conventionService;
        }

        public int CurrentUserId { get; private set; }
        public int ActiveConventionId { get; private set; }

        public void Init(int currentUserId)
        {
            using (conventionService.UnitOfWorkProvider.Create())
            {
                try
                {
                    var convention = conventionService.Repository.GetActive();
                    ActiveConventionId = convention.Id;
                    CurrentUserId = currentUserId;
                }
                catch (Exception ex)
                {
                    throw new UIException("Nie je možné určiť prebiehajúci con. Kontaktujte administrátora.", ex);
                }
            }
        }
    }
}
