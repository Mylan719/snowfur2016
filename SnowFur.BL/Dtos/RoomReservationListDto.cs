using SnowFur.BL.Facades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Dtos
{
    public class RoomReservationListDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Night1 { get; set; }
        public bool Night2 { get; set; }
        public bool Night3 { get; set; }
        public bool IsSponsor { get; set; }
        public bool IsVegetarian { get; set; }
        public decimal AmountPayed { get; set; }
        public DateTime? DatePaid { get; set; }
        public string DatePaidFormated => DatePaid.HasValue ? DatePaid.Value.ToString("d. MM. yyyy", CultureInfo.CreateSpecificCulture("sk-SK")) : "nikdy";
        public decimal AmountToPay => PaymentHelper.CalculateSumToPay(Night1, Night2, Night3, IsDogAttending, IsSponsor, DatePaid);
        public bool IsPayed => AmountPayed >= AmountToPay;
        public bool IsDogAttending { get; set; }
        public string Note { get; set; }
    }
}