using System;
using System.Collections.Generic;
using Syncfusion.SfRating.XForms;
using Xamarin.Forms;

namespace minrva
{
	public partial class LeaveReviewPage : ContentPage
	{
		User otherUser;
		Boardgames item;
		bool borrowed;
		TableManager manager;

		public LeaveReviewPage(User user, Boardgames product, bool borrowed)
		{
			InitializeComponent();
			otherUser = user;
			item = product;
			this.borrowed = borrowed;
			manager = TableManager.DefaultManager;
			if (borrowed)
			{
				userExperienceTitle.Text = String.Format("How was your experience borrowing from {0} {1}?", otherUser.FirstName, otherUser.LastName);
				userFeedbackDescription.Text = String.Format("Leave some feedback about how reliable {0} was, when you borrowed {1} from them", otherUser.FirstName, item.Name);
				itemExperienceTitle.Text = String.Format("How would you rate the condition of {0} when you borrowed it?", item.Name);
				itemFeedbackDescription.Text = String.Format("Leave some feedback about the product, {0}, that you borrowed", item.Name);
			}
			else
			{
				userExperienceTitle.Text = String.Format("How was your experience lending to {0} {1}?", otherUser.FirstName, otherUser.LastName);
				userFeedbackDescription.Text = String.Format("Leave some feedback about how well your item, {0} was looked after by {1} for other users to see!", item.Name, otherUser.FirstName);
				itemExperienceTitle.IsVisible = false;
				itemFeedbackDescription.IsVisible = false;
				itemRating.IsVisible = false;
				itemReview.IsVisible = false;
			}

		}

		void Handle_Focused_User(object sender, FocusEventArgs e)
		{
			if (Equals(userReview.Text, "Leave a review for this user"))
			{
				userReview.Text = string.Empty;
				userReview.TextColor = Color.Black;
			}
		}

		void Handle_Focused_Item(object sender, FocusEventArgs e)
		{
			if (Equals(itemReview.Text, "Leave a review for this item"))
			{
				itemReview.Text = string.Empty;
				itemReview.TextColor = Color.Black;
			}
		}

		public async void OnSend(object sender, EventArgs e)
		{
			if (borrowed)
			{
				if (userRating.Value == 0 || itemRating.Value == 0)
				{
					await DisplayAlert("Error", "Both ratings must be given", "Ok");
					return;
				}
				else
				{
					String sid = await App.Authenticator.GetUserId();
					Ratings usersRating = new Ratings { IsItem = false, Rating = userRating.Value, Review = userReview.Text, RatedID = otherUser.UserId, ReviewerID = sid };
					await manager.SaveRatingsAsync(usersRating);
					Ratings itemsRating = new Ratings { IsItem = true, Rating = itemRating.Value, Review = itemReview.Text, RatedID = item.Id, ReviewerID = sid };
					await manager.SaveRatingsAsync(itemsRating);
					await DisplayAlert("Success", "Your rating has been recorded!", "Ok");
					await Navigation.PopModalAsync();
				}
			}
			else
			{
				if (userRating.Value == 0)
				{
					await DisplayAlert("Error", "Rating must be given", "Ok");
					return;
				}
				else
				{
					String sid = await App.Authenticator.GetUserId();
					Ratings usersRating = new Ratings { IsItem = false, Rating = userRating.Value, Review = userReview.Text, RatedID = otherUser.UserId, ReviewerID = sid };
					await manager.SaveRatingsAsync(usersRating);
					await DisplayAlert("Success", "Your rating has been recorded!", "Ok");
					await Navigation.PopModalAsync();
				}
			}
		}
	}
}
