using System;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using UIKit;
using Cirrious.MvvmCross.Binding;
using System.Reflection;

namespace MapCross.Touch
{
	public class MvxUISegmentedControlSelectedSegmentTargetBinding : MvxPropertyInfoTargetBinding<UISegmentedControl>
	{
		public MvxUISegmentedControlSelectedSegmentTargetBinding(object target, PropertyInfo targetPropertyInfo)
			: base(target, targetPropertyInfo)
		{
			this.View.ValueChanged += HandleValueChanged;
		}

		private void HandleValueChanged(object sender, System.EventArgs e)
		{
			var view = this.View;
			if (view == null)
			{
				return;
			}
			FireValueChanged(view.SelectedSegment);
		}

		public override MvxBindingMode DefaultMode
		{
			get { return MvxBindingMode.OneWayToSource; }
		}

		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			if (isDisposing)
			{
				var view = this.View;
				if (view != null)
				{
					view.ValueChanged -= HandleValueChanged;
				}
			}
		}
	}
}

