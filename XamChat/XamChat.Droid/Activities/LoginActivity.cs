using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XamChat.Core;

namespace XamChat.Droid
{
    [Activity(Label = "@string/ApplicationName", Theme = "@android:style/Theme.Holo.Light", MainLauncher = true)]
    public class LoginActivity : BaseActivity<LoginViewModel>
    {
        EditText username, password;
        Button login;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Login);

            username = FindViewById<EditText>(Resource.Id.username);
            password = FindViewById<EditText>(Resource.Id.password);
            login = FindViewById<Button>(Resource.Id.login);

            login.Click += OnLogin;
        }

        async void OnLogin (object sender, EventArgs e)
        {
            viewModel.Username = username.Text;
            viewModel.Password = password.Text;

            try
            {
                await viewModel.Login();

                //TODO: navigate to a new activity
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }
    }
}


