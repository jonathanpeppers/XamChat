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

			//LoadData();

            return true;
        }

		public async override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			var loginViewModel = ServiceContainer.Resolve<LoginViewModel>();
			try
			{
				string token = deviceToken.Description;
				token = token.Substring(1, token.Length - 2);
				await loginViewModel.RegisterPush(token);
			}
			catch (Exception exc)
			{
				Console.WriteLine("Error registering push: " + exc);
			}
		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			Console.WriteLine("Error registering push: " + error.LocalizedDescription);
		}

		private async void LoadData()
		{
			var service = ServiceContainer.Resolve<IWebService>() as AzureWebService;

			await service.LoadData();
		}
    }
}

