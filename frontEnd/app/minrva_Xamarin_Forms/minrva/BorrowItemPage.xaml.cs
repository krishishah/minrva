using System;
using System.Diagnostics;
using System.Collections.Generic;

using Xamarin.Forms;

namespace minrva
{
	public partial class BorrowItemPage : ContentPage
	{

		public BorrowItemPage(Boardgames game)
		{
			InitializeComponent();

			gameName.Text = game.Name;
			gameDescription.Text = game.Description;
			gameLendDuration.Text = "Available for " + game.Lend_duration + " days.";
		}

		public async void OnBorrow(object sender, EventArgs e)
		{
			string startDateString = String.Format("{0:dd/MM/yyyy}", startDate.Date);
			string endDateString = String.Format("{0:dd/MM/yyyy}", endDate.Date);
			string msg = "Request sent to lender, to borrow " + gameName.Text + " from " + startDateString + " to "
																		+ endDateString;                                                         
			await DisplayAlert("Success", msg, "Cancel");
			await Navigation.PopModalAsync(false);	
		}
	}
}
