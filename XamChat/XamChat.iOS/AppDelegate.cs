using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            //ViewModels
            ServiceContainer.Register<LoginViewModel>(() => new LoginViewModel());
            ServiceContainer.Register<FriendViewModel>(() => new FriendViewModel());
            ServiceContainer.Register<MessageViewModel>(() => new MessageViewModel());
            ServiceContainer.Register<RegisterViewModel>(() => new RegisterViewModel());

            //Models
            ServiceContainer.Register<ISettings>(() => new FakeSettings());
			//ServiceContainer.Register<IWebService>(() => new FakeWebService());
			ServiceContainer.Register<IWebService>(() => new AzureWebService());

			LoadData();

            return true;
        }

		private async void LoadData()
		{
			var service = ServiceContainer.Resolve<IWebService>() as AzureWebService;

			await service.LoadData();
		}
    }
}

