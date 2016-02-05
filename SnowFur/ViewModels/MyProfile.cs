using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using SnowFur.ViewModels.Controls;
using DotVVM.Framework.Runtime.Filters;
using System.Threading.Tasks;
using SnowFur.BL.Facades;
using Microsoft.AspNet.Identity;
using AutoMapper;
using SnowFur.BL.Dtos;
using Riganti.Utils.Infrastructure.Core;

namespace SnowFur.ViewModels
{
    [Authorize]
    public class MyProfile : PageViewModelBase
    {
        public PersonalProfileForm PersonalProfileForm { get; set; }
        public PasswordChangeForm PasswordChangeForm { get; set; }
        public ReservationForm ReservationForm { get; set; }

        [Bind(Direction.ServerToClient)]
        public bool IsRegistrationPaid { get; set; }

        [Bind(Direction.ServerToClient)]
        public decimal AmountPaid { get; set; }

        [Bind(Direction.ServerToClient)]
        public decimal AmountToPay { get; set; }

        [Bind(Direction.ServerToClient)]
        public int ActiveTabId { get; set; }

        [Bind(Direction.ServerToClient)]
        public string CurrentUserName => Context.OwinContext.Authentication.User?.Identity?.Name;

        [Bind(Direction.ServerToClient)]
        public int CurrentUserId => Context.OwinContext.Authentication.User?.Identity?.GetUserId<int>() ?? 0;

        private LoginFacade loginFacade;
        private PersonalProfileFacade personalProfileFacade;
        private RoomReservationFacade roomReservationFacade;

        public MyProfile( PersonalProfileFacade proFac, LoginFacade logFac, RoomReservationFacade resFac)
        {
            personalProfileFacade = proFac;
            loginFacade = logFac;
            roomReservationFacade = resFac;

            SubpageTitle = "Môj profil";
            RabitBackgroundCssClass = "sf-bg-program";
            ActiveTabId = 1;
        }

        public override Task Init()
        {
            var task = base.Init();
            roomReservationFacade.CurrentUserId = CurrentUserId;
            return task;
        }

        public override Task PreRender()
        {;
            var profileDto = personalProfileFacade.GetPersonalProfile(CurrentUserId) ?? new PersonalProfileDto();
            Mapper.Map(profileDto, PersonalProfileForm);

            var reservationDetail = roomReservationFacade.Get(CurrentUserId) ?? new RoomReservationDto();
            Mapper.Map(reservationDetail, ReservationForm);

            IsRegistrationPaid = reservationDetail.IsPayed;
            AmountPaid = reservationDetail.AmountPayed;
            AmountToPay = reservationDetail.AmountToPay;


            return base.PreRender();
        }

        public void SwitchTab(int tabId)
        {
            ActiveTabId = tabId;
        }

        public string GetClassForId(int tabId)
        {
            return (ActiveTabId == tabId) ? "active" : string.Empty;
        }

        [ModelValidationFilter]
        public void SavePasswordChange()
        {
            try
            {
                var changePasswordDto = Mapper.Map<ChangePasswordDto>(PasswordChangeForm);
                loginFacade.ChangePassword(CurrentUserId, changePasswordDto);
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
            }
            SuccessMessage = !IsError ? "Hesle zmenené" : string.Empty;
            ActiveTabId = 1;
            PasswordChangeForm.Clear();
        }

        [ModelValidationFilter]
        public void SaveProfile ()
        {
            try {
                var profileDto = Mapper.Map<PersonalProfileDto>(PersonalProfileForm);
                personalProfileFacade.Update(profileDto, CurrentUserId);               
            }
            catch(UIException ex)
            {
                ErrorMessage = ex.Message;
            }
            SuccessMessage = !IsError ? "Zmeny uložené" : string.Empty;
            ActiveTabId = 2;
        }

        [ModelValidationFilter]
        public void SaveReservation()
        {
            try
            {
                var reservationDto = roomReservationFacade.Create();
                Mapper.Map(ReservationForm, reservationDto);

                roomReservationFacade.Save(reservationDto);
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
            }
            SuccessMessage = !IsError ? "Rezervácia uložená" : string.Empty;
            ActiveTabId = 3;
        }
    }
}
