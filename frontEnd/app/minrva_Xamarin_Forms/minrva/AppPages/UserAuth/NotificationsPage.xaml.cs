﻿using System.Collections.Generic;
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

		public NotificationsPage()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			RefreshItems(true, syncItems: false);
		}

		public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var reqMsg = e.SelectedItem as RequestMessage;
			var alert = false;
			Request req = reqMsg.Request;
			Boardgames requestedItem = reqMsg.RequestedItem;

			if (reqMsg.AcceptStatus.Equals("Pending") && reqMsg.RequestType.Equals("Lend Request"))
			{
				alert = await DisplayAlert("Borrowing request", "Would you like to lend " + requestedItem.Name + " to " + reqMsg.Borrower.FirstName + " " + reqMsg.Borrower.LastName + " from " + req.StartDate + " to " + req.EndDate, "Yes", "No");
			}
			    
			if (alert)
			{
				req.Accepted = "True";
				await tableManager.SaveRequestAsync(req);
				requestedItem.Borrowed = true;
				await tableManager.SaveBoardgamesAsync(requestedItem);
				await DisplayAlert("Success", "You have confirmed the loan. You can now contact " + reqMsg.Borrower.FirstName + " " + reqMsg.Borrower.LastName + " at " + reqMsg.Borrower.Email + " to confirm when and where to complete the transaction.", "Ok");
				await RefreshItems(true, syncItems: false);
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
				var lenderPendingRequests = reqs.Where(r => (String.Equals(r.Lender, sid)) && (r.Accepted.Equals("Pending")));
				var lenderAcceptedRequests = reqs.Where(r => (String.Equals(r.Lender, sid)) && (r.Accepted.Equals("True"))); 
				var borrowPendingRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (r.Accepted.Equals("Pending")));
				var borrowRejectedRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (r.Accepted.Equals("False")));
				var borrowAcceptedRequests = reqs.Where(r => (String.Equals(r.Borrower, sid)) && (r.Accepted.Equals("True")));

				//List<RequestMessage> lenderPendingMsgs = new List<RequestMessage>();
				//List<RequestMessage> lenderAcceptedMsgs = new List<RequestMessage>();
				//List<RequestMessage> borrowerAcceptedMsgs = new List<RequestMessage>();
				//List<RequestMessage> borrowerPendingMsgs = new List<RequestMessage>();
				//List<RequestMessage> borrowerRejectedMsgs = new List<RequestMessage>();

				List<RequestMessage> requestsMsgs = new List<RequestMessage>();


				foreach (Request r in lenderPendingRequests)
				{
					User borrowingUser = users.Where(user => String.Equals(r.Borrower, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					//lenderPendingMsgs.Add(new RequestMessage(requestedItem, borrowingUser, "Lend Request", "Pending", r.UpdatedAt, r));
					string requestType = "Lend Request";
					string requestStatus = "Pending";
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", borrowingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, borrowingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, r));

				}

				foreach (Request r in lenderAcceptedRequests)
				{
					User borrowingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					//lenderAcceptedMsgs.Add(new RequestMessage(requestedItem, borrowingUser, "Lend Request", "True", r.UpdatedAt, r));
					string requestType = "Lend Request";
					string requestStatus = "Accepted";
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", borrowingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, borrowingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, r));

				}

				foreach (Request r in borrowAcceptedRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					//borrowerAcceptedMsgs.Add(new RequestMessage(requestedItem, lendingUser, "Borrow Request", "True", r.UpdatedAt, r));
					string requestType = "Borrow Request";
					string requestStatus = "Accepted";
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", lendingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, lendingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, r));

				}

				foreach (Request r in borrowPendingRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					//borrowerPendingMsgs.Add(new RequestMessage(requestedItem, lendingUser, "Borrow Request", "Pending", r.UpdatedAt, r));
					string requestType = "Borrow Request";
					string requestStatus = "Pending";
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", lendingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, lendingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, r));

				}

				foreach (Request r in borrowRejectedRequests)
				{
					User lendingUser = users.Where(user => String.Equals(r.Lender, user.UserId)).ElementAt(0);
					Boardgames requestedItem = games.Where(game => String.Equals(r.ItemId, game.Id)).ElementAt(0);
					//borrowerRejectedMsgs.Add(new RequestMessage(requestedItem, lendingUser, "Borrow Request", "False", r.UpdatedAt, r));
					string requestType = "Borrow Request";
					string requestStatus = "Rejected";
					string notifView = String.Format("{0}: {1}", requestType, requestedItem.Name);
					string notifViewDetail = String.Format("{0} - {1}", lendingUser.FirstName, requestStatus);
					requestsMsgs.Add(new RequestMessage(requestedItem, lendingUser, requestType, requestStatus, r.UpdatedAt, notifView, notifViewDetail, r));

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