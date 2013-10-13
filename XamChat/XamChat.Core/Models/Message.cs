using System;

namespace XamChat.Core
{
    public class Message
    {
        public int Id { get; set; }

        public int ConversationId { get; set; }

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }
    }
}

