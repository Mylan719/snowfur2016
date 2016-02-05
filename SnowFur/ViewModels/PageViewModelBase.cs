using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using SnowFur.ViewModels.Controls;

namespace SnowFur.ViewModels
{
    public class PageViewModelBase : DotvvmViewModelBase
    {
        public string SubpageTitle { get; set; }
        public string Title { get { return string.Format("{0} - {1}", LogoText, SubpageTitle); } }
        public string LogoText { get; set; }

        public LoginSection LoginSection { get; set; }

        [Bind(Direction.ServerToClient)]
        public string ErrorMessage { get; set; } = string.Empty;

        [Bind(Direction.ServerToClient)]
        public bool IsError => !string.IsNullOrEmpty(ErrorMessage);

        [Bind(Direction.ServerToClient)]
        public bool IsSuccessfullyFinished { set; get; }

        [Bind(Direction.ServerToClient)]
        public string SuccessMessage { get; set; }

        [Bind(Direction.ServerToClient)]
        public bool IsSuccess => !string.IsNullOrEmpty(SuccessMessage);

        [Bind(Direction.None)]
        public string RabitBackgroundCssClass { get; set; }

        [Bind(Direction.ServerToClient)]
        public string RabitFullCss => string.Format("col-sm-4 sf-bg {0}", RabitBackgroundCssClass);

        public PageViewModelBase() : base()
        {
            LogoText = "Snowfur 2016";
            RabitBackgroundCssClass = "sf-bg-intro";
        }

        public override Task Init()
        {
            LoginSection.Context = Context;

            return base.Init();
        }

        public TOut GetQuerySafe<TOut> ( string key)
        {
            if (!Context.Query.ContainsKey(key) || !(Context.Query[key] is TOut))
            {
                return default(TOut);
            }
            return (TOut) Context.Query[key];
        }

    }
}
