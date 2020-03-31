using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ResolutionGroupName("Vapor")]
[assembly: ExportEffect(typeof(MyPassword.Droid.Effects.EntryNoneBorderEffect), "EntryNoneBorderEffect")]
namespace MyPassword.Droid.Effects
{
    public class EntryNoneBorderEffect : Xamarin.Forms.Platform.Android.PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control != null)
            {
                //var effect = (XEntryNoneBorderEffect)Element.Effects.FirstOrDefault(e => e is XEntryNoneBorderEffect);
                //if (effect != null && Control is Android.Widget.EditText)
                //{
                //    Android.Widget.EditText txt = (this.Control as Android.Widget.EditText);
                //}
                if (Control is Android.Widget.EditText edit)
                {
                    edit.SetPadding(3, 3, 3, 3); 
                }
                this.Control.SetBackground(null);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}