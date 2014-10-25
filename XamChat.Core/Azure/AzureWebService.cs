using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace XamChat.Core
{
	public class AzureWebService : IWebService
	{
		MobileServiceClient client = new MobileServiceClient(
			"https://xam-chat.azure-mobile.net/", 
			"PeYJEtbXwXDzKTqbjraHinYDNVrRcc27");

		#region IWebService implementation

		public async Task<User> Login(string username, string password)
		{
			var user = new User 
			{
				Username = username, 
				Password = password 
			};
			await client.GetTable<User>().InsertAsync(user);
			return user;
		}

		public Task<User> Register(User user)
		{
			throw new NotImplementedException();
		}

		public Task<User[]> GetFriends(string userId)
		{
			throw new NotImplementedException();
		}

		public Task<User> AddFriend(string userId, string username)
		{
			throw new NotImplementedException();
		}

		public Task<Conversation[]> GetConversations(string userId)
		{
			throw new NotImplementedException();
		}

		public Task<Message[]> GetMessages(string conversationId)
		{
			throw new NotImplementedException();
		}

		public Task<Message> SendMessage(Message message)
		{
			throw new NotImplementedException();
		}

		#endregion

		public async Task LoadData()
		{
			var users = client.GetTable<User>();
			var friends = client.GetTable<Friend>();

			var me = new User
			{
				Username = "jonathanpeppers",
				Password = "password"
			};
			var friend = new User
			{
				Username = "chucknorris",
				Password = "password"
			};

			await users.InsertAsync(me);
			await users.InsertAsync(friend);

			await friends.InsertAsync(new Friend { MyId = me.Id, Username = friend.Username });
			await friends.InsertAsync(new Friend { MyId = friend.Id, Username = me.Username });

			var conversations = client.GetTable<Conversation>();
			var messages = client.GetTable<Message>();

			var conversation = new Conversation
			{
				MyId = me.Id,
				UserId = friend.Id,
				Username = friend.Username,
				LastMessage = "HEY!"
			};

			await conversations.InsertAsync(conversation);
			await messages.InsertAsync(new Message 
			{ 
				ConversationId = conversation.Id, 
				ToId = me.Id,
				UserId = friend.Id, 
				Username = friend.Username, Text = "What's up?", 
				Date = DateTime.Now.AddSeconds(-60),
			});
			await messages.InsertAsync(new Message 
			{ 
				ConversationId = conversation.Id, 
				ToId = friend.Id,
				UserId = me.Id, 
				Username = me.Username, Text = "Not much", 
				Date = DateTime.Now.AddSeconds(-30),
			});
			await messages.InsertAsync(new Message 
			{ 
				ConversationId = conversation.Id, 
				ToId = me.Id,
				UserId = friend.Id, 
				Username = friend.Username, Text = "HEY!", 
				Date = DateTime.Now,
			});
		}
	}
}

