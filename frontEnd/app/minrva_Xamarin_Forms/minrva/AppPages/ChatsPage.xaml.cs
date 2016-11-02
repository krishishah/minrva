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
				var games = await tableManager.GetBoardgamesAsync(syncItems);
				var users = await tableManager.GetUserAsync(syncItems);
				//var lenderItemRequests = reqs.Where(r => (String.Equals(r.Lender, sid)) && (r.Accepted == false));
				var borrowItemRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (String.Equals(r.Accepted,"True")));
				List<ChatDetails> acceptedMsgs = new List<ChatDetails>();

				foreach (Request r in borrowItemRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					acceptedMsgs.Add(new ChatDetails(requestedItem, lendingUser));
				}

				acceptedList.ItemsSource = acceptedMsgs;
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