using DotVVM.Framework.ViewModel;
using SnowFur.BL.Dtos;
using SnowFur.BL.Facades;
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
        private readonly ReservationFacade reservationFacade;

        private MyProfile parent;
        public List<UserServiceOrderDto> Orders { get; set; }

        public decimal ToPay => Orders.Where(o => o.IsOrdered).Sum(o => o.Price);

        public ReservationForm(ReservationFacade reservationFacade)
        {
            this.reservationFacade = reservationFacade;
        }

        public void SetParent(MyProfile parent)
        {
            this.parent = parent;
        }

        public override Task Init()
        {
            parent.ReportErrors(() =>
            {
                reservationFacade.Init(CurrentUserId);
            });
            return base.Init();
        }

        public override Task PreRender()
        {
            parent.ReportErrors(() =>
            {
                Orders = reservationFacade.GetServiceOrders();
            });
            return base.PreRender();
        }

        public void Cancel()
        {
            parent.ActiveTabId = 3;
        }

        public void Save()
        {
            parent.ReportErrors(() =>
            {
                foreach (var order in Orders)
                {
                    if (order.IsOrdered)
                    {
                        reservationFacade.OrderService(order.ServiceId);
                    }
                    else
                    {
                        reservationFacade.UnorderService(order.ServiceId);
                    }
                }
            });
            parent.ActiveTabId = 3;
        }
    }
}
