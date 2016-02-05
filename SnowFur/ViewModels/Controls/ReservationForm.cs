using DotVVM.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.ViewModels.Controls
{
    public class ReservationForm : DotvvmViewModelBase
    {
        public bool Night1 { get; set; }
        public bool Night2 { get; set; }
        public bool Night3 { get; set; }

        public bool IsVegetarian { get; set; }

        public bool IsSponsor { get; set; }

        public bool IsDogAttending { get; set; }

        [MaxLength(500, ErrorMessage = "Maximálna dĺžka je 500 znakov.")]
        public string Note { get; set; }
    }
}
