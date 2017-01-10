using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;

namespace minrva
{
	public partial class ItemsLentBorrowed : ContentPage
	{
		TableManager tableManager;
		public String Username { get; set; }
		public User Receiver;


		public ItemsLentBorrowed(User name)
		{
			Username = name.FirstName;
			Receiver = name;

			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			RefreshItems(true, syncItems: false);
		}

		async void ClickedBack(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
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
			//using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
			//{
				string sid = await App.Authenticator.GetUserId();
				var reqs = await tableManager.GetRequestAsync(syncItems);
				var games = await tableManager.GetBoardgamesAsync(syncItems);
				var users = await tableManager.GetUserAsync(syncItems);
				var lenderAcceptedRequests = reqs.Where(r => (String.Equals(r.Lender, sid)) && (r.Accepted.Equals("True")));
				var borrowAcceptedRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (r.Accepted.Equals("True")));

				List<RequestMessage> borrows = new List<RequestMessage>();
				List<RequestMessage> lends = new List<RequestMessage>();

				string requestType = "Lend Request";
				string requestStatus = "Accepted";
				string col = "#00cc00";
				string seenUnseenCol = "#E0E0E0";

				foreach (Request r in lenderAcceptedRequests)
				{
					User borrowingUser = users.Where(user => String.Equals(r.Borrower, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					string notifView = String.Format("{0}", requestedItem.Name);
					string notifViewDetail = String.Format(" ");

					if (borrowingUser.UserId == Receiver.UserId)
					{
						lends.Add(new RequestMessage(requestedItem, borrowingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, r));
					}
				}

				requestType = "Borrow Request";
				requestStatus = "Accepted";

				foreach (Request r in borrowAcceptedRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					string notifView = String.Format("{0}",requestedItem.Name);
					string notifViewDetail = String.Format(" ");

					if (lendingUser.UserId == Receiver.UserId)
					{
						borrows.Add(new RequestMessage(requestedItem, lendingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, r));
					}
				}

				borrowList.ItemsSource = borrows;
				lendList.ItemsSource = lends;
			}
		//}
	}
}
