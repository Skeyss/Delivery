using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Delivery.iOS.Renderer;
using Delivery.Renderer;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace Delivery.iOS.Renderer
{
	public class MyEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				//skeys verificar 
				Control.BorderStyle = UITextBorderStyle.None;
				Control.Layer.CornerRadius = 10;
				Control.TextColor = UIColor.White;
			}
		}
	}
}