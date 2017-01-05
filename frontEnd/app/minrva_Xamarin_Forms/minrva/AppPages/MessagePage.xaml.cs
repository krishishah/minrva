using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using System.Linq;

namespace minrva
{
	public partial class MessagePage : ContentPage
	{
		string descriptionPlaceholder = "Enter Message";
		public String Username { get; set;}
		public User Receiver;
		TableManager manager;
		bool authenticated = false;


		public MessagePage(User name, String itemId)
		{
			InitializeComponent();
			manager = TableManager.DefaultManager;
			Username = name.FirstName;
			Receiver = name;
			BindingContext = this;
			RefreshItems(false, syncItems: false);
			//refreshOnTimer();
		}


		public void Authenticate()
		{
			authenticated = true;
		}


		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await RefreshItems(false, syncItems: false);
		}


		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		async void ItemsLentBorrowedPage(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ItemsLentBorrowed());
		}

		public async void OnRefresh(object sender, EventArgs e)
		{
			var list = (ListView)sender;
			Exception error = null;
			try
			{
				await RefreshItems(false, true);
			}
			catch (Exception ex)
			{
				error = ex;
			}
			finally
			{
				list.EndRefresh();
			}

			if (error != null)
			{
				await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
			}
		}

		public async void OnSyncItems(object sender, EventArgs e)
		{
			await RefreshItems(true, true);
		}

		void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			if (Equals(newMessage.Text, descriptionPlaceholder))
			{
				newMessage.Text = string.Empty;
				newMessage.TextColor = Color.Black;
			}
		}

		async Task AddItem(Message item)
		{
			await manager.SaveMessageAsync(item);
		}


		private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
		{
			string sid = await App.Authenticator.GetUserId();
			
			
			using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
			{
				var message = await manager.GetMessageAsync(syncItems);

				var msgs = message.Where(m => (String.Equals(m.Sender, sid) && String.Equals(m.Receiver, Receiver.UserId))
				                                || (String.Equals(m.Receiver, sid) && String.Equals(m.Sender, Receiver.UserId)));
				foreach (Message m in msgs)
				{
					if (String.Equals(m.Sender, sid))
					{
						m.Alignment = LayoutOptions.EndAndExpand;
						m.MessageBoxColour = "#b35050";
						m.TextColour = "White";
					}
					else 
					{
						m.Alignment = LayoutOptions.StartAndExpand;
						m.MessageBoxColour = "White";
						m.TextColour = "Black";
					}
				}
				messageList.ItemsSource = msgs;


			}
		}

		public async void SendMessageCommand(object sender, EventArgs e)
		{
			if (!Equals(newMessage.Text, descriptionPlaceholder))
			{
				string sid = await App.Authenticator.GetUserId();
				var chat = await manager.GetChatAsync();
				var chatId = chat.Where(c => ((String.Equals(c.Lender, sid)) && String.Equals(c.Borrower, Receiver.UserId)) ||
									   ((String.Equals(c.Borrower, sid)) && String.Equals(c.Lender, Receiver.UserId))).ElementAt(0);
				var message = new Message { Sender = sid, Receiver = Receiver.UserId, Text = newMessage.Text, ChatId = chatId.Id };
				await AddItem(message);
				await RefreshItems(false, syncItems: false);

				var last = messageList.ItemsSource.Cast<object>().LastOrDefault();
				messageList.ScrollTo(last, ScrollToPosition.MakeVisible, true);

				newMessage.Text = descriptionPlaceholder;
				newMessage.TextColor = Color.Gray;
			}
		}

		private class ActivityIndicatorScope : IDisposable
		{
			private bool showIndicator;
			private ActivityIndicator indicator;
			private Task indicatorDelay;

			public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
			{
				this.indicator = indicator;
				this.showIndicator = showIndicator;

				if (showIndicator)
				{
					indicatorDelay = Task.Delay(2000);
					SetIndicatorActivity(true);
				}
				else
				{
					indicatorDelay = Task.FromResult(0);
				}
			}

			private void SetIndicatorActivity(bool isActive)
			{
				this.indicator.IsVisible = isActive;
				this.indicator.IsRunning = isActive;
			}

			public void Dispose()
			{
				if (showIndicator)
				{
					indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
				}
			}
		}
	}
}
