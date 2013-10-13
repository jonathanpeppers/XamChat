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

            username.ShouldReturn = _ => 
            {
                password.BecomeFirstResponder();
                return false;
            };
            password.ShouldReturn = _ =>
            {
                OnLogin(this, EventArgs.Empty);
                return false;
            };
            login.TouchUpInside += OnLogin;

            //Custom back button
            NavigationItem.BackBarButtonItem = new UIBarButtonItem("Logout", UIBarButtonItemStyle.Plain, null);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            username.Text =
                password.Text = string.Empty;

            loginViewModel.IsBusyChanged += OnIsBusyChanged;
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            loginViewModel.IsBusyChanged += OnIsBusyChanged;
        }

        void OnIsBusyChanged (object sender, EventArgs e)
        {
            username.Enabled =
                password.Enabled = 
                login.Enabled = 
                indicator.Hidden = !loginViewModel.IsBusy;
        }

        async void OnLogin (object sender, EventArgs e)
        {
            loginViewModel.Username = username.Text;
            loginViewModel.Password = password.Text;

            try
            {
                await loginViewModel.Login();

                PerformSegue("OnLogin", this);
            }
            catch (Exception exc)
            {
                exc.DisplayError();
            }
        }
	}
}
