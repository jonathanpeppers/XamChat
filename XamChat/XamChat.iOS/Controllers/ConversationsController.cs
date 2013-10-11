using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	public partial class ConversationsController : UITableViewController
	{
        readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();

		public ConversationsController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.Source = new TableSource();
        }

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            try
            {
                await messageViewModel.GetConversations();

                TableView.ReloadData();
            }
            catch (Exception exc)
            {
                exc.DisplayError();
            }
        }

        private class TableSource : UITableViewSource
        {
            const string CellName = "ConversationCell";
            readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();

            public override int RowsInSection(UITableView tableview, int section)
            {
                return messageViewModel.Conversations == null ? 0 : messageViewModel.Conversations.Length; 
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var conversation = messageViewModel.Conversations [indexPath.Row];
                var cell = tableView.DequeueReusableCell(CellName);
                if (cell == null)
                {
                    cell = new UITableViewCell(UITableViewCellStyle.Subtitle, CellName);
                    cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
                }
                cell.TextLabel.Text = conversation.Username;
                cell.DetailTextLabel.Text = conversation.LastMessage;
                return cell;
            }
        }
	}
}
