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
using PushSharp.Client;

namespace XamChat.Droid
{
	[BroadcastReceiver(Permission = GCMConstants.PERMISSION_GCM_INTENTS)]
	[IntentFilter(new string[] { GCMConstants.INTENT_FROM_GCM_MESSAGE }, Categories = new string[] { PushConstants.BundleId })]
	[IntentFilter(new string[] { GCMConstants.INTENT_FROM_GCM_REGISTRATION_CALLBACK }, Categories = new string[] { PushConstants.BundleId })]
	[IntentFilter(new string[] { GCMConstants.INTENT_FROM_GCM_LIBRARY_RETRY }, Categories = new string[] { PushConstants.BundleId })]
	public class PushReceiver : PushHandlerBroadcastReceiverBase<PushHandlerService>
	{
	}
}

