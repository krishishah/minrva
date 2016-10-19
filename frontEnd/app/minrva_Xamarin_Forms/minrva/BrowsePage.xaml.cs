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
				var game = results.ElementAt(0);
				var message = game.Description + "\n\nThis game is available for " + game.Lend_duration + " days\n";
				var alert = await DisplayAlert(game.Name, message, "Borrow", "Cancel");
				if (alert)
				{
					BorrowItem(game);
				}
				searchBar.Text = "";
				
			}
			else {
				await DisplayAlert("Result", searchBar.Text + " is currently not available", "Cancel");
			}

		}

		private async void BorrowItem(Boardgames game)
		{
			await DisplayAlert("Success", "You've borrowed " + game.Name, "Exit");
		}
	}
}
