using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.Diagnostics;
using Syncfusion.SfRating.XForms;
using System.IO;

namespace minrva
{
	public partial class ProfileViewPage : ContentPage
	{

		TableManager tableManager;
		User profOwner;
		Boardgames reqItem;
		Request req;
		bool borrowing;

		public ProfileViewPage(User profOwner, Boardgames reqItem, Request req, bool borrowing)
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			this.profOwner = profOwner;
			this.reqItem = reqItem;
			this.borrowing = borrowing;
			if (!borrowing)
			{
				this.req = req;
			}
			displayDetails();
			displayProfilePicture();
		}

		private async void displayDetails()
		{
			await displayLendBorrowCount();
			Name.Text = String.Format("{0} {1}", profOwner.FirstName, profOwner.LastName);
			if (!this.borrowing)
			{
				BorrowReq.Text = String.Format("{0} {1} has requested to borrow {2} from {3} to {4}", profOwner.FirstName, profOwner.LastName, reqItem.Name, req.StartDate, req.EndDate);
			}
			else 
			{
				BorrowReq.IsVisible = false;
				YesButton.IsVisible = false;
				NoButton.IsVisible = false;
			}
			userRating.Value = await getUserRating();
			await RefreshItems(false, syncItems: false);
		}

		async Task<double> getUserRating()
		{
			var ratingsTable = await tableManager.GetRatingsAsync();
			var ratings = ratingsTable.Where(r => String.Equals(profOwner.UserId, r.RatedID)).Select(rating => rating.Rating);
			if (ratings.Count() > 0)
			{
				return ratings.Average();
			}
			else
			{
				return 0;
			}
		}

		public async void ViewReviews(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ReviewsPage(profOwner.UserId, false));
		}

		private async void displayProfilePicture()
		{
			var imageBytes = await ImageManager.GetProfilePicture(profOwner.UserId);

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

		async Task displayLendBorrowCount()
		{
			string sid = profOwner.UserId;
			var requestTable = await tableManager.GetRequestAsync();
			var itemsTable = await tableManager.GetBoardgamesAsync();
			int lendCount = itemsTable.Where(item => String.Equals(sid, item.Owner)).Count();
			int borrowCount = requestTable.Where(user => String.Equals(sid, user.Borrower)).Count();
			LendBorrow.Text = String.Format("L:{0} | B:{1}", lendCount, borrowCount);
		}

		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var game = e.SelectedItem as Boardgames;
			var userTable = await tableManager.GetUserAsync();
			await Navigation.PushModalAsync(new ItemViewPage(game, userTable.Where(u => string.Equals(u.UserId, game.Owner)).ElementAt(0)));
			await RefreshItems(false, syncItems: false);
		}

		async Task AddItem(Chat item)
		{
			await tableManager.SaveChatAsync(item);
		}

		public async void AcceptRequest(object sender, EventArgs e)
		{
			string sid = await App.Authenticator.GetUserId();
			req.Accepted = "True";
			await tableManager.SaveRequestAsync(req);
			reqItem.Borrowed = true;
			await tableManager.SaveBoardgamesAsync(reqItem);
			var chat = new Chat { Lender = sid, Borrower = profOwner.UserId };
			await AddItem(chat);
			await Navigation.PopModalAsync();
		}

		public async void RejectRequest(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
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
			var items = await tableManager.GetBoardgamesAsync(syncItems);
			usersItems.ItemsSource = items.Where(game => (String.Equals(game.Owner, profOwner.UserId) && !game.Borrowed));
		}

		protected override void OnAppearing()
		{
			displayDetails();
		}
	}

}
