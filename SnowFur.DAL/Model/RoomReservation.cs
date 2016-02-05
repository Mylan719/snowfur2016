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
        [Key]
        [ForeignKey("User")]
        public int Id { get; set; }
        public bool Night1 { get; set; }
        public bool Night2 { get; set; }
        public bool Night3 { get; set; }
        public decimal AmountPayed { get; set; }
        public DateTime? DatePaid { get; set; }

        public bool IsSponsor { get; set; }
        public bool IsDogAttending { get; set; }      
        public bool IsVegetarian { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        public User User { get; set; }

    }
}
