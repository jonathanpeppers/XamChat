using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Permission(Name = XamChat.Droid.PushConstants.BundleId + ".permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = XamChat.Droid.PushConstants.BundleId + ".permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace XamChat.Droid
{
	public static class PushConstants
	{
		public const string BundleId = "com.jonathanpeppers.xamchat";
		public const string ProjectId = "370328391848";
	}
}

