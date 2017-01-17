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

		// View of another user's profile, i.e. only shows items that they are lending out that are still available
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
			displayGemAndRank();
			VouchMessage.GestureRecognizers.Add(new TapGestureRecognizer((view) => SeeVouchers()));
		}

		private async void displayDetails()
		{

			displayVouchDetails();
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

		async Task displayGemAndRank()
		{
			var ratingsTable = await tableManager.GetRatingsAsync();
			var itemsTable = await tableManager.GetBoardgamesAsync();
			int lendCount = itemsTable.Where(item => String.Equals(profOwner.UserId, item.Owner)).Count();
			var ratings = ratingsTable.Where(r => String.Equals(profOwner.UserId, r.RatedID)).Select(rating => rating.Rating);

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

		async void Clicked_RankTable(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new RankTablePage());
		}

		async Task displayLendBorrowCount()
		{
			string sid = profOwner.UserId;
			var requestTable = await tableManager.GetRequestAsync();
			var itemsTable = await tableManager.GetBoardgamesAsync();
			int lendCount = requestTable.Where(user => String.Equals(sid, user.Lender) && String.Equals(user.Accepted,"Returned")).Count();
			int borrowCount = requestTable.Where(user => String.Equals(sid, user.Borrower)).Count();
			LendBorrow.Text = String.Format("L:{0} | B:{1}", lendCount, borrowCount);
		}


		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as Boardgames;
			var userTable = await tableManager.GetUserAsync();
			await Navigation.PushModalAsync(new ItemViewPage(item, userTable.Where(u => string.Equals(u.UserId, item.Owner)).ElementAt(0)));
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
			req.Accepted = "False";
			await tableManager.SaveRequestAsync(req);
			await Navigation.PopModalAsync();
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
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
			displayVouchDetails();

			var items = await tableManager.GetBoardgamesAsync(syncItems);
			usersItems.ItemsSource = items.Where(game => (String.Equals(game.Owner, profOwner.UserId) && !game.Borrowed));
		}

		protected override void OnAppearing()
		{
			displayDetails();

		}

		public async void ClickVouch(object sender, EventArgs e)
		{
			var vouch = new Vouch();
			vouch.Voucher = await App.Authenticator.GetUserId();
			vouch.Vouchee = profOwner.UserId;

			var vouchTable = await tableManager.GetVouchAsync();
			var userTable = await tableManager.GetUserAsync();

			User owner = userTable.Where(u => String.Equals(u.UserId, profOwner.UserId)).ElementAt(0);


			bool alreadyVouched = vouchTable.Where(entry => String.Equals(vouch.Vouchee, entry.Vouchee)
			                                       && String.Equals(vouch.Voucher, entry.Voucher)).Count() > 0;

			if (!alreadyVouched)
			{
				await tableManager.SaveVouchAsync(vouch);
				vouchButton.Text = "Unvouch";
				await DisplayAlert("Success", String.Format("You have now vouched for {0}!", owner.FirstName), "OK");
			}
			else
			{
				var vouched = vouchTable.Where(entry => String.Equals(vouch.Vouchee, entry.Vouchee)
				                                  && String.Equals(vouch.Voucher, entry.Voucher)).ElementAt(0);
				await tableManager.DeleteVouchAsync(vouched);
				vouchButton.Text = "Vouch";
				await DisplayAlert("Success", String.Format("You have now Unvouched {0}!", owner.FirstName), "OK");
			}
			displayVouchDetails();

		}

		public async Task<List<Vouch>> createTrustNetwork()
		{
			string sid = await App.Authenticator.GetUserId();
			var vouchTable = await tableManager.GetVouchAsync();

			var currentUserVouchList = vouchTable.Where(owner => String.Equals(sid, owner.Voucher));

			List<Vouch> vouchNetwork = new List<Vouch>();

			foreach (Vouch v in currentUserVouchList)
			{
				var nestedVouchList = vouchTable.Where(vouch => String.Equals(v.Vouchee, vouch.Voucher));

				if (String.Equals(v.Vouchee, profOwner.UserId))
				{
					vouchNetwork.Add(v);
				} 

				else
				{
					foreach (Vouch z in nestedVouchList)
					{
						if (String.Equals(z.Vouchee, profOwner.UserId))
						{
							vouchNetwork.Add(z);
						}
					}
				}
			}
			return vouchNetwork;
		}

		private async void displayVouchDetails()
		{
			VouchMessage.Text = await trustNetworkToString(await createTrustNetwork());
			var vouch = new Vouch();
			vouch.Voucher = await App.Authenticator.GetUserId();
			vouch.Vouchee = profOwner.UserId;
			var vouchTable = await tableManager.GetVouchAsync();
			bool alreadyVouched = vouchTable.Where(entry => String.Equals(vouch.Vouchee, entry.Vouchee)
												   && String.Equals(vouch.Voucher, entry.Voucher)).Count() > 0;
			if (alreadyVouched)
			{
				vouchButton.Text = "Unvouch";
			}
			else {
				vouchButton.Text = "Vouch";
			}
		}

		public async Task<string> trustNetworkToString(List<Vouch> trustNetwork)
		{
			var userTable = await tableManager.GetUserAsync();
			string message = "";
			string sid = await App.Authenticator.GetUserId();
			User owner = userTable.Where(u => String.Equals(u.UserId, profOwner.UserId)).ElementAt(0);
			int trustCounter = 0;

			if (trustNetwork.Count == 0)
			{
				return String.Format("{0} is not in your Trust Network", owner.FirstName);
			}

			foreach (Vouch v in trustNetwork)
			{
				if (String.Equals(v.Voucher, sid))
				{
					message = "You" + message;
				}
				else
				{
					if (trustNetwork.IndexOf(v) < 1)
					{
						User user = userTable.Where(u => String.Equals(u.UserId, v.Voucher)).ElementAt(0);
						message += String.Format(", {0}", user.FirstName);
					}
					else
					{
						trustCounter++;
					}
				}
			}

			Debug.WriteLine(message);

			if (message.StartsWith(", "))
				message = message.Remove(0, 2);

			if (trustCounter == 1)
				message += String.Format(" and {0} other person in your network", trustCounter);
			else if (trustCounter > 1)
				message += String.Format(" and {0} others in your network", trustCounter);
			else if (!message.StartsWith("You"))
				return message += String.Format(" has vouched for {0}", owner.FirstName);
			else if (message.StartsWith("You, "))
				message = "You and" + message.Remove(0, 4);
			
			return message += String.Format(" have vouched for {0}", owner.FirstName);

		}

		public async void SeeVouchers()
		{
			await Navigation.PushModalAsync(new VouchersViewPage(profOwner));
			
		}


	}

}
