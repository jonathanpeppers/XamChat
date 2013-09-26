using System;
using System.Threading.Tasks;

namespace XamChat.Core
{
    public class FakeWebService : IWebService
    {
        public int SleepDuration { get; set; }

        public FakeWebService()
        {
            SleepDuration = 1000;
        }

        private Task Sleep()
        {
            return Task.Delay(SleepDuration);
        }

        public async Task<User> Login(string username, string password)
        {
            await Sleep();

            return new User { Id = 1, Username = username };
        }

        public Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> GetFriends(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddFriend(string username)
        {
            throw new NotImplementedException();
        }

        public Task<Conversation[]> GetConversations(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Message[]> GetMessages(int conversationId)
        {
            throw new NotImplementedException();
        }
    }
}

