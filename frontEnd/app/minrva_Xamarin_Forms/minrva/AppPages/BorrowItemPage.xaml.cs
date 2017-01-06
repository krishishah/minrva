using System;
using System.Diagnostics;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace minrva
{
	public partial class BorrowItemPage : ContentPage
	{

		TableManager tableManager;
		string owner;
		string id;
		int lend_duration;

		public BorrowItemPage(Boardgames game)
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			gameName.Text = game.Name;
			gameDescription.Text = game.Description;
			lend_duration = game.Lend_duration;
			gameLendDuration.Text = "Available for " + lend_duration + " days.";
			startDate.MinimumDate = DateTime.Today.AddDays(1);
			endDate.Date = startDate.Date.AddDays(lend_duration);
			endDate.MinimumDate = startDate.Date.AddDays(1);
			endDate.MaximumDate = endDate.Date;

			this.owner = game.Owner;
			this.id = game.Id;
		}

		async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		async void startDateChanged(object sender, EventArgs e)
		{
			endDate.MaximumDate = startDate.Date.AddDays(lend_duration);
			endDate.Date = startDate.Date.AddDays(lend_duration);
			endDate.MinimumDate = startDate.Date.AddDays(1);
		}

		async Task AddItem(Request item)
		{
			await tableManager.SaveRequestAsync(item);
		}
		public async void OnBorrow(object sender, EventArgs e)
		{
			var usersTable = await tableManager.GetUserAsync();
			var lenders = usersTable.Where(a => a.UserId == owner);
			if (lenders.Count() > 0)
			{
				User lender = lenders.ElementAt(0);
				string borrower = await App.Authenticator.GetUserId();
				string startDateString = String.Format("{0:dd/MM/yyyy}", startDate.Date);
				string endDateString = String.Format("{0:dd/MM/yyyy}", endDate.Date);
				var request = new Request { Borrower = borrower, Lender = owner, ItemId = id, StartDate = startDateString, EndDate = endDateString, Accepted = "Pending" };
				await AddItem(request);
				string msg = "Request sent to " + lender.FirstName + " " + lender.LastName + " to borrow " + gameName.Text + " from " + startDateString + " to "
																			+ endDateString;
				await DisplayAlert("Success", msg, "Okay");
				await Navigation.PopModalAsync(false);
			}
		}
	}
}
