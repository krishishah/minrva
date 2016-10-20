using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class MessagesPage : ContentPage
	{
		TableManager tableManager;

		public MessagesPage()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			RefreshItems(true, syncItems: false);
		}

		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var requested = e.SelectedItem as Request;
			var alert = await DisplayAlert("Borrowing request", "Would you like to lend " + requested.RequestedItem.Name + " to " + requested.BorrowingUser.FirstName + " " + requested.BorrowingUser.LastName + " from " + requested.StartDate + " to " + requested.EndDate, "Yes", "No");
			if (alert)
			{
				requested.Accepted = true;
				await tableManager.SaveRequestAsync(requested);
				Boardgames game = requested.RequestedItem;
				game.Borrowed = true;
				await tableManager.SaveBoardgamesAsync(game);
			}
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
				var lenderItemRequests = reqs.Where(r => (String.Equals(r.Lender, sid)) && (r.Accepted = false));
				foreach (Request r in lenderItemRequests)
				{
					r.BorrowingUser = users.Where(user => String.Equals(r.Borrower, user.UserId)).ElementAt(0);
					r.RequestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
				}
				requestsList.ItemsSource = lenderItemRequests;
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
