using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Naxam.Controls.Platform.Droid;
using Plugin.Iconize;
using Plugin.Iconize.Droid.Controls;

namespace MyPassword.Droid.Helper
{
    class BottomBarHelper
    {

        public static void SetupBottomTabs(Context context)
        {
            var stateList = new Android.Content.Res.ColorStateList(
                new int[][] {
                    new int[] { Android.Resource.Attribute.StateChecked
                },
                new int[] { Android.Resource.Attribute.StateEnabled
                }
                },
                new int[] {
                    Color.Orange, //Selected
                    Color.White //Normal
	            });

            BottomTabbedRenderer.BackgroundColor = new Color(25, 118, 210);
            BottomTabbedRenderer.FontSize = 10f;
            BottomTabbedRenderer.IconSize = 24;
            BottomTabbedRenderer.ItemTextColor = stateList;
            BottomTabbedRenderer.ItemIconTintList = stateList;
            //BottomTabbedRenderer.Typeface = Typeface.CreateFromAsset(this.Assets, "architep.ttf");
            //BottomTabbedRenderer.ItemBackgroundResource = Resource.Drawable.bnv_selector;
            BottomTabbedRenderer.ItemSpacing = 4;
            //BottomTabbedRenderer.ItemPadding = new Xamarin.Forms.Thickness(6);
            BottomTabbedRenderer.BottomBarHeight = 56;
            BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Center;
            BottomTabbedRenderer.ShouldUpdateSelectedIcon = true;

            BottomTabbedRenderer.MenuItemIconSetter = (menuItem, iconSource, selected) => {
                try
                {
                    var iconized = Iconize.FindIconForKey(iconSource.File);
                    if (iconized == null)
                    {
                        //if (selected == true)
                        //    iconSource = iconSource + "_Sel";
                        BottomTabbedRenderer.DefaultMenuItemIconSetter.Invoke(menuItem, iconSource, selected);
                        return;
                    }
                    var drawable = new IconDrawable(context, iconized).Color(selected ? Color.Red : Color.White).SizeDp(30);
                    menuItem.SetIcon(drawable);

                }
                catch(Exception e)
                {

                }
            };

        }

    }
}