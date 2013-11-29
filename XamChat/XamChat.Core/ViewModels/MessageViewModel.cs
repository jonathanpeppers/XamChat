using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace XamChat.Core
{
    public class MessageViewModel : BaseViewModel
    {
        public Conversation[] Conversations { get; private set; }

        public Conversation Conversation { get; set; }

        public Message[] Messages { get; private set; }

        public string Text { get; set; }

        public async Task GetConversations()
        {
            if (settings.User == null)
                throw new Exception("Not logged in.");

            IsBusy = true;
            try
            {
                Conversations = await service.GetConversations(settings.User.Id);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetMessages()
        {
            if (Conversation == null)
                throw new Exception("No conversation selected.");

            IsBusy = true;
            try
            {
                Messages = await service.GetMessages(Conversation.Id);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task SendMessage()
        {
            if (settings.User == null)
                throw new Exception("Not logged in.");

            if (Conversation == null)
                throw new Exception("No conversation selected.");

            if (string.IsNullOrEmpty(Text))
                throw new Exception("Message is blank.");

            IsBusy = true;
            try
            {
                var message = await service.SendMessage(new Message
                {
                    UserId = settings.User.Id,
					Username = settings.User.Username,
                    ConversationId = Conversation.Id,
					ToId = Conversation.UserId,
                    Text = Text,
                    Date = DateTime.Now,
                });

                //Update our local list of messages
                var messages = new List<Message>();
                if (Messages != null)
                    messages.AddRange(Messages);
                messages.Add(message);

                Messages = messages.ToArray();
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void Clear()
        {
            Conversation = null;
            Messages = null;
            Text = null;
        }
    }
}

