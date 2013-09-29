using System;
using NUnit.Framework;
using XamChat.Core;

namespace XamChat.Tests
{
    public class RegisterViewModelTests
    {
        RegisterViewModel registerViewModel;
        ISettings settings;

        [SetUp]
        public void SetUp()
        {
            Test.SetUp();

            settings = ServiceContainer.Resolve<ISettings>();
            registerViewModel = new RegisterViewModel();
        }

        [Test]
        public void RegisterSuccessfully()
        {
            registerViewModel.Username = "testuser";
            registerViewModel.Password =
                registerViewModel.ConfirmPassword = "password";

            registerViewModel.Register().Wait();

            Assert.That(settings.User, Is.Not.Null);
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void RegisterWithNoUsernameOrPassword()
        {
            //Throws an exception
            registerViewModel.Register().Wait();
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void RegisterWhenPasswordsDoNotMatch()
        {
            registerViewModel.Username = "testuser";
            registerViewModel.Password = "password";
            registerViewModel.ConfirmPassword = "different";

            //Throws an exception
            registerViewModel.Register().Wait();
        }
    }
}

