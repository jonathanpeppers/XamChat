using System;
using NUnit.Framework;
using XamChat.Core;

namespace XamChat.Tests
{
    [TestFixture]
    public class MessageViewModelTests
    {
        MessageViewModel messageViewModel;
        ISettings settings;

        [SetUp]
        public void SetUp()
        {
            Test.SetUp();

            settings = ServiceContainer.Resolve<ISettings>();
            messageViewModel = new MessageViewModel();
        }

        [Test]
        public void GetConversationsSuccessfully()
        {
            //Simulate login
            settings.User = new User();

            messageViewModel.GetConversations().Wait();

            Assert.That(messageViewModel.Conversations, Is.Not.Empty);
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void GetConversationsWithoutLogin()
        {
            //Throws exception
            messageViewModel.GetConversations().Wait();
        }

        [Test]
        public void GetMessagesSuccessfully()
        {
            //Simulate login
            settings.User = new User();
            //Simulate conversation selection
            messageViewModel.Conversation = new Conversation();

            messageViewModel.GetMessages().Wait();

            Assert.That(messageViewModel.Messages, Is.Not.Empty);
        }

        [Test, ExpectedException(typeof(AggregateException))]
        public void GetMessagesWithoutConversation()
        {
            //Simulate login
            settings.User = new User();

            //Throws exception
            messageViewModel.GetMessages().Wait();
        }

        [Test]
        public void SendMessage()
        {
            //Simulate login
            settings.User = new User();
            //Simulate conversation selection
            messageViewModel.Conversation = new Conversation();
            messageViewModel.Text = "Hello";

            messageViewModel.SendMessage().Wait();

            Assert.That(messageViewModel.Messages, Has.Some.With.Property("Text").EqualTo(messageViewModel.Text));
        }
    }
}

