using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.BL.Dtos
{
    public class PriceListSectionDto 
    {
        [Bind(Direction.None)]
        public int Type { get; set; }
        public string Name { get; set; }
        public List<PriceListItemDto> Items { get; set; }
    }
}
