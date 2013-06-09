using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Drawing;

namespace pagedview
{
	public class PivotViewController : UIViewController
	{
		private IList<UIViewController> _viewControllers;
		private UIScrollView _scrollView = new UIScrollView();
		private UIPageControl _pageControl = new UIPageControl();
		private int _index;

		public PivotViewController (IList<UIViewController> viewControllers)
			:this(viewControllers, 0)
		{

		}

		public PivotViewController (IList<UIViewController> viewControllers, int index)
		{
			_index = index;

			_viewControllers = viewControllers;

			_scrollView.Frame = new RectangleF(0, 0, 320, UIScreen.MainScreen.Bounds.Height);
			_scrollView.PagingEnabled = true;
			_scrollView.Bounces = false;
			_scrollView.DelaysContentTouches = false;
			_scrollView.ShowsHorizontalScrollIndicator = false;
			_scrollView.ContentSize = new System.Drawing.SizeF (960, UIScreen.MainScreen.Bounds.Height);
			_scrollView.ScrollRectToVisible (new RectangleF (320, 0, UIScreen.MainScreen.Bounds.Width, 
			                                                 UIScreen.MainScreen.Bounds.Height), true);

			_pageControl.Frame = new RectangleF (0, UIScreen.MainScreen.Bounds.Height - 100, 320, 30);

			_scrollView.DecelerationEnded += HandleScrollViewDecelerationEnded;
			_pageControl.ValueChanged += HandlePageControlValueChanged;
		}

		void HandleScrollViewDecelerationEnded (object sender, EventArgs e)
		{
			if (_scrollView.ContentOffset.X <= 0) {
				var current = MathEx.Mod((_index - 1), _viewControllers.Count);
				LoadPages (current);

			} else if (_scrollView.ContentOffset.X >= 640) { 
				var current = MathEx.Mod ((_index + 1), _viewControllers.Count);
				LoadPages (current);
			}
		}

		void HandlePageControlValueChanged (object sender, EventArgs e)
		{
			LoadPages (_pageControl.CurrentPage);
		}

		public override void ViewDidLoad ()
		{
			View.Frame = UIScreen.MainScreen.Bounds;
			View.BackgroundColor = UIColor.Black;
			View.AddSubview(_scrollView);
			View.AddSubview(_pageControl);
		}

		public override void ViewDidAppear (bool animated)
		{
			Refresh();
		}

		public void Refresh() {

			LoadPages (_index);

			_pageControl.Pages = _viewControllers.Count;
			_pageControl.CurrentPage = _index;		
		}

		private void LoadPages(int index){
			foreach (var p in _viewControllers)
				p.View.RemoveFromSuperview();

			var current = index;
			var next = MathEx.Mod ((current + 1), _viewControllers.Count);
			var previous = MathEx.Mod ((current - 1), _viewControllers.Count);;
			_pageControl.CurrentPage = current;

			var prev = _viewControllers[previous];
			prev.View.Frame = new RectangleF(0, 0, 320, this._scrollView.Frame.Height-30);
			_scrollView.AddSubview(prev.View);

			var cur = _viewControllers[current];
			cur.View.Frame = new RectangleF(320, 0, 320, this._scrollView.Frame.Height-30);
			_scrollView.AddSubview(cur.View);

			var nex = _viewControllers[next];
			nex.View.Frame = new RectangleF(640, 0, 320, this._scrollView.Frame.Height-30);
			_scrollView.AddSubview(nex.View);

			_index = current;

			_scrollView.ScrollRectToVisible (new RectangleF(320, 0, 320, UIScreen.MainScreen.Bounds.Height), false);

		}
	}
	
}

