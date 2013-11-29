using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using XamChat.Core;

namespace XamChat.Core
{
	public class AzureWebService : IWebService
    {
		MobileServiceClient client = new MobileServiceClient(
			"https://jon-xamchat.azure-mobile.net/", 
			"tstsyBmIJMbwkvoVyCqvlfhjACyEok19"
		);

		public AzureWebService()
		{
			CurrentPlatform.Init();
		}

		public async Task LoadData()
		{
			var users = client.GetTable<User>();
			var friends = client.GetTable<Friend>();
			var conversations = client.GetTable<Conversation>();
			var messages = client.GetTable<Message>();

			var me = new User { Username = "jonathanpeppers", Password = "password" };
			var friend = new User { Username = "chucknorris", Password = "password" };

			await users.InsertAsync(me);
			await users.InsertAsync(friend);

			await friends.InsertAsync(new Friend { MyId = me.Id, Username = friend.Username });
			await friends.InsertAsync(new Friend { MyId = friend.Id, Username = me.Username });

			var conversation = new Conversation { MyId = me.Id, UserId = friend.Id, Username = friend.Username, LastMessage = "HEY!" };

			await conversations.InsertAsync(conversation);
			await messages.InsertAsync(new Message
			{
				ConversationId = conversation.Id,
				UserId = friend.Id,
				Username = friend.Username,
				Text = "What's up?",
			   	Date = DateTime.Now.AddSeconds(-60),
			});
			await messages.InsertAsync(new Message
			{
				ConversationId = conversation.Id,
				UserId = me.Id,
				Username = me.Username,
				Text = "Not much",
			   	Date = DateTime.Now.AddSeconds(-30),
			});
			await messages.InsertAsync(new Message
			{
				ConversationId = conversation.Id,
				UserId = friend.Id,
				Username = friend.Username,
				Text = "HEY!",
			   	Date = DateTime.Now,
			});
		}

		public async Task<User> Login(string username, string password)
		{
			var user = new User { Username = username, Password = password };
			await client.GetTable<User>().InsertAsync(user);
			return user;
		}

		public async Task<User> Register(User user)
		{
			await client.GetTable<User>().InsertAsync(user);
			return user;
		}

		public async Task<User[]> GetFriends(string userId)
		{
			var list = await client.GetTable<Friend>().Where(f => f.MyId == userId).ToListAsync();
			return list.Select(f => new User { Id = f.UserId, Username = f.Username}).ToArray();
		}

		public async Task<User> AddFriend(string userId, string username)
		{
			var friend = new Friend { MyId = userId, Username = username };
			await client.GetTable<Friend>().InsertAsync(friend);
			return new User { Id = friend.UserId, Username = friend.Username };
		}

		public async Task<Conversation[]> GetConversations(string userId)
		{
			var list = await client.GetTable<Conversation>().Where(c => c.MyId == userId).ToListAsync();
			return list.ToArray();
		}

		public async Task<Message[]> GetMessages(string conversationId)
		{
			var list = await client.GetTable<Message>().Where(m => m.ConversationId == conversationId).ToListAsync();
			return list.ToArray();
		}

		public async Task<Message> SendMessage(Message message)
		{
			await client.GetTable<Message>().InsertAsync(message);
			return message;
		}

		public async Task RegisterPush(string userId, string deviceToken)
		{
			await client.GetTable<Device>().InsertAsync(new Device { UserId = userId, DeviceToken = deviceToken });
		}
    }
}

