using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Facades
{
    public static class PaymentHelper
    {
        public static decimal BasePricePerNight => 25;
        public static decimal PriceForDog => 0;
        public static DateTime SoonPaimentDeadline => new DateTime(2015, 12, 12, 23, 59, 59);
        public static decimal LatePaimentPenalty => 1;

        public static decimal SponsorDayAddition => 5;

        public static decimal CalculateSumToPay(bool night1, bool night2, bool night3, bool dog, bool isSponsor, DateTime? datePaid)
        {
            bool isOverDeadline = (!datePaid.HasValue && DateTime.UtcNow > SoonPaimentDeadline) || (datePaid.HasValue && datePaid.Value > SoonPaimentDeadline);

            decimal totalPricePerNight = BasePricePerNight + (isSponsor ? SponsorDayAddition : 0) + (isOverDeadline ? LatePaimentPenalty : 0);

            int nights = 0;

            if (night1) { nights++; }
            if (night2) { nights++; }
            if (night3) { nights++; }


            return nights * totalPricePerNight + (dog ? PriceForDog : 0);
        }
    }
}
