using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace minrva
{
	public partial class FeedMapPage : ContentPage
	{
		TableManager manager;
		Position currentPosition;

		public FeedMapPage(Position position)
		{
			InitializeComponent();
			manager = TableManager.DefaultManager;
			currentPosition = position;
			displayItems();
		}

		private async void displayItems()
		{
			MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(currentPosition, Distance.FromMiles(5.0)));
			var available = await manager.GetBoardgamesAsync();
			string sid = await App.Authenticator.GetUserId();
			var list = available.Where(game => !String.Equals(game.Owner, sid) && (game.Borrowed == false));
			foreach (var item in list)
			{
				Position position = new Position(item.Latitude, item.Longitude);
				var pin = new Pin
				{
					Type = PinType.Place,
					Position = position,
					Label = item.Name,
					Address = item.Description
				};
				MyMap.Pins.Add(pin);
			}

		}

		void gotoFeedPage(object sender, EventArgs e)
		{
			App.Current.MainPage = new MainTabContainer();
		}

	}
}
