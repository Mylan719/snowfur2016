using Microsoft.AspNet.Identity.EntityFramework;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IEntity<int>, IGuardedEntity
    {
        [MaxLength(500)]
        public string AdditionalInfo { get; set; }

        public DateTime? DateDeleted { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual PersonalProfile PersonalProfile { get; set; }
        public virtual ICollection<RoomReservation> RoomReservations { get; set; } = new List<RoomReservation>();
        public virtual ICollection<ServiceOrder> ServiceOrders { get; set; } = new List<ServiceOrder>();
        public virtual ICollection<ConventionPayment> Payments { get; set; } = new List<ConventionPayment>();
    }
}
