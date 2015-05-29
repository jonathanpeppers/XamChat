using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
            //View Models
            ServiceContainer.Register<LoginViewModel>(() => new LoginViewModel());
            ServiceContainer.Register<FriendViewModel>(() => new FriendViewModel());
            ServiceContainer.Register<RegisterViewModel>(() => new RegisterViewModel());
            ServiceContainer.Register<MessageViewModel>(() => new MessageViewModel());

            //Models
            ServiceContainer.Register<ISettings>(() => new FakeSettings());
            ServiceContainer.Register<IWebService>(() => new FakeWebService());
            //ServiceContainer.Register<IWebService>(() => new AzureWebService());

			UIApplication.Main(args, null, "AppDelegate");
		}
	}
}
