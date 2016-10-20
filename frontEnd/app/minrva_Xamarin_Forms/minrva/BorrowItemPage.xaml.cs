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
		public BorrowItemPage(Boardgames game)
		{
			InitializeComponent();

			tableManager = TableManager.DefaultManager;
			gameName.Text = game.Name;
			gameDescription.Text = game.Description;
			gameLendDuration.Text = "Available for " + game.Lend_duration + " days.";

			this.owner = game.Owner;
			this.id = game.Id;
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
				var request = new Request { Borrower = borrower, Lender = owner, ItemId = id, StartDate = startDateString, EndDate = endDateString, Accepted = false };
				await AddItem(request);
				string msg = "Request sent to " + lender.FirstName + " " + lender.LastName + " to borrow " + gameName.Text + " from " + startDateString + " to "
																			+ endDateString;
				await DisplayAlert("Success", msg, "Okay");
				await Navigation.PopModalAsync(false);
			}
		}
	}
}
