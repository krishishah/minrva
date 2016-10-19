using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace minrva
{
	public partial class BrowsePage : ContentPage
	{
		BoardgamesManager manager;
		public BrowsePage()
		{
			InitializeComponent();
			manager = BoardgamesManager.DefaultManager;
		}

		public async void OnSearch(object sender, EventArgs e)
		{
			var boardGamesTable = await manager.GetBoardgamesAsync();
			var results = boardGamesTable.Where(b => String.Equals(b.Name, searchBar.Text, StringComparison.CurrentCultureIgnoreCase));
			if (results.Count() > 0)
			{
				resultList.ItemsSource = results;
			}
			else {
				await DisplayAlert("No results found", searchBar.Text + " is currently not available", "Cancel");
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
				BorrowItem(game);
		}

		private async void BorrowItem(Boardgames game)
		{
			await DisplayAlert("Success", "You've borrowed " + game.Name, "Exit");
		}
	}
}
