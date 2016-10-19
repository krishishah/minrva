using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class InsertItemPage : ContentPage
	{
		BoardgamesManager manager;

		public InsertItemPage()
		{
			InitializeComponent();

			manager = BoardgamesManager.DefaultManager;
		}

		// Data methods
		async Task AddItem(Boardgames item)
		{
			await manager.SaveTaskAsync(item);
		}

		public async void OnAdd(object sender, EventArgs e)
		{
			var boardgames = new Boardgames { Name = newItemName.Text, Description = newItemDescription.Text, Lend_duration = Int32.Parse(newItemLendDuration.Text), Location = newItemLocation.Text};
			await AddItem(boardgames);

			newItemName.Text = string.Empty;
			newItemDescription.Text = string.Empty;
			newItemLendDuration.Text = string.Empty;
			newItemLocation.Text = string.Empty;
			newItemCategory.SelectedIndex = 0;
			newItemName.Unfocus();
		}
	}
}

