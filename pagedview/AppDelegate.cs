using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace pagedview
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UINavigationController navigationController;
		UIWindow window;
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);

			var vc = new List<UIViewController> () {
				new ColorViewController(UIColor.Red, "Hello world from page 1"),
				new ColorViewController(UIColor.Yellow, "Hello world from page 2"),
				new ColorViewController(UIColor.Green, "Hello world from page 3"),
				new ColorViewController(UIColor.Magenta, "Hello world from page 4"),
				new ColorViewController(UIColor.Cyan, "Hello world from page 5"),
				new ColorViewController(UIColor.Brown, "Hello world from page 6"),
				new ColorViewController(UIColor.Orange, "Hello world from page 7"),
				new ColorViewController(UIColor.Purple, "Hello world from page 8")
			};

			var controller = new PivotViewController (vc, 0);
			navigationController = new UINavigationController (controller);
			window.RootViewController = navigationController;

			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

