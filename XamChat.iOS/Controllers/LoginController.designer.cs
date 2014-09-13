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
	[Register ("LoginController")]
	partial class LoginController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIActivityIndicatorView indicator { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton login { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField password { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField username { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (indicator != null) {
				indicator.Dispose ();
				indicator = null;
			}
			if (login != null) {
				login.Dispose ();
				login = null;
			}
			if (password != null) {
				password.Dispose ();
				password = null;
			}
			if (username != null) {
				username.Dispose ();
				username = null;
			}
		}
	}
}
