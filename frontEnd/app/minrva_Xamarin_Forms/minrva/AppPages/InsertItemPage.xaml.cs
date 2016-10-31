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

			var boardgames = new Boardgames { Name = newItemName.Text, Description = newItemDescription.Text, Lend_duration = Int32.Parse(newItemLendDuration.Text), Location = newItemLocation.Text, Owner = sid, Borrowed = false};
			await AddItem(boardgames);

			newItemName.Text = string.Empty;
			newItemDescription.Text = string.Empty;
			newItemLendDuration.Text = string.Empty;
			newItemLocation.Text = string.Empty;
			newItemCategory.Title = "Enter Category";
			newItemName.Unfocus();
		}

	}
}

