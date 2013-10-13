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
#if NUNIT
            return Task.FromResult(true);
#else
            return Task.Delay(SleepDuration);
#endif
        }

        public async Task<User> Login(string username, string password)
        {
            await Sleep();

            return new User { Id = 1, Username = username };
        }

        public async Task<User> Register(User user)
        {
            await Sleep();

            return user;
        }

        public async Task<User[]> GetFriends(int userId)
        {
            await Sleep();

            return new[]
            {
                new User { Id = 2, Username = "bobama" },
                new User { Id = 3, Username = "bobloblaw" },
                new User { Id = 4, Username = "gmichael" },
                new User { Id = 5, Username = "scooper" },
            };
        }

        public async Task<User> AddFriend(int userId, string username)
        {
            await Sleep();

            return new User { Id = 6, Username = username };
        }

        public async Task<Conversation[]> GetConversations(int userId)
        {
            await Sleep();

            return new[]
            {
                new Conversation { Id = 1, UserId = 2, Username = "bobama", LastMessage = "Hey!" },
                new Conversation { Id = 2, UserId = 3, Username = "bobloblaw", LastMessage = "Have you seen that new movie?" },
                new Conversation { Id = 3, UserId = 4, Username = "gmichael", LastMessage = "What?" },
            };
        }

        public async Task<Message[]> GetMessages(int conversationId)
        {
            await Sleep();

            return new[]
            {
                new Message { Id = 1, ConversationId = conversationId, UserId = 2, Username = "bobama", Text = "Hey", Date = DateTime.Now.AddDays(-1).AddMinutes(-5) },
                new Message { Id = 2, ConversationId = conversationId, UserId = 1, Username = "testuser", Text = "What's up?", Date = DateTime.Now.AddDays(-1) },
                new Message { Id = 1, ConversationId = conversationId, UserId = 2, Username = "bobama", Text = "Have you seen that new movie?", Date = DateTime.Now.AddMinutes(-1) },
                new Message { Id = 2, ConversationId = conversationId, UserId = 1, Username = "testuser", Text = "It's great!", Date = DateTime.Now.AddSeconds(-30) },
            };
        }

        public async Task<Message> SendMessage(Message message)
        {
            await Sleep();

            return message;
        }
    }
}

