using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;

namespace minrva
{
	public partial class NotificationsPage : ContentPage
	{
		TableManager tableManager;
		string seenUnseenCol = "#E0E0E0";

		public NotificationsPage()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			RefreshItems(false, syncItems: false);
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await RefreshItems(false, syncItems: false);
		}

		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var reqMsg = e.SelectedItem as RequestMessage;
			seenUnseenCol = "White";

			if (!String.Equals("Vouch", reqMsg.RequestType))
			{

				var alert = false;
				var alert2 = false;
				var alert3 = false;
				Request req = reqMsg.Request;
				Boardgames requestedItem = reqMsg.RequestedItem;

				if (reqMsg.AcceptStatus.Equals("Pending") && reqMsg.RequestType.Equals("Lend Request"))
				{
					alert = await DisplayAlert("Borrowing request", reqMsg.OtherUser.FirstName + " " + reqMsg.OtherUser.LastName + " has requested to borrow " + requestedItem.Name + " from " + req.StartDate + " to " + req.EndDate, "View Profile", "Cancel");
				}
				else if (reqMsg.AcceptStatus.Equals("Returned") && reqMsg.RequestType.Equals("Borrow Request"))
				{
					await Navigation.PushModalAsync(new LeaveReviewPage(reqMsg.OtherUser, requestedItem, true));
					requestedItem.Borrowed = false;
					await tableManager.SaveBoardgamesAsync(requestedItem);
					await tableManager.DeleteRequestAsync(req);
					await RefreshItems(false, syncItems: false);
				}
				else if (reqMsg.AcceptStatus.Equals("Accepted") && reqMsg.RequestType.Equals("Lend Request"))
				{
					alert2 = await DisplayAlert("Undo", "Undo accepted lend request?", "Yes", "No");
				}
				else if (reqMsg.AcceptStatus.Equals("Pending") && reqMsg.RequestType.Equals("Borrow Request"))
				{
					alert3 = await DisplayAlert("Undo", "Undo pending borrow request?", "Yes", "No");
				}

				if (alert)
				{
					await Navigation.PushModalAsync(new ProfileViewPage(reqMsg.OtherUser, requestedItem, reqMsg.Request, false));
					await RefreshItems(false, syncItems: false);
				}

				if (alert2)
				{
					await tableManager.DeleteRequestAsync(req);
					await RefreshItems(false, syncItems: false);
				}

				if (alert3)
				{
					await tableManager.DeleteRequestAsync(req);
					await RefreshItems(false, syncItems: false);
				}
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
			using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
			{
				string sid = await App.Authenticator.GetUserId();
				var reqs = await tableManager.GetRequestAsync(syncItems);
				var games = await tableManager.GetBoardgamesAsync(syncItems);
				var users = await tableManager.GetUserAsync(syncItems);
				var vouches = await tableManager.GetVouchAsync(syncItems);
				var myVouches = vouches.Where(v => (String.Equals(sid, v.Vouchee)));
				var lenderPendingRequests = reqs.Where(r => (String.Equals(r.Lender, sid)) && (r.Accepted.Equals("Pending")));
				var lenderAcceptedRequests = reqs.Where(r => (String.Equals(r.Lender, sid)) && (r.Accepted.Equals("True"))); 
				var borrowPendingRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (r.Accepted.Equals("Pending")));
				var borrowRejectedRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (r.Accepted.Equals("False")));
				var borrowAcceptedRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (r.Accepted.Equals("True")));
				var borrowReturnedRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (r.Accepted.Equals("Returned")));

				List<RequestMessage> requestsMsgs = new List<RequestMessage>();

				string requestType = "Lend Request";
				string requestStatus = "Pending";
				string col = "#00cc00";
				seenUnseenCol = "#E0E0E0";

				foreach (Request r in lenderPendingRequests)
				{
					User borrowingUser = users.Where(user => String.Equals(r.Borrower, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", borrowingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, borrowingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, r));
				}

				requestStatus = "Accepted";
				seenUnseenCol = "White";

				foreach (Request r in lenderAcceptedRequests)
				{
					User borrowingUser = users.Where(user => String.Equals(r.Borrower, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					Debug.WriteLine("Date accepted: {0}", r.UpdatedAt);
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", borrowingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, borrowingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, r));
				}

				requestType = "Borrow Request";
				col = "#b35050";
				seenUnseenCol = "White";

				foreach (Request r in borrowAcceptedRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					Debug.WriteLine("Date accepted: {0}", r.UpdatedAt);
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", lendingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, lendingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, r));
				}

				requestStatus = "Pending";
				seenUnseenCol = "White";

				foreach (Request r in borrowPendingRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					Debug.WriteLine("Date pending: {0}", r.UpdatedAt);
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", lendingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, lendingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, r));
				}

				requestStatus = "Rejected";
				seenUnseenCol = "White";

				foreach (Request r in borrowRejectedRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					Debug.WriteLine("Date: {0}", r.UpdatedAt);
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", lendingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, lendingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, r));
				}

				requestStatus = "Returned";
				seenUnseenCol = "#E0E0E0";

				foreach (Request r in borrowReturnedRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", lendingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, lendingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, r));
				}

				col = "#6666ff";
				seenUnseenCol = "White";
					
				foreach (Vouch v in myVouches)
				{
					User voucher = users.Where(user => String.Equals(user.UserId, v.Voucher)).ElementAt(0);
					string notifView = String.Format("Vouch Alert: {0} {1}", voucher.FirstName, voucher.LastName);
					string notifViewDetail = String.Format("{0} has vouched for you", voucher.FirstName);
					requestsMsgs.Add(new RequestMessage(null, voucher, "Vouch", "Vouch Alert", v.UpdatedAt, notifView, notifViewDetail, col, seenUnseenCol, null));
				}


				requestsMsgs.Sort((y, x) => x.UpdatedAt.CompareTo(y.UpdatedAt));

				notifsList.ItemsSource = requestsMsgs;
			}
		}

		private class ActivityIndicatorScope : IDisposable
		{
			private bool showIndicator;
			private ActivityIndicator indicator;
			private Task indicatorDelay;

			public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
			{
				this.indicator = indicator;
				this.showIndicator = showIndicator;

				if (showIndicator)
				{
					indicatorDelay = Task.Delay(2000);
					SetIndicatorActivity(true);
				}
				else
				{
					indicatorDelay = Task.FromResult(0);
				}
			}

			private void SetIndicatorActivity(bool isActive)
			{
				this.indicator.IsVisible = isActive;
				this.indicator.IsRunning = isActive;
			}

			public void Dispose()
			{
				if (showIndicator)
				{
					indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
				}
			}
		}
	}
}
