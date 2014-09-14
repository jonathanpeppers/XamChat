using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	partial class ConversationsController : UITableViewController
	{
		readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();

		public ConversationsController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			TableView.Source = new TableSource(this);
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			try
			{
				await messageViewModel.GetConversations();

				TableView.ReloadData();
			}
			catch(Exception exc)
			{
				new UIAlertView("Oops!", exc.Message, null, "Ok").Show();
			}
		}

		class TableSource : UITableViewSource
		{
			const string CellName = "ConversationCell";
			readonly ConversationsController controller;
			readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();

			public TableSource(ConversationsController controller)
			{
				this.controller = controller;
			}

			public override int RowsInSection(UITableView tableView, int section)
			{
				return messageViewModel.Conversations == null ? 0 : messageViewModel.Conversations.Length;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var conversation = messageViewModel.Conversations[indexPath.Row];
				var cell = tableView.DequeueReusableCell(CellName);
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, CellName);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}
				cell.TextLabel.Text = conversation.Username;
				return cell;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var conversation = messageViewModel.Conversations[indexPath.Row];
				messageViewModel.Conversation = conversation;
				controller.PerformSegue("OnConversations", this);
			}
		}
	}
}
