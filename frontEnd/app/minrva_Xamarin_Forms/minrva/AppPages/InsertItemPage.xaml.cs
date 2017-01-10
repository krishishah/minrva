using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Media;
using System.IO;
using System.Linq;
using Plugin.Media.Abstractions;

namespace minrva
{
	public partial class InsertItemPage : ContentPage
	{
		TableManager manager;
		Geocoder geocoder;
		string descriptionPlaceholder = "Enter Description";
		MediaFile itemImageFile = null;


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
			
			//loader.IsRunning = true;
			//loader.IsVisible = true;

			string sid = await App.Authenticator.GetUserId();

			if (string.IsNullOrEmpty(newItemName.Text)|| newItemCategory.SelectedIndex.Equals(-1) || string.IsNullOrEmpty(newItemDescription.Text) || string.IsNullOrEmpty(newItemLendDuration.Text) || string.IsNullOrEmpty(newItemLocation.Text))
			{
				await DisplayAlert("Error", "All fields must be completed", "Ok");
			}
			else {
				
				var location = newItemLocation.Text;
				var latitude = await getLatitudeFromLocation(location);
				var longitude = await getLongitudeFromLocation(location);
				int duration;
				if (int.TryParse(newItemLendDuration.Text, out duration) && duration > 0)
				{
					buttonsPanel.IsVisible = false;
					using (var scope = new ActivityIndicatorScope(syncIndicator, true))
					{
						var boardgames = new Boardgames { Name = newItemName.Text, Description = newItemDescription.Text, Lend_duration = Int32.Parse(newItemLendDuration.Text), Location = location, Latitude = latitude, Longitude = longitude, Owner = sid, Borrowed = false, Category = newItemCategory.Items[newItemCategory.SelectedIndex] };
						await AddItem(boardgames);

						//Add item photo to storage account
						if (itemImageFile != null)
						{
							var boardGamesTable = await manager.GetBoardgamesAsync();
							var itemId = boardGamesTable.Where(b => (String.Equals(b.Owner, sid)))
																		 .OrderByDescending(b => b.CreatedAt)
																		 .Select(b => b.Id)
																		 .ElementAt(0);
							//var itemId = itemsOwnedByCurrentUser[0];
							var itemName = await ImageManager.GenerateItemPhotoName(itemId);
							await ImageManager.UploadImage(itemImageFile.GetStream(), itemName);
							itemImageFile.Dispose();
							itemImageFile = null;
						}

						//loader.IsRunning = false;
						//loader.IsVisible = false;

					}
					buttonsPanel.IsVisible = true;
					await DisplayAlert("Success", "Your item has been added", "Ok");

					// Empty all fields after item has been successfully added
					newItemName.Text = string.Empty;
					newItemDescription.Text = descriptionPlaceholder;
					newItemDescription.TextColor = Color.Gray;
					newItemLendDuration.Text = string.Empty;
					newItemLocation.Text = string.Empty;
					newItemCategory.SelectedIndex = -1;
					newItemName.Unfocus();
					itemImage.Source = null;
				}
				else
				{
					await DisplayAlert("Error", "Number of days must be a positive whole number", "Ok");
				}

			}
		}

		private async Task<double> getLatitudeFromLocation(string location)
		{
			var approximateLocations = await geocoder.GetPositionsForAddressAsync(location);
			var enumerator = approximateLocations.GetEnumerator();
			enumerator.MoveNext();
			var position = enumerator.Current;
			return position.Latitude;
		}

		private async Task<double> getLongitudeFromLocation(string location)
		{
			var approximateLocations = await geocoder.GetPositionsForAddressAsync(location);
			var enumerator = approximateLocations.GetEnumerator();
			enumerator.MoveNext();
			var position = enumerator.Current;
			return position.Longitude;
		}

		public async void OnUpload(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsPickPhotoSupported)
			{
				await DisplayAlert("Photos Not Supported!", "Permission not granted to photo gallery", "OK");
				return;
			}
			itemImageFile = await CrossMedia.Current.PickPhotoAsync();

			if (itemImageFile == null)
				return;


			itemImage.Source = ImageSource.FromStream(() =>
			{
				return itemImageFile.GetStream();
			});
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

