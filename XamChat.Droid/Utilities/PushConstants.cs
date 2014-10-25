using System;
using Android.App;

namespace XamChat.Droid
{
	[assembly: Permission(
		Name = XamChat.Droid.PushConstants.BundleId + 
		".permission.C2D_MESSAGE")]
	[assembly: UsesPermission(
		Name = XamChat.Droid.PushConstants.BundleId + 
		".permission.C2D_MESSAGE")]
	[assembly: UsesPermission(
		Name = "com.google.android.c2dm.permission.RECEIVE")]
	[assembly: UsesPermission(
		Name = "android.permission.GET_ACCOUNTS")]
	[assembly: UsesPermission(
		Name = "android.permission.INTERNET")]
	[assembly: UsesPermission(
		Name = "android.permission.WAKE_LOCK")]

	public static class PushConstants
	{
		public const string BundleId = "your-bundle-id";
		public const string ProjectNumber = 
			"your-project-number";
	}
}

