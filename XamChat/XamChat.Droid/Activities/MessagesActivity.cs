using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamChat.Core;
using Message = XamChat.Core.Message;

namespace XamChat.Droid
{
    [Activity(Label = "Messages")]			
    public class MessagesActivity : BaseActivity<MessageViewModel>
    {
        ListView listView;
        EditText messageText;
        Button sendButton;
        Adapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Title = viewModel.Conversation.Username;

            SetContentView(Resource.Layout.Messages);

            listView = FindViewById<ListView>(Resource.Id.messageList);
            messageText = FindViewById<EditText>(Resource.Id.messageText);
            sendButton = FindViewById<Button>(Resource.Id.sendButton);

            listView.Adapter =
                adapter = new Adapter();

            sendButton.Click += async (sender, e) => 
            {
                viewModel.Text = messageText.Text;

                try
                {
                    await viewModel.SendMessage();

                    messageText.Text = string.Empty;
                }
                catch (Exception exc)
                {
                    DisplayError(exc);
                }
            };
        }

        protected async override void OnResume()
        {
            base.OnResume();

            try
            {
                await viewModel.GetMessages();

                adapter.NotifyDataSetInvalidated();
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }

        class Adapter : BaseAdapter<Message>
        {
            readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            readonly ISettings settings = ServiceContainer.Resolve<ISettings>();

            public override long GetItemId(int position)
            {
                return messageViewModel.Messages [position].Id;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                throw new NotImplementedException();
            }

            public override int Count
            {
                get { return messageViewModel.Messages == null ? 0 : messageViewModel.Messages.Length; }
            }

            public override Message this[int index]
            {
                get { return messageViewModel.Messages [index]; }
            }

            public override int ViewTypeCount
            {
                get { return 2; }
            }

            public override int GetItemViewType(int position)
            {
                var message = this [position];
                return message.UserId == settings.User.Id ? 0 : 1;
            }
        }
    }
}

