using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using XamChat.Core;

namespace XamChat.iOS
{
	public partial class LoginController : UIViewController
	{
        readonly LoginViewModel loginViewModel = ServiceContainer.Resolve<LoginViewModel>();

		public LoginController (IntPtr handle) : base (handle)
		{

		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            login.TouchUpInside += OnLogin;
        }

        async void OnLogin (object sender, EventArgs e)
        {
            loginViewModel.Username = username.Text;
            loginViewModel.Password = password.Text;

            try
            {
                await loginViewModel.Login();


            }
            catch (Exception exc)
            {
                exc.DisplayError();
            }
        }
	}
}
