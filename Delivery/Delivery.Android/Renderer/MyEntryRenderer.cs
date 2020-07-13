using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using Delivery.Renderer;
using Delivery.Droid.Renderer;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace Delivery.Droid.Renderer
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                base.OnElementChanged(e);

                if (Control != null)
                {
                    Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                }
            }
        }
    }
}