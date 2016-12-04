using DotVVM.Framework.ViewModel;
using SnowFur.BL.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.ViewModels.Controls
{
    public class ReservationForm : OwinViewModelBase
    {
        public List<UserServiceOrderDto> Orders { get; set; }

        public ReservationForm()
        {

        }
    }
}
