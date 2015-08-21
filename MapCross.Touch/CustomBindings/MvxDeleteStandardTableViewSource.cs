using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MapCross.Core;
using MapCross.Core.ViewModels;

using UIKit;
using Foundation;
using Cirrious.MvvmCross.Binding.Bindings;
using System.Collections.Generic;
using MapCross.Core.Services;

namespace MapCross.Touch
{
	public class MvxDeleteStandardTableViewSource : MvxStandardTableViewSource
	{

//		private IRemove m_ViewModel;
//		protected string cellIdentifier = "OrdersCell";

//		#region Constructors
//		public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView, UITableViewCellStyle style, NSString cellIdentifier, IEnumerable<MvxBindingDescription> descriptions, UITableViewCellAccessory tableViewCellAccessory = 0) 
//			: base(tableView, style, cellIdentifier, descriptions, tableViewCellAccessory)
//		{
//			m_ViewModel = viewModel;
//		}
//
//
//		public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView, string bindingText) : base(tableView, bindingText)
//		{
//			m_ViewModel = viewModel;
//		}
//
//		public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView, NSString cellIdentifier) : base(tableView, cellIdentifier)
//		{
//			m_ViewModel = viewModel;
//			tableView.RegisterNibForCellReuse(UINib.FromName("OrdersCell", NSBundle.MainBundle), OrdersCell.Key);
//
////			tableView.RegisterClassForCellReuse(typeof(OrdersCell), new NSString("OrdersCell"));
//
//		}
//
//		public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView) : base(tableView)
//		{
//			m_ViewModel = viewModel;
//
//		}
//
//
//		public MvxDeleteStandardTableViewSource(IRemove viewModel, UITableView tableView, UITableViewCellStyle style, NSString cellId, string binding, UITableViewCellAccessory accessory)
//			: base(tableView, style, cellId, binding, accessory)
//		{
//			m_ViewModel = viewModel;
//		}
//		#endregion
		FirstViewModel _viewModel;
		public MvxDeleteStandardTableViewSource(FirstViewModel viewModel, UITableView tableView, string bindingText) : base(tableView, bindingText)
		{
			tableView.RegisterNibForCellReuse(UINib.FromName("OrdersCell", NSBundle.MainBundle), OrdersCell.Key);
			_viewModel = viewModel;
		}

		protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
		{
			var cellName = OrdersCell.Key;	
			var cell =  (UITableViewCell)tableView.DequeueReusableCell(cellName, indexPath);						

			//cell.BackgroundColor = entity.IsActive ? UIColor.Green : UIColor.Gray;
//			UIView cellBackgroundView = new UIView(new RectangleF(0, 0, cell.Bounds.Width, cell.Bounds.Height));
//			cellBackgroundView.BackgroundColor = entity.IsActive ? UIColor.Green : UIColor.Gray;
//			cell.BackgroundView = cellBackgroundView;
			cell.Accessory = UITableViewCellAccessory.None;
			return cell;
		}    	

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle)
			{
			case UITableViewCellEditingStyle.Delete:
				_viewModel.Orders[indexPath.Row].IsSelected = true;
				_viewModel.DeleteCommand.Execute(null);
				break;
			case UITableViewCellEditingStyle.None:
				break;
			} 
		}
	}
}

