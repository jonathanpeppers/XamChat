using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	partial class MessagesController : UITableViewController
	{
		readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();

		UIToolbar toolbar;
		UITextField message;
		UIBarButtonItem send;
		NSObject willShowObserver, willHideObserver;

		public MessagesController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			//Text Field
			message = new UITextField(new RectangleF(0, 0, 240, 32))
			{
				BorderStyle = UITextBorderStyle.RoundedRect,
				ReturnKeyType = UIReturnKeyType.Send,
				ShouldReturn = _ =>
				{
					Send();
					return false;
				},
			};

			//Bar button item
			send = new UIBarButtonItem("Send", UIBarButtonItemStyle.Plain, (sender, e) => Send());

			//Toolbar
			toolbar = new UIToolbar(new RectangleF(0, TableView.Frame.Height - 44, TableView.Frame.Width, 44));
			toolbar.Items = new UIBarButtonItem[]
			{
				new UIBarButtonItem(message),
				send
			};
			NavigationController.View.AddSubview(toolbar);

			TableView.Source = new TableSource();
			TableView.TableFooterView = new UIView(new RectangleF(0, 0, TableView.Frame.Width, 44))
			{
				BackgroundColor = UIColor.Clear,
			};
		}


		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			Title = messageViewModel.Conversation.Username;

			//Keyboard notifications
			willShowObserver = UIKeyboard.Notifications.ObserveWillShow(
				(sender, e) => OnKeyboardNotification(e));
			willHideObserver = UIKeyboard.Notifications.ObserveWillHide(
				(sender, e) => OnKeyboardNotification(e));

			//IsBusy
			messageViewModel.IsBusyChanged += OnIsBusyChanged;
			try
			{
				await messageViewModel.GetMessages();
				TableView.ReloadData();
				message.BecomeFirstResponder();
			}
			catch (Exception exc)
			{
				new UIAlertView("Oops!", exc.Message, null, "Ok").Show();
			}
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			//Unsubcribe notifications
			if (willShowObserver != null)
			{
				willShowObserver.Dispose();
				willShowObserver = null;
			}
			if (willHideObserver != null)
			{
				willHideObserver.Dispose();
				willHideObserver = null;
			}
			//IsBusy
			messageViewModel.IsBusyChanged -= OnIsBusyChanged;
		}

		void OnIsBusyChanged (object sender, EventArgs e)
		{
			message.Enabled =
				send.Enabled = !messageViewModel.IsBusy;
		}

		void ScrollToEnd()
		{
			TableView.ContentOffset = new PointF(0, TableView.ContentSize.Height - TableView.Frame.Height);
		}

		void OnKeyboardNotification (UIKeyboardEventArgs e)
		{
			//Check if the keyboard is becoming visible
			bool willShow = e.Notification.Name == UIKeyboard.WillShowNotification;

			//Start an animation, using values from the keyboard
			UIView.BeginAnimations("AnimateForKeyboard");
			UIView.SetAnimationDuration(e.AnimationDuration);
			UIView.SetAnimationCurve(e.AnimationCurve);

			//Calculate keyboard height, etc.
			if (willShow)
			{
				var keyboardFrame = e.FrameEnd;
				var frame = TableView.Frame;
				frame.Height -= keyboardFrame.Height;
				TableView.Frame = frame;
				frame = toolbar.Frame;
				frame.Y -= keyboardFrame.Height;
				toolbar.Frame = frame;
			}
			else
			{
				var keyboardFrame = e.FrameBegin;
				var frame = TableView.Frame;
				frame.Height += keyboardFrame.Height;
				TableView.Frame = frame;
				frame = toolbar.Frame;
				frame.Y += keyboardFrame.Height;
				toolbar.Frame = frame;
			}

			//Commit the animation
			UIView.CommitAnimations();
			ScrollToEnd();
		}

		async void Send()
		{
			//Just hide the keyboard if they didn't type anything
			if (string.IsNullOrEmpty(message.Text))
			{
				message.ResignFirstResponder();
				return;
			}

			//Set the text, send the message
			messageViewModel.Text = message.Text;
			await messageViewModel.SendMessage();

			//Clear the text field & view model
			message.Text =
				messageViewModel.Text = string.Empty;

			//Reload the table
			TableView.ReloadData();

			//Hide the keyboard
			message.ResignFirstResponder();

			//Scroll to end, to see the new message
			ScrollToEnd();
		}

		class TableSource : UITableViewSource
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
				var message = messageViewModel.Messages[indexPath.Row];
				bool isMyMessage = message.UserId == settings.User.Id;
				var cell = tableView.DequeueReusableCell(isMyMessage ? MyCellName : TheirCellName) as BaseMessageCell;
				cell.Update(message);
				return cell;
			}
		}
	}
}
