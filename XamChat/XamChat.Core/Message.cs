using System;

namespace XamChat.Core
{
    public class Message
    {
        public int Id { get; set; }

        public int ConversationId { get; set; }

        public int UserId { get; set; }

        //Readonly, used for displaying on UI
        public string Username { get; private set; }

        public string Text { get; set; }
    }
}

