using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace minrva
{
	public partial class InsertItemPage : ContentPage
	{
		TableManager manager;
		Geocoder geocoder;

		public InsertItemPage()
		{
			InitializeComponent();

			manager = TableManager.DefaultManager;
			geocoder = new Geocoder();
		}

		// Data methods
		async Task AddItem(Boardgames item)
		{
			await manager.SaveBoardgamesAsync(item);
		}

		public async void OnAdd(object sender, EventArgs e)
		{
			string sid = await App.Authenticator.GetUserId();

			var location = newItemLocation.Text;
			var latitude = await getLatitudeFromLocation(location);
			var longitude = await getLongitudeFromLocation(location);

			var boardgames = new Boardgames { Name = newItemName.Text, Description = newItemDescription.Text, Lend_duration = Int32.Parse(newItemLendDuration.Text), Location = location, Latitude = latitude, Longitude = longitude, Owner = sid, Borrowed = false};
			await AddItem(boardgames);

			newItemName.Text = string.Empty;
			newItemDescription.Text = string.Empty;
			newItemLendDuration.Text = string.Empty;
			newItemLocation.Text = string.Empty;
			newItemCategory.Title = "Enter Category";
			newItemName.Unfocus();
		}

		private async Task<double> getLatitudeFromLocation(string location)
		{
			var approximateLocations = await geocoder.GetPositionsForAddressAsync(location);
			var enumerator = approximateLocations.GetEnumerator();
			var position = enumerator.Current;
			System.Diagnostics.Debug.WriteLine("Latitude: " + position.Latitude);
			return position.Latitude;
		}

		private async Task<double> getLongitudeFromLocation(string location)
		{
			var approximateLocations = await geocoder.GetPositionsForAddressAsync(location);
			var enumerator = approximateLocations.GetEnumerator();
			var position = enumerator.Current;
			System.Diagnostics.Debug.WriteLine("Longitude: " + position.Longitude);
			return position.Longitude;
		}

	}
}

