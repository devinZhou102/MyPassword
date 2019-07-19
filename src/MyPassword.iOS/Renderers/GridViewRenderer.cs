using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MyPassword.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using FormsGridView = Plugin.GridViewControl.Common.GridView;
using FormsGridViewRenderer = Plugin.GridViewControl.iOS.Renderers.GridViewRenderer;

[assembly: ExportRenderer(typeof(FormsGridView), typeof(GridViewRenderer))]

namespace MyPassword.iOS.Renderers
{
    public class GridViewRenderer : FormsGridViewRenderer
    {
    }
}