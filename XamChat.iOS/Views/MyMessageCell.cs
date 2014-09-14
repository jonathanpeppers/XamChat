using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	public partial class MyMessageCell : BaseMessageCell
	{
		public MyMessageCell (IntPtr handle) : base (handle)
		{
		}

		public override void Update(Message message)
		{
			this.message.Text = message.Text;
		}
	}
}
