using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace pagedview
{
	public partial class ColorViewController : UIViewController
	{
		UIColor Color;
		public ColorViewController (UIColor color) : base ("ColorViewController", null)
		{
			Color = color;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.View.BackgroundColor = Color;
			this.View.Frame = new RectangleF (0, 0, 320, UIScreen.MainScreen.Bounds.Height);

			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

