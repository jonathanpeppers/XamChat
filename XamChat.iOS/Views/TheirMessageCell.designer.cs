// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace XamChat.iOS
{
	[Register ("TheirMessageCell")]
	partial class TheirMessageCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel message { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (message != null) {
				message.Dispose ();
				message = null;
			}
		}
	}
}
