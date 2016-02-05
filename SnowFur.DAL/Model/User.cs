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
    public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>, IEntity<int>
    {
        [MaxLength(500)]
        public string AdditionalInfo { get; set; }

        public bool IsDeleted { get; set; }

        public virtual PersonalProfile PersonalProfile { get; set; }
        public virtual RoomReservation RoomReservation { get; set; }

        public virtual List<RoomMateOffer> ReceivedRoomMateOffers { get; set; }
        public virtual List<RoomMateOffer> SentRoomMateOffers { get; set; }
    }
}
