using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	public partial class FriendsController : UITableViewController
	{
        readonly FriendViewModel friendViewModel = ServiceContainer.Resolve<FriendViewModel>();

		public FriendsController (IntPtr handle) : base (handle)
		{
		}
	}
}
