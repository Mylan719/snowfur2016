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
        public string CurrentUserName => Authentication.User?.Identity?.Name;

        [Bind(Direction.ServerToClient)]
        public int CurrentUserId => Authentication.User?.Identity?.GetUserId<int>() ?? 0;

        private readonly AccountFacade accountFacade;
        private readonly PersonalProfileFacade personalProfileFacade;
        private readonly RoomReservationFacade roomReservationFacade;

        public MyProfile( PersonalProfileFacade proFac, AccountFacade accFac, RoomReservationFacade resFac)
        {
            personalProfileFacade = proFac;
            accountFacade = accFac;
            roomReservationFacade = resFac;

            SubpageTitle = "Môj profil";
            RabitBackgroundCssClass = "sf-bg-program";
            ActiveTabId = 1;
        }

        public override Task Init()
        {
            var task = base.Init();
            roomReservationFacade.CurrentUserId = CurrentUserId;
            personalProfileFacade.CurrentUserId = CurrentUserId;
            return task;
        }

        public override Task PreRender()
        {;
            var profileDto = personalProfileFacade.GetPersonalProfile() ?? new PersonalProfileDto();
            Mapper.Map(profileDto, PersonalProfileForm);

            //TODO: Fill reservation Data

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
                accountFacade.ChangePassword(CurrentUserId, changePasswordDto);
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
                personalProfileFacade.Update(profileDto);               
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
            throw new NotImplementedException();
        }
    }
}
