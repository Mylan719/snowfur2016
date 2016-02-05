using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Dtos
{
    public class RoomReservationDto : IEntity<int>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Night1 { get; set; }
        public bool Night2 { get; set; }
        public bool Night3 { get; set; }
        public bool IsVegetarian { get; set; }
        public bool IsSponsor { get; set; }
        public bool IsDogAttending { get; set; }
        public decimal AmountPayed { get; set; }
        public DateTime? DatePaid { get; set; }
        public decimal AmountToPay => PaymentHelper.CalculateSumToPay(Night1, Night2, Night3, IsDogAttending, IsSponsor, DatePaid);
        public bool IsPayed => AmountPayed >= AmountToPay;
        public string Note { get; set; }
    }
}
