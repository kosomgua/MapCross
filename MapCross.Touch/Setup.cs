using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using UIKit;
using Cirrious.MvvmCross.Binding.Bindings.Target.Construction;

namespace MapCross.Touch
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
			: base(applicationDelegate, window)
		{
		}

		protected override IMvxApplication CreateApp()
		{
			return new Core.App();
		}

		protected override void InitializeFirstChance()
		{
			base.InitializeFirstChance();
		}

		protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
		{
			registry.RegisterPropertyInfoBindingFactory(typeof(MvxUISegmentedControlSelectedSegmentTargetBinding), typeof(UISegmentedControl), "SelectedSegment");
		}
	}
}