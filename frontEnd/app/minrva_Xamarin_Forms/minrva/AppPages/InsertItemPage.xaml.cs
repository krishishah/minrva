using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class InsertItemPage : ContentPage
	{
		TableManager manager;
		string descriptionPlaceholder = "Enter Description";

		public InsertItemPage()
		{
			InitializeComponent();

			manager = TableManager.DefaultManager;

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
			if (string.IsNullOrEmpty(newItemName.Text)|| newItemCategory.SelectedIndex.Equals(-1) || string.IsNullOrEmpty(newItemDescription.Text) || string.IsNullOrEmpty(newItemLendDuration.Text) || string.IsNullOrEmpty(newItemLocation.Text))
			{
				await DisplayAlert("Error", "All fields must be completed", "Ok");
			}
			else {
				string sid = await App.Authenticator.GetUserId();
				var boardgames = new Boardgames { Name = newItemName.Text, Description = newItemDescription.Text, Lend_duration = Int32.Parse(newItemLendDuration.Text), Location = newItemLocation.Text, Owner = sid, Borrowed = false, Category = newItemCategory.Items[newItemCategory.SelectedIndex] };
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
	}
}

