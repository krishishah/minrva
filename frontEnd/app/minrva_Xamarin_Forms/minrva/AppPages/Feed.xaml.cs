using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using System.IO;

namespace minrva
{
	public partial class Feed : ContentPage
	{

		TableManager manager;
		// Track whether the user has authenticated. 
		bool authenticated = false;
		double cLat;
		double cLon;
		Plugin.Geolocator.Abstractions.IGeolocator locator;
		Position position;
		IEnumerable<Boardgames> listOfItems;

		public Feed()
		{
			InitializeComponent();
			manager = TableManager.DefaultManager;
			locator = CrossGeolocator.Current;
			RefreshItems(false, syncItems: false);
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

		public async void OnSearch(object sender, EventArgs e)
		{
			var boardGamesTable = await manager.GetBoardgamesAsync();
			string sid = await App.Authenticator.GetUserId();
			var results = boardGamesTable.Where(b => (!String.Equals(b.Owner, sid)) && (String.Equals(b.Name, searchBar.Text, StringComparison.CurrentCultureIgnoreCase)) && b.Borrowed == false);
			if (results.Count() > 0)
			{
				feedList.ItemsSource = results;
			}
			else {
				await DisplayAlert("No results found", searchBar.Text + " is currently not available", "Cancel");
			}
		}

		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as BoardgamesViewModel;
			var userTable = await manager.GetUserAsync();
			var itemTable = await manager.GetBoardgamesAsync();
			var itemObj = itemTable.Where(x => String.Equals(x.Id, item.Id)).ElementAt(0);
			await Navigation.PushModalAsync(new ItemViewPage(itemObj, userTable.Where(u=>string.Equals(u.UserId, item.Owner)).ElementAt(0)));
			await RefreshItems(false, syncItems: false);
		}

		public async void ShowCategory(object sender, EventArgs e)
		{
			string sid = await App.Authenticator.GetUserId();
			var available = await manager.GetBoardgamesAsync();
			string category = selectCategory.Items[selectCategory.SelectedIndex];
			if (string.Equals(category, "All"))
			{
				feedList.ItemsSource = available.Where(game => (!String.Equals(game.Owner, sid)) && (!game.Borrowed));
			}
			else
			{
				feedList.ItemsSource = available.Where(game => (!String.Equals(game.Owner, sid)) && (!game.Borrowed) && (String.Equals(game.Category, category)));
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

			if (selectCategory.IsVisible)
			{
				var position = new Plugin.Geolocator.Abstractions.Position();
				using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
				{
					try
					{
						position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
					}
					catch (Exception e)
					{
						Debug.WriteLine(String.Format("Error: {0}", e.ToString()));
					}

					cLat = position.Latitude;
					cLon = position.Longitude;
					this.position = new Position(cLat, cLon);
					string sid = await App.Authenticator.GetUserId();
					var available = await manager.GetBoardgamesAsync(syncItems);
					IEnumerable<Boardgames> list = Enumerable.Empty<Boardgames>();
					if (selectCategory.SelectedIndex == -1)
					{
						list = available.Where(game => (!String.Equals(game.Owner, sid)) && (!game.Borrowed));
					}
					else {
						string category = selectCategory.Items[selectCategory.SelectedIndex];
						if (string.Equals(category, "All"))
						{
							list = available.Where(game => (!String.Equals(game.Owner, sid)) && (!game.Borrowed));
						}
						else
						{
							list = available.Where(game => (!String.Equals(game.Owner, sid)) && (!game.Borrowed) && (String.Equals(game.Category, category)));
						}
					}

					List<BoardgamesViewModel> feedViewList = new List<BoardgamesViewModel>();

					foreach (Boardgames x in list)
					{
						BoardgamesViewModel listElement = new BoardgamesViewModel();

						listElement.Id = x.Id;
						listElement.Name = x.Name;
						listElement.Description = x.Description;
						listElement.Owner = x.Owner;
						listElement.Location = x.Location;
						listElement.Category = x.Category;

						//Temporary hack to load item image as we currently only allow 1 image per item to be downloaded
						byte[] itemImageBytes = await ImageManager.GetImage(String.Format("{0}_0",x.Id));
						listElement.ImageSource = "minrva_icon.png";

						if (itemImageBytes != null)
							listElement.ImageSource = ImageSource.FromStream(() => new MemoryStream(itemImageBytes));

						listElement.Distance = calculateDistance(cLat, cLon, x.Latitude, x.Longitude);

						feedViewList.Add(listElement);
					}

					feedViewList.Sort((x, y) => x.Distance.CompareTo(y.Distance));

					feedList.ItemsSource = feedViewList;
					listOfItems = list;
				}
			}
		}

		private double calculateDistance(double lat1, double lon1, double lat2, double lon2)
		{
			double theta = lon1 - lon2;
			double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
			dist = Math.Acos(dist);
			dist = rad2deg(dist);
			dist = dist * 60 * 1.1515;
			dist = dist * 0.8684;
			return (dist);
		}

		private double deg2rad(double deg)
		{
			return (deg * Math.PI / 180.0);
		}

		private double rad2deg(double rad)
		{
			return (rad / Math.PI * 180.0);
		}


		public async void CancelPressed(object sender, EventArgs e)
		{
			if (searchBar.Text == null)
			{
				selectCategory.IsVisible = true;
				await RefreshItems(false, syncItems: false);
			}
		}

		public async void Searching(object sender, EventArgs e)
		{
			selectCategory.IsVisible = false;
			feedList.ItemsSource = null;
		}

		async void gotoFeedMapPage(object sender, EventArgs e)
		{
			if (listOfItems == null || position == null)
			{
				await DisplayAlert("Alert", "List of Items not yet loaded", "OK");
			}
			else {
				Application.Current.MainPage = new FeedMapPage(position: position, list_of_items: listOfItems);
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
