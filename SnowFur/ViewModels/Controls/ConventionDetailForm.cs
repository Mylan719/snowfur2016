using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotVVM.Framework.ViewModel;
using SnowFur.BL.Dtos;
using SnowFur.BL.Extensions;
using SnowFur.BL.Facades;
using SnowFur.ViewModels.Admin;

namespace SnowFur.ViewModels.Controls
{
    public class ConventionDetailForm
    {
        private ConventionFacade facade;
        private ConventionsViewModel parent;

        public ConventionDetailDto Detail { get; set; }

        public string StartDateString
        {
            get { return Detail?.StartDate.ToHtmlPickerFormatDate() ?? ""; }
            set { if (Detail != null) Detail.StartDate = value.FromHtmlPickerFormatDate(); }
        }

        public string EndDateString
        {
            get { return Detail?.EndDate.ToHtmlPickerFormatDate() ?? ""; }
            set { if (Detail != null) Detail.EndDate = value.FromHtmlPickerFormatDate(); }
        }

        [Bind(Direction.ServerToClient)]
        public bool IsShown => Detail != null;

        public void Load(ConventionFacade conventionFacade, ConventionsViewModel parent)
        {
            this.facade = conventionFacade;
            this.parent = parent;
        }

        public void Hide()
        {
            Detail = null;
        }

        public void Save()
        {
            parent.ReportErrors(() =>
            {
                facade.Save(Detail);
                parent.SuccessMessage = $"Con {Detail.Name} bol uložený.";
                Detail = null;
            });
        }
    }
}