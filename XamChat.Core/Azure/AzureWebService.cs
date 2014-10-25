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
	}
}

