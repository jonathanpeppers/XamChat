using System;
using NUnit.Framework;
using XamChat.Core;

namespace XamChat.Tests
{
    [TestFixture]
    public class LoginViewModelTests
    {
        LoginViewModel loginViewModel;
        ISettings settings;

        [SetUp]
        public void SetUp()
        {
            Test.SetUp();

            settings = ServiceContainer.Resolve<ISettings>();
            loginViewModel = new LoginViewModel();
        }

        [Test]
        public void LoginSuccessfully()
        {
            loginViewModel.Username = "testuser";
            loginViewModel.Password = "password";

            loginViewModel.Login().Wait();

            Assert.That(settings.User, Is.Not.Null);
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void LoginWithNoUsernameOrPassword()
        {
            //Throws an exception
            loginViewModel.Login().Wait();
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void LoginWithNoPassword()
        {
            loginViewModel.Username = "testuser";

            //Throws an exception
            loginViewModel.Login().Wait();
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void LoginWithNoUsername()
        {
            loginViewModel.Password = "password";

            //Throws an exception
            loginViewModel.Login().Wait();
        }
    }
}

