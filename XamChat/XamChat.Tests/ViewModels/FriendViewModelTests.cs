using System;
using NUnit.Framework;
using XamChat.Core;

namespace XamChat.Tests
{
    [TestFixture]
    public class FriendViewModelTests
    {
        FriendViewModel friendViewModel;
        ISettings settings;

        [SetUp]
        public void SetUp()
        {
            Test.SetUp();

            settings = ServiceContainer.Resolve<ISettings>();
            friendViewModel = new FriendViewModel();
        }

        [Test]
        public void GetFriendsSuccessfully()
        {
            //Simulate login
            settings.User = new User();

            friendViewModel.GetFriends().Wait();

            Assert.That(friendViewModel.Friends, Is.Not.Empty);
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void GetFriendsWithoutLogin()
        {
            //Throws an exception
            friendViewModel.GetFriends().Wait();
        }

        [Test]
        public void AddFriendSuccessfully()
        {
            //Simulate login
            settings.User = new User();

            friendViewModel.Username = "myfriend";

            friendViewModel.AddFriend().Wait();

            Assert.That(friendViewModel.Friends, Has.Some.With.Property("Username").EqualTo(friendViewModel.Username));
        }
    }
}

