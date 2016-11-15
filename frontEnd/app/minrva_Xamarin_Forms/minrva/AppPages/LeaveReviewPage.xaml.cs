using System;
using System.Collections.Generic;
using Syncfusion.SfRating.XForms;
using Xamarin.Forms;

namespace minrva
{
	public partial class LeaveReviewPage : ContentPage
	{
		User borrower;
		Boardgames item;

		public LeaveReviewPage()
		{
			InitializeComponent();
		}

		public LeaveReviewPage(User borrower, Boardgames item)
		{
			this.borrower = borrower;
			this.item = item;
			experienceTitle.Text = String.Format("How was your experience lending to {0} {1}?", borrower.FirstName, borrower.LastName);
			feedbackDescription.Text = String.Format("Leave some feedback about how well your item, {0} was looked after by {1} for other users to see!", item.Name, borrower.FirstName);
			rating = new SfRating();
		}
	}
}
