using System;
using System.Threading.Tasks;

namespace XamChat.Core
{
    public interface IWebService
    {
        Task<User> Login(string username, string password);

        Task<User> Register(User user);

        Task<User[]> GetFriends(int userId);

        Task<User> AddFriend(string username);

        Task<Conversation[]> GetConversations(int userId);

        Task<Message[]> GetMessages(int conversationId);
    }
}

