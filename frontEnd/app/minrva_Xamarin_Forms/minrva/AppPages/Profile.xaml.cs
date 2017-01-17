using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using Syncfusion.SfRating.XForms;
using System.IO;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace minrva
{
	public partial class Profile : ContentPage
	{

		TableManager tableManager;
		string sid;
		MediaFile profilePictureFile = null;

		public Profile()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			displayDetails();
		}

		private async void displayDetails()
		{
			this.sid = await App.Authenticator.GetUserId();
			await displayUserName();
			await displayLendBorrowCount();
			userRating.Value = await getUserRating();
			await displayProfilePicture();
			await displayGemAndRank();
			await RefreshItems(true, syncItems: false);
		}

		async Task<double> getUserRating()
		{
			var ratingsTable = await tableManager.GetRatingsAsync();
			var ratings = ratingsTable.Where(r => String.Equals(sid, r.RatedID)).Select(rating => rating.Rating);
			if (ratings.Count() > 0)
			{
				return ratings.Average();
			}
			else
			{
				return 0;
			}
		}

		async Task displayUserName()
		{
			var usersTable = await tableManager.GetUserAsync();
			var user = usersTable.Where(u => String.Equals(sid, u.UserId)).ElementAt(0);
			Name.Text = String.Format("{0} {1}", user.FirstName, user.LastName);
		}

		async Task displayLendBorrowCount()
		{
			var requestTable = await tableManager.GetRequestAsync();
			var itemsTable = await tableManager.GetBoardgamesAsync();
			int lendCount = requestTable.Count(req => String.Equals(sid, req.Lender) && String.Equals(req.Accepted, "Returned"));
			int borrowCount = requestTable.Count(user => String.Equals(sid, user.Borrower));
			LendBorrow.Text = String.Format("L:{0} | B:{1}", lendCount, borrowCount);
		}

		async Task displayProfilePicture()
		{
			var imageBytes = await ImageManager.GetProfilePicture(sid);

			if (imageBytes == null)
			{
				ProfilePicture.Source = "minrva_icon.png";
			}
			else
			{
				ProfilePicture.Source = ImageSource.FromStream(() =>
											new MemoryStream(imageBytes));
			}
		}

		async Task displayGemAndRank()
		{
			var ratingsTable = await tableManager.GetRatingsAsync();
			var itemsTable = await tableManager.GetBoardgamesAsync();
			int lendCount = itemsTable.Where(item => String.Equals(sid, item.Owner)).Count();
			var ratings = ratingsTable.Where(r => String.Equals(sid, r.RatedID)).Select(rating => rating.Rating);

			double avgRatings;
			string rank;

			if (ratings.Count() > 0)
			{
				avgRatings = ratings.Average();
			}
			else
			{
				avgRatings = 0;
			}


			if (lendCount > 0 && lendCount < 3)
			{
				rank = "Amber";
				Gem.Source = "Gem3.png";
			}
			else if (lendCount >= 3 && lendCount < 10 && avgRatings >= 3)
			{
				rank = "Emerald";
				Gem.Source = "Gem5.png";
			}
			else if (lendCount >= 10 && lendCount < 25 && avgRatings >= 4)
			{
				rank = "Amethyst";
				Gem.Source = "Gem9.png";
			}
			else if (lendCount >= 25 && avgRatings >= 4.5)
			{
				rank = "Diamond";
				Gem.Source = "Gem8.png";
			}
			else
			{
				rank = "Topaz";
				Gem.Source = "Gem6.png";
			}

			Rank.Text = String.Format("Rank:  {0}", rank);

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

		async void ClickedTrustNetwork(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new FirstLayerPage());
		}

		async void Clicked_RankTable(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new RankTablePage());
		}

		public async void ViewReviews(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ReviewsPage(sid, false));
		}

		async void Clicked_Upload(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsPickPhotoSupported)
			{
				await DisplayAlert("Photos Not Supported!", "Permission not granted to photo gallery", "OK");
				return;
			}
			profilePictureFile = await CrossMedia.Current.PickPhotoAsync();

			if (profilePictureFile == null)
				return;

			await ImageManager.UploadProfilePicture(profilePictureFile.GetStream(), sid);
			profilePictureFile.Dispose();
			profilePictureFile = null;

			await displayProfilePicture();
		}

		async void ClickedVoucheeList(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new VoucheesList());
		}

		// Retrieving information about item selected on profile by current user (e.g. if it has been lent out or not)
		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var game = e.SelectedItem as Boardgames;
			var requests = await tableManager.GetRequestAsync();

			string borrowMsg = "";
			if (game.Borrowed)
			{
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
				List<Request> pendingReqs = requests.Where(r => string.Equals(r.ItemId, game.Id) && string.Equals(r.Accepted, "Pending")).ToList();

				if (pendingReqs.Count == 0)
				{
					var alert = await DisplayAlert("Item information", borrowMsg, "Delete", "OK");

					if (alert)
					{
						await tableManager.DeleteBoardgamesAsync(game);
					}
				}
				else
				{
					await DisplayAlert("Item information", borrowMsg, "OK");
				}
			}
			await RefreshItems(false, syncItems: false);

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