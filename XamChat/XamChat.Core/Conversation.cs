using System;

namespace XamChat.Core
{
    public class Conversation
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        //Readonly, used for displaying on UI
        public string Username { get; private set; }
    }
}

