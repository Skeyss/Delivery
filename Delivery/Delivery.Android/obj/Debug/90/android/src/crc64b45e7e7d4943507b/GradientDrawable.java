package crc64b45e7e7d4943507b;


public class GradientDrawable
	extends android.graphics.drawable.GradientDrawable
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Delivery.Droid.Renderer.GradientDrawable, Delivery.Android", GradientDrawable.class, __md_methods);
	}


	public GradientDrawable ()
	{
		super ();
		if (getClass () == GradientDrawable.class)
			mono.android.TypeManager.Activate ("Delivery.Droid.Renderer.GradientDrawable, Delivery.Android", "", this, new java.lang.Object[] {  });
	}


	public GradientDrawable (android.graphics.drawable.GradientDrawable.Orientation p0, int[] p1)
	{
		super (p0, p1);
		if (getClass () == GradientDrawable.class)
			mono.android.TypeManager.Activate ("Delivery.Droid.Renderer.GradientDrawable, Delivery.Android", "Android.Graphics.Drawables.GradientDrawable+Orientation, Mono.Android:System.Int32[], mscorlib", this, new java.lang.Object[] { p0, p1 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
