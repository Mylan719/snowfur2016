using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Runtime.Filters;
using System.ComponentModel.DataAnnotations;
using SnowFur.BL.Facades;
using Riganti.Utils.Infrastructure.Core;

namespace SnowFur.ViewModels.Admin
{
    [Authorize("admin")]
    public class BroadcastMail : PageViewModelBase
    {
        private MailingFacade mailingFacade;

        public BroadcastMail(MailingFacade fac)
        {
            mailingFacade = fac;
        }

        [Required(ErrorMessage = "Vyžadované")]
        [MaxLength(50, ErrorMessage = "Maximálne 50 znakov.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Vyžadované")]
        [MaxLength(500, ErrorMessage = "Maximálne 500 znakov.")]
        public string Message { get; set; }

        [ModelValidationFilter]
        public void SendMail()
        {
            try
            {
                mailingFacade.SendAdminBroadcastMail(Subject, Message);
                IsSuccessfullyFinished = true;
            }
            catch (UIException ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
