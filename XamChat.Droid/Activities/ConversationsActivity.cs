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

namespace XamChat.Droid
{
	[Activity(Label = "Conversations")]
	public class ConversationsActivity : BaseActivity<MessageViewModel>
	{
		ListView listView;
		Adapter adapter;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.Conversations);
			listView = FindViewById<ListView>(Resource.Id.conversationsList);
			listView.Adapter = 
				adapter = new Adapter(this);

			listView.ItemClick += (sender, e) => 
			{
				viewModel.Conversation = adapter[e.Position];

				StartActivity(typeof(MessagesActivity));
			};
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.ConversationsMenu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			if (item.ItemId == Resource.Id.addFriendMenu)
			{
				StartActivity(typeof(FriendsActivity));
			}
			return base.OnOptionsItemSelected(item);
		}

		protected async override void OnResume()
		{
			base.OnResume();
			try
			{
				await viewModel.GetConversations();
				adapter.NotifyDataSetInvalidated();
			}
			catch (Exception exc)
			{
				DisplayError(exc);
			}
		}

		class Adapter : BaseAdapter<Conversation>
		{
			readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
			readonly LayoutInflater inflater;

			public Adapter(Context context)
			{
				inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
			}

			public override long GetItemId(int position)
			{
				//This is an abstract method, just a simple implementation
				return position;
			}

			public override View GetView(int position, View convertView, ViewGroup parent)
			{
				if (convertView == null)
				{
					convertView = inflater.Inflate(Resource.Layout.ConversationListItem, null);
				}
				var conversation = this [position];
				var username = convertView.FindViewById<TextView>(Resource.Id.conversationUsername);
				var lastMessage = convertView.FindViewById<TextView>(Resource.Id.conversationLastMessage);
				username.Text = conversation.Username;
				lastMessage.Text = conversation.LastMessage;
				return convertView;
			}

			public override int Count
			{
				get { return messageViewModel.Conversations == null ? 0 : messageViewModel.Conversations.Length; }
			}

			public override Conversation this[int index]
			{
				get { return messageViewModel.Conversations [index]; }
			}
		}
	}
}

