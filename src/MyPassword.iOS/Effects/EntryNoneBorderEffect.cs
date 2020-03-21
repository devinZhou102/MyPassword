using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: ResolutionGroupName("Vapor")]
[assembly: ExportEffect(typeof(MyPassword.iOS.Effects.EntryNoneBorderEffect), nameof(MyPassword.Effects.EntryNoneBorderEffect))]
namespace MyPassword.iOS.Effects
{
    public class EntryNoneBorderEffect : Xamarin.Forms.Platform.iOS.PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control != null && Control is UITextField)
            {
                UITextField field = Control as UITextField;
                field.BorderStyle = UITextBorderStyle.None;
                Control.Layer.BorderWidth = 0;
            }
        }

        protected override void OnDetached()
        {
        }
    }
}