using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace minrva
{
	public partial class InsertItemPage : ContentPage
	{
		TableManager manager;
		Geocoder geocoder;
		string descriptionPlaceholder = "Enter Description";
		public InsertItemPage()
		{
			InitializeComponent();

			manager = TableManager.DefaultManager;
			geocoder = new Geocoder();
			newItemName.Keyboard = Keyboard.Create(KeyboardFlags.All);
			newItemDescription.Keyboard = Keyboard.Create(KeyboardFlags.All);
			newItemLocation.Keyboard = Keyboard.Create(KeyboardFlags.All);
		}

		void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			if (Equals(newItemDescription.Text, descriptionPlaceholder))
			{
				newItemDescription.Text = string.Empty;
				newItemDescription.TextColor = Color.Black;
			}
		}

		// Data methods
		async Task AddItem(Boardgames item)
		{
			await manager.SaveBoardgamesAsync(item);
		}

		public async void OnAdd(object sender, EventArgs e)
		{
			string sid = await App.Authenticator.GetUserId();

			if (string.IsNullOrEmpty(newItemName.Text)|| newItemCategory.SelectedIndex.Equals(-1) || string.IsNullOrEmpty(newItemDescription.Text) || string.IsNullOrEmpty(newItemLendDuration.Text) || string.IsNullOrEmpty(newItemLocation.Text))
			{
				await DisplayAlert("Error", "All fields must be completed", "Ok");
			}
			else {
				var location = newItemLocation.Text;
				var latitude = await getLatitudeFromLocation(location);
				var longitude = await getLongitudeFromLocation(location);
				var boardgames = new Boardgames { Name = newItemName.Text, Description = newItemDescription.Text, Lend_duration = Int32.Parse(newItemLendDuration.Text), Location = location, Latitude = latitude, Longitude = longitude, Owner = sid, Borrowed = false, Category = newItemCategory.Items[newItemCategory.SelectedIndex] };
				await AddItem(boardgames);
				await DisplayAlert("Success", "Your item has been added", "Ok");

				newItemName.Text = string.Empty;
				newItemDescription.Text = descriptionPlaceholder;
				newItemDescription.TextColor = Color.Gray;
				newItemLendDuration.Text = string.Empty;
				newItemLocation.Text = string.Empty;
				newItemCategory.SelectedIndex = -1;
				newItemName.Unfocus();
			}
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

