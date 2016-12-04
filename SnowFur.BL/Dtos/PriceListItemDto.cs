using DotVVM.Framework.ViewModel;
using System;

namespace SnowFur.BL.Dtos
{
    public class PriceListItemDto
    {
        public string Name { get; set; }
        
        [Bind(Direction.None)]
        public DateTime? ChargeAfter { get; set; }
        public string ChargedAfterFormated { get; set; }

        [Bind(Direction.None)]
        public decimal Price { get; set; }
        public string PriceFormated { get; set; }

    }
}