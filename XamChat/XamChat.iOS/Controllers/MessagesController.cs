using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	public partial class MessagesController : UITableViewController
	{
        readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();

		public MessagesController (IntPtr handle) : base (handle)
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

            Title = messageViewModel.Conversation.Username;
            try
            {
                await messageViewModel.GetMessages();

                TableView.ReloadData();

                //Scroll to end
                var indexPath = NSIndexPath.FromRowSection(messageViewModel.Messages.Length - 1, 0);
                TableView.ScrollToRow(indexPath, UITableViewScrollPosition.Bottom, false);
            }
            catch (Exception exc)
            {
                exc.DisplayError();
            }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            messageViewModel.Clear();
        }

        private class TableSource : UITableViewSource
        {
            const string MyCellName = "MyCell";
            const string TheirCellName = "TheirCell";
            readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            readonly ISettings settings = ServiceContainer.Resolve<ISettings>();

            public override int RowsInSection(UITableView tableview, int section)
            {
                return messageViewModel.Messages == null ? 0 : messageViewModel.Messages.Length;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var message = messageViewModel.Messages [indexPath.Row];
                bool isMyMessage = message.UserId == settings.User.Id;
                var cell = tableView.DequeueReusableCell(isMyMessage ? MyCellName : TheirCellName) as BaseMessageCell;
                cell.Update(message);
                return cell;
            }
        }
	}
}
