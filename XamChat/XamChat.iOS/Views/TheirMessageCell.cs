using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	public partial class TheirMessageCell : BaseMessageCell
	{
		public TheirMessageCell (IntPtr handle) : base (handle)
		{
		}

        public override void Update(Message message)
        {
            this.message.Text = message.Text;
            this.date.Text = message.Date.ToString("MM/dd/yy H:mm");
        }
	}
}
