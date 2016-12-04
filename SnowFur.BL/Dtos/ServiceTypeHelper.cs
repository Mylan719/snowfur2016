using SnowFur.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Dtos
{
    public static class ServiceTypeHelper
    {
        public static string GetName(int serviceType)
        {
            switch ((ServiceType)serviceType)
            {
                case ServiceType.AccomodationNight:
                    return "Ubytovanie";
                case ServiceType.Food:
                    return "Stravovanie";
                case ServiceType.Additional:
                    return "Ostatné";
                default:
                    return "Ostatné";

            }
        }

        public static IEnumerable<ServiceTypeDto> GetServiceTypes()
        {
            foreach (int i in Enumerable.Range(0, 3))
            {
                yield return new ServiceTypeDto { Id = i, Name = GetName(i) };
            }
        }
    }
}
