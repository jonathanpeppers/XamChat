using System;
using System.Threading.Tasks;

namespace XamChat.Core
{
	public interface IWebService
	{
		Task<User> Login(string username, string password);

		Task<User> Register(User user);

		Task<User[]> GetFriends(string userId);

		Task<User> AddFriend(string userId, string username);

		Task<Conversation[]> GetConversations(string userId);

		Task<Message[]> GetMessages(string conversationId);

		Task<Message> SendMessage(Message message);
	}
}

