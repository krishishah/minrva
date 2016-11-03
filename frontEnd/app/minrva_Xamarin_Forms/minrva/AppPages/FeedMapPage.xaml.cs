using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace minrva
{
	public partial class FeedMapPage : ContentPage
	{
		TableManager manager;
		Position currentPosition;
		IEnumerable<Boardgames> list_of_items;

		public FeedMapPage(Position position, IEnumerable<Boardgames> list_of_items)
		{
			InitializeComponent();
			manager = TableManager.DefaultManager;
			currentPosition = position;
			this.list_of_items = list_of_items;
			displayItems();
		}

		private async void displayItems()
		{
			MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(currentPosition, Distance.FromMiles(5.0)));
			//var available = await manager.GetBoardgamesAsync();
			//string sid = await App.Authenticator.GetUserId();
			//var list = available.Where(game => !String.Equals(game.Owner, sid) && (game.Borrowed == false));
			var list = list_of_items;
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
