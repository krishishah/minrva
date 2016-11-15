using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Syncfusion.SfRating.XForms;

namespace minrva
{
	public partial class Profile : ContentPage
	{

		TableManager tableManager;
		string sid;

		public Profile()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			displayDetails();
		}

		private async void displayDetails()
		{
			await displayUserName();
			await displayLendBorrowCount();
			this.sid = await App.Authenticator.GetUserId();
			userRating.Value = await getUserRating();
			await RefreshItems(true, syncItems: false);
		}

		async Task<double> getUserRating()
		{
			var ratingsTable = await tableManager.GetRatingsAsync();
			var ratings = ratingsTable.Where(r => String.Equals(sid, r.RatedID)).Select(rating => rating.Rating);
			return ratings.Average();
		}

		async Task displayUserName()
		{
			var usersTable = await tableManager.GetUserAsync();
			string sid = await App.Authenticator.GetUserId();
			var user = usersTable.Where(u => String.Equals(sid, u.UserId)).ElementAt(0);
			Name.Text = String.Format("{0} {1}", user.FirstName, user.LastName);
		}

		async Task displayLendBorrowCount()
		{
			string sid = await App.Authenticator.GetUserId();
			var requestTable = await tableManager.GetRequestAsync();
			var itemsTable = await tableManager.GetBoardgamesAsync();
			int lendCount = itemsTable.Where(item => String.Equals(sid, item.Owner)).Count();
			int borrowCount = requestTable.Where(user => String.Equals(sid, user.Borrower)).Count();
			LendBorrow.Text = String.Format("L:{0} | B:{1}", lendCount, borrowCount);
		}


		async void Clicked_Logout(object sender, EventArgs e)
		{
			bool loggedOut = false;

			if (App.Authenticator != null)
				loggedOut = await App.Authenticator.LogoutAsync();

			if (loggedOut)
				App.Current.MainPage = new LoginPage();
			else
				await DisplayAlert("Logout Error", "You have failed to log out.", "OK");
		}

		public async void ViewReviews(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ReviewsPage(sid, false));
		}

		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var game = e.SelectedItem as Boardgames;
			string borrowMsg = "";
			if (game.Borrowed)
			{
				var requests = await tableManager.GetRequestAsync();
				Request req = requests.Where(r => string.Equals(r.Lender, this.sid) && string.Equals(r.ItemId, game.Id)).ElementAt(0);
				var users = await tableManager.GetUserAsync();
				User borrower = users.Where(u => string.Equals(u.UserId, req.Borrower)).ElementAt(0);
				var alert = await DisplayAlert("Item information", String.Format("You have lent this game to {0} {1} from {2} to {3}", borrower.FirstName, borrower.LastName, req.StartDate, req.EndDate), "Mark As Returned", "Cancel");
				if (alert)
				{
					await Navigation.PushModalAsync(new LeaveReviewPage(borrower, game, false));
					req.Accepted = "Returned";
					await tableManager.SaveRequestAsync(req);
				}
			}
			else
			{
				borrowMsg = String.Format("This game is still available for users to borrow in {0} for {1} days\n", game.Location, game.Lend_duration);
				await DisplayAlert("Item information", borrowMsg, "Ok");
			}

		}

		// http://developer.xamarin.com/guides/cross-platform/xamarin-forms/working-with/listview/#pulltorefresh
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
				string sid = await App.Authenticator.GetUserId();
				var lendingItems = await tableManager.GetBoardgamesAsync(syncItems);
				myItems.ItemsSource = lendingItems.Where(game => (String.Equals(game.Owner, sid)));
		}

		protected override void OnAppearing()
		{
			displayDetails();
		}
	}

}
