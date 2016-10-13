using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.DAL.Model
{
    public class RoomReservation : IEntity<int>
    { 
        public int Id { get; set; }

        public int? UserId { get; set; }
        public int? RoomId { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }
    }
}
