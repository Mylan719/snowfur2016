using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SnowFur.BL.Dtos;
using SnowFur.ViewModels.Admin;

namespace SnowFur.ViewModels.Controls
{
    public class ConventionDetailForm
    {       
        public ConventionDetailDto Detail { get; }
        public ConventionsViewModel Parent { get; set; }

        public ConventionDetailForm(ConventionDetailDto detail)
        {
            Detail = detail;
        }

        public void Hide()
        {
            
        }

        public void Save()
        {
            
        }
    }
}