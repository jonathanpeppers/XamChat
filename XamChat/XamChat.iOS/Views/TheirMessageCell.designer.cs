// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace XamChat.iOS
{
	[Register ("TheirMessageCell")]
	partial class TheirMessageCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel date { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel message { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (message != null) {
				message.Dispose ();
				message = null;
			}

			if (date != null) {
				date.Dispose ();
				date = null;
			}
		}
	}
}
