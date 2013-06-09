using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Drawing;

namespace pagedview
{
	public class PivotViewController : UIViewController
	{
		private IList<UIViewController> _viewControllers;
		private UIScrollView _scrollView = new PagedScrollView();
		private UIPageControl _pageControl = new UIPageControl();

		public PivotViewController (IList<UIViewController> viewControllers)
		{
			_viewControllers = viewControllers;

			_scrollView.PagingEnabled = true;
			_scrollView.Bounces = true;
			_scrollView.DelaysContentTouches = true;
			_scrollView.ShowsHorizontalScrollIndicator = false;
			_scrollView.ContentSize = new System.Drawing.SizeF (960, UIScreen.MainScreen.Bounds.Height);
			_scrollView.ScrollRectToVisible (new RectangleF (320, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height), true);

			_pageControl.Frame = new RectangleF (0, 380, 320, 30);

			_scrollView.DecelerationEnded += HandleScrollViewDecelerationEnded;
			_pageControl.ValueChanged += HandlePageControlValueChanged;
		}
		public static int mod(int a, int b)
		{
			if (a < 0)
				return b + (a % b);
			else
				return a % b;
		}

		int index = 0;
		void HandleScrollViewDecelerationEnded (object sender, EventArgs e)
		{

			if (_scrollView.ContentOffset.X <= 0) {
				foreach (var p in _viewControllers)
					p.View.RemoveFromSuperview();
				var next = index;
				var current = mod((index - 1), _viewControllers.Count);
				var previous = mod ((current - 1), _viewControllers.Count);
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

				index = current;

				_scrollView.ScrollRectToVisible (new RectangleF(320, 0, 320, 480), false);

			} else if (_scrollView.ContentOffset.X >= 640) { 
				foreach (var p in _viewControllers)
					p.View.RemoveFromSuperview();
				var current = mod ((index + 1), _viewControllers.Count);
				var next = mod ((current + 1), _viewControllers.Count);
				var previous = index;
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

				index = current;

				_scrollView.ScrollRectToVisible (new RectangleF(320, 0, 320, 480), false);
			}
		}

		void HandlePageControlValueChanged (object sender, EventArgs e)
		{
			foreach (var p in _viewControllers)
				p.View.RemoveFromSuperview();
			var current = _pageControl.CurrentPage;
			var next = mod ((current + 1), _viewControllers.Count);
			var previous = mod ((current - 1), _viewControllers.Count);;
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

			index = current;

			_scrollView.ScrollRectToVisible (new RectangleF(320, 0, 320, 480), false);
		}

		public override void ViewDidLoad ()
		{
			Console.WriteLine("Paged view did load");
			View.Frame = UIScreen.MainScreen.Bounds;
			View.BackgroundColor = UIColor.Black;
			View.AddSubview(_scrollView);
			View.AddSubview(_pageControl);
		}

		public override void ViewDidAppear (bool animated)
		{
//			Console.WriteLine("Paged view did appear");
			ReloadPages();
		}

		public void ReloadPages() {

			foreach (var p in _viewControllers)
				p.View.RemoveFromSuperview();

			int i;
			var numberOfPages = _viewControllers.Count;
			for (i=0; i<2; i++) {
				var pageViewController = _viewControllers[i];
				pageViewController.View.Frame = new RectangleF(320*(i+1), 0, 320, this._scrollView.Frame.Height-30);
				_scrollView.AddSubview(pageViewController.View);
			}
			var last = _viewControllers[_viewControllers.Count - 1];
			last.View.Frame = new RectangleF(0, 0, 320, this._scrollView.Frame.Height-30);
			_scrollView.AddSubview(last.View);
			i = i + 2;

			_pageControl.Pages = _viewControllers.Count;
			_pageControl.CurrentPage = index;

			//_pages[0].ViewDidAppear(true);
		}
	}

	sealed class PagedScrollView : UIScrollView
	{
		public PagedScrollView()
		{
			ShowsHorizontalScrollIndicator = false;
			ShowsVerticalScrollIndicator = false;
			Bounces = true;
			ContentSize = new SizeF(320, 480);
			PagingEnabled = true;
			Frame = new RectangleF(0, 0, 320, 480);
		}
	}
}

