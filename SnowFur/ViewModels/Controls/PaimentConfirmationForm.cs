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
using DotVVM.Framework.Runtime.Filters;

namespace SnowFur.ViewModels.Controls
{
    [Authorize]
    public class PaimentConfirmationForm
    {
        [Required(ErrorMessage = "Zadajte zaplatenu sumu")]
        public decimal Amount { get; set; }

        [Range(1, 31)]
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        [Protect(ProtectMode.SignData)]
        public int CurrentUserId { get; set; }

        [Bind(Direction.ServerToClient)]
        public string CurrentUserName { get; set; }

        [Bind(Direction.ServerToClient)]
        public bool IsShown { get; set; }

        [Bind(Direction.ServerToClient)]
        public List<int> Months => new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        [Bind(Direction.ServerToClient)]
        public List<int> Years => new List<int> { 2018, 2019 };

        [Bind(Direction.None)]
        public Reservations ParentViewModel { get; set; }

        private ConventionFacade conventionFacade;

        public PaimentConfirmationForm(ConventionFacade conventionFacade)
        {
            this.conventionFacade = conventionFacade;
        }

        public void Show(int userId)
        {
            ParentViewModel.ReportErrors(() =>
            {
                var info = conventionFacade.GetUserPayment(userId);
                CurrentUserId = userId;
                Amount = info?.Amount ?? 0;
                Day = info?.Date.Day ?? DateTime.UtcNow.Day;
                Month = info?.Date.Month ?? DateTime.UtcNow.Month;
                Year = info?.Date.Year ?? DateTime.UtcNow.Year;
                IsShown = true;
            });
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
           ParentViewModel.ReportErrors(() =>
           {
               conventionFacade.SetUserPayment(CurrentUserId, Amount);
               ParentViewModel.SuccessMessage = "Platba potvrdená.";
               IsShown = false;
           });
        }
    }   
}
