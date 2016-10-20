using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;

namespace minrva
{
	public partial class Feed : ContentPage
	{

		TableManager manager;
		// Track whether the user has authenticated. 
		bool authenticated = false;

		public Feed()
		{
			InitializeComponent();
			manager = TableManager.DefaultManager;
			RefreshItems(true, syncItems: false);
		}

		public void Authenticate()
		{
			authenticated = true;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			// Refresh items only when authenticated.
			if (authenticated == true)
			{
				// Set syncItems to true in order to synchronize the data 
				// on startup when running in offline mode.
				await RefreshItems(true, syncItems: false);

			}
		}

		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var game = e.SelectedItem as Boardgames;
			var message = game.Description + "\n\nThis game is available in " + game.Location + " for " + game.Lend_duration + " days\n";
			var alert = false;
			if (Device.OS != TargetPlatform.iOS && game != null)
			{
				alert = await DisplayAlert(game.Name, message, "Borrow", "Cancel");
			}
			else {
				alert = await DisplayAlert(game.Name, message, "Borrow", "Cancel");
			}
			if (alert)
			{
				BorrowItemPage borrowPage = new BorrowItemPage(game);
				await Navigation.PushModalAsync(borrowPage, false);
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
				var available = await manager.GetBoardgamesAsync(syncItems);
				feedList.ItemsSource = available.Where(game => (!String.Equals(game.Owner, sid)) && (game.Borrowed = false));
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
