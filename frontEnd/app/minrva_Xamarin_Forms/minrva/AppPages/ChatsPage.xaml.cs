using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class ChatsPage : ContentPage
	{
		TableManager tableManager;

		public ChatsPage()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			RefreshItems(true, syncItems: false);
		}

		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var chatDetails = e.SelectedItem as ChatDetails;
			Boardgames requestedItem = chatDetails.RequestedItem;
			User recipient = chatDetails.Recipient;
			MessagePage messagePage = new MessagePage(recipient.FirstName, requestedItem.Id);
			await Navigation.PushModalAsync(messagePage, false);
		}

		// http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#pulltorefresh
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

		private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
		{
			using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
			{
				string sid = await App.Authenticator.GetUserId();
				var reqs = await tableManager.GetRequestAsync(syncItems);
				var chats = await tableManager.GetChatAsync(syncItems);
				var games = await tableManager.GetBoardgamesAsync(syncItems);
				var users = await tableManager.GetUserAsync(syncItems);
				//var lenderItemRequests = reqs.Where(r => (String.Equals(r.Lender, sid)) && (r.Accepted == false));
				var itemLendRequests = reqs.Where(r => (String.Equals(r.Lender,sid)) && (String.Equals(r.Accepted,"True")));
				var itemBorrowRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (String.Equals(r.Accepted, "True")));

				List<ChatDetails> acceptedMsgs = new List<ChatDetails>();
				bool seen = false;

				foreach (Request r in itemLendRequests)
				{
					User borrowingUser = users.Where(user => String.Equals(r.Borrower, user.UserId)).ElementAt(0);
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);


					foreach (ChatDetails c in acceptedMsgs)
					{
						if (String.Equals(borrowingUser.Id, c.Recipient.Id))
						{
							seen = true;
							Debug.WriteLine("Borrowing user if: {0}", borrowingUser.FirstName);
						}
					}

					if (!seen)
					{
						acceptedMsgs.Add(new ChatDetails(requestedItem, borrowingUser));
					}

					seen = false;
				}


				foreach (Request r in itemBorrowRequests)
				{
					User borrowingUser = users.Where(user => String.Equals(r.Borrower, user.UserId)).ElementAt(0);
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);


					foreach (ChatDetails c in acceptedMsgs)
					{
						if ((String.Equals(lendingUser.Id, c.Recipient.Id)) || (String.Equals(borrowingUser.Id, c.Recipient.Id)))
						{
							seen = true;
							Debug.WriteLine("Lending user if: {0}", lendingUser.FirstName);
						}
					}

					if (!seen)
					{
						acceptedMsgs.Add(new ChatDetails(requestedItem, lendingUser));
					}

					seen = false;
				}

				acceptedList.ItemsSource = reqs.Where(r => (String.Equals(r.Lender, sid)) && (String.Equals(r.Accepted, "True")));
				//acceptedList.ItemsSource = chats.Where(r => (String.Equals(r.Lender, sid)) || (String.Equals(r.Borrower, sid));
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