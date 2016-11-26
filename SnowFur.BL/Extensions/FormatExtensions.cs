using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Extensions
{
    public static class FormatExtensions
    {
        public static string ToHtmlPickerFormatDate(this DateTime date)
        {
            return $"{date:yyyy-MM-dd}";
        }

        public static DateTime FromHtmlPickerFormatDate(this string datePickerString)
        {
            return DateTime.ParseExact(datePickerString, "yyyy-MM-dd", null);
        }
    }
}
