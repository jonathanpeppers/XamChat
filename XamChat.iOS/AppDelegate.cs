using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		public override UIWindow Window
		{
			get;
			set;
		}
		
		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			//View Models
			ServiceContainer.Register<LoginViewModel>(() => new LoginViewModel());
			ServiceContainer.Register<FriendViewModel>(() => new FriendViewModel());
			ServiceContainer.Register<RegisterViewModel>(() => new RegisterViewModel());
			ServiceContainer.Register<MessageViewModel>(() => new MessageViewModel());

			//Models
			ServiceContainer.Register<ISettings>(() => new FakeSettings());
			ServiceContainer.Register<IWebService>(() => new FakeWebService());

			return true;
		}
	}
}

