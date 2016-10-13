using DotVVM.Framework.ViewModel;
using Riganti.Utils.Infrastructure.Core;
using SnowFur.BL.Facades;
using SnowFur.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFur.ViewModels.Controls
{
    public class PaimentConfirmationForm : OwinViewModelBase
    {
        [Required(ErrorMessage = "Zadajte zaplatenu sumu")]
        public decimal Amount { get; set; }

        [Range(1, 31)]
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        [Protect(ProtectMode.SignData)]
        [Bind(Direction.Both)]
        public int CurrentUserId { get; set; }

        [Bind(Direction.ServerToClient)]
        public string CurrentUserName { get; set; }

        [Bind(Direction.ServerToClient)]
        public bool IsShown { get; set; }

        [Bind(Direction.ServerToClient)]
        public List<int> Months => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        [Bind(Direction.ServerToClient)]
        public List<int> Years => new List<int> { 2015, 2016 };

        [Bind(Direction.None)]
        public Reservations ParentViewModel { get; set; }

        private RoomReservationFacade roomReservationFacade;
        private PersonalProfileFacade personalProfileFacade;

        public PaimentConfirmationForm(RoomReservationFacade roomReservationFacade, PersonalProfileFacade personalProfileFacade)
        {
            this.roomReservationFacade = roomReservationFacade;
            this.personalProfileFacade = personalProfileFacade;
        }

        public void Show(int userId)
        {
            CurrentUserId = userId;
            CurrentUserName = personalProfileFacade.GetUserNameById(userId);

            var reservation = roomReservationFacade.Get(userId);

            if (reservation != null)
            {
                Amount = reservation.AmountPayed;
                if (reservation.DatePaid.HasValue)
                {
                    Day = reservation.DatePaid.Value.Day;
                    Month = reservation.DatePaid.Value.Month;
                    Year = reservation.DatePaid.Value.Year;
                }
            }
            IsShown = true;
        }

        public void Hide()
        {
            CurrentUserId = -1;
            CurrentUserName = string.Empty;
            Day = 0;
            Month = -1;
            Year = -1;
            Amount = 0;
            IsShown = false;
        }

        public void Confirm()
        {
            try
            {
                var paidDate = new DateTime(Year, Month, Day, 10, 0, 0);

                string userNameConfirmed = roomReservationFacade.PayReservation(CurrentUserId, Amount, paidDate);
                ParentViewModel.SuccessMessage = string.Format("Potvrdená suma {0} Eur v prospech užívateľa {1}", Amount, userNameConfirmed);
            }
            catch (UIException ex)
            {
                ParentViewModel.ErrorMessage = ex.Message;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ParentViewModel.ErrorMessage = ex.Message;
            }
            finally
            {
                Hide();
            }
        }
    }
}
