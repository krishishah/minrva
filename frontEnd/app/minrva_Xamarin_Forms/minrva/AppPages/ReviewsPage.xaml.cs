using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class ReviewsPage : ContentPage
	{
		TableManager manager;
		string reviewedEntityID;

		public ReviewsPage(string reviewedID, bool isItem)
		{
			InitializeComponent();
			reviewedEntityID = reviewedID;
			manager = TableManager.DefaultManager;
			updateTitle(isItem);
			RefreshItems(false, syncItems: false);
		}

		async void updateTitle(bool isItem)
		{
			string name;
			if (isItem)
			{
				var itemsTable = await manager.GetBoardgamesAsync();
				name = itemsTable.Where(item => string.Equals(reviewedEntityID, item.Id)).Select(col => col.Name).ElementAt(0);
			}
			else
			{
				var usersTable = await manager.GetUserAsync();
				User user = usersTable.Where(u => string.Equals(reviewedEntityID, u.UserId)).ElementAt(0);
				name = String.Format("{0} {1}", user.FirstName, user.LastName);
			}
			pageTitle.Text = "Reviews for " + name;
		}

		public async void OnRefresh(object sender, EventArgs e)
		{
			var list = (ListView)sender;
			Exception error = null;
			try
			{
				await RefreshItems(false, true);
			}
			catch (Exception ex)
			{
				error = ex;
			}
			finally
			{
				list.EndRefresh();
			}

			if (error != null)
			{
				await DisplayAlert("Refresh Error", "Couldn't refresh data (" + error.Message + ")", "OK");
			}
		}

		public async void OnSyncItems(object sender, EventArgs e)
		{
			await RefreshItems(true, true);
		}

		private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
		{
			var reviews = await manager.GetRatingsAsync();
			var entityReviews = reviews.Where(r => string.Equals(r.RatedID, reviewedEntityID));
			var usersTable = await manager.GetUserAsync();
			foreach (Ratings rating in entityReviews)
			{
				User reviewingUser = usersTable.Where(u => string.Equals(rating.ReviewerID, u.UserId)).ElementAt(0);
				rating.Reviewer = String.Format("{0} {1}", reviewingUser.FirstName, reviewingUser.LastName);
			}
			allReviews.ItemsSource = entityReviews;
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
