using System;
using System.Threading.Tasks;

namespace XamChat.Core
{
    public class LoginViewModel : BaseViewModel
    {
        readonly IWebService service = ServiceContainer.Resolve<IWebService>();

        public string Username { get; set; }

        public string Password { get; set; }

        public User User { get; private set; }

        public async Task Login()
        {
            if (string.IsNullOrEmpty(Username))
                throw new Exception("Username is blank.");

            if (string.IsNullOrEmpty(Password))
                throw new Exception("Password is blank.");

            IsBusy = true;
            try
            {
                User = await service.Login(Username, Password);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

