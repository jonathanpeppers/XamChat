using System;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	public class BaseMessageCell : UITableViewCell
	{
		public BaseMessageCell(IntPtr handle) : base(handle)
		{
		}

		public virtual void Update(Message message)
		{
		}
	}
}

