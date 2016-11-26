using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Binding.Expressions;
using DotVVM.Framework.Controls;

namespace SnowFur.Views.Controls
{
	public class YesNo : DotvvmMarkupControl
	{
        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DotvvmProperty HeaderProperty
                = DotvvmProperty.Register<string, YesNo>(c => c.Header);

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DotvvmProperty MessageProperty
                = DotvvmProperty.Register<string, YesNo>(c => c.Message);

        public Command YesAction
        {
            get { return (Command)GetValue(YesActionProperty); }
            set { SetValue(YesActionProperty, value); }
        }
        public static readonly DotvvmProperty YesActionProperty
                = DotvvmProperty.Register<Command, YesNo>(c => c.YesAction);

        public Command NoAction
        {
            get { return (Command)GetValue(NoActionProperty); }
            set { SetValue(NoActionProperty, value); }
        }
        public static readonly DotvvmProperty NoActionProperty
                = DotvvmProperty.Register<Command, YesNo>(c => c.NoAction);

        public bool IsShown
        {
            get { return (bool)GetValue(IsShownProperty); }
            set { SetValue(IsShownProperty, value); }
        }
        public static readonly DotvvmProperty IsShownProperty
                = DotvvmProperty.Register<bool, YesNo>(c => c.IsShown);
    }
}

