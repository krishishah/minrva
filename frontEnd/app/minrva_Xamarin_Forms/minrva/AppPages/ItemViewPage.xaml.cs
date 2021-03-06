﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class ItemViewPage : ContentPage
	{

		Boardgames item;
		User owner;
		TableManager manager;

		public ItemViewPage(Boardgames item, User owner)
		{
			InitializeComponent();
			manager = TableManager.DefaultManager;
			this.item = item;
			this.owner = owner;
			Owner.Text = owner.FirstName + ' ' + owner.LastName;
			ProductName.Text = item.Name;
			Description.Text = item.Description;
			Category.Text = item.Category;
			Location.Text = item.Location;
			Duration.Text = String.Format("Available for {0} days", item.Lend_duration);
			displayItemImage();
			displayRatings();
		}

		async void displayItemImage()
		{
			var imageBytes = await ImageManager.GetImage(item.Id + "_0");

			if (imageBytes != null)
			{
				ItemImage.Source = ImageSource.FromStream(() =>
											new MemoryStream(imageBytes));
				ItemImage.HeightRequest = 200;
			}
		}

		async void displayRatings()
		{
			overallRating.Value = await getOverallRating();
			await RefreshItems(false, syncItems: false);
		}

		// Calculating average rating from all ratings received by item
		async Task<double> getOverallRating()
		{
			var ratingsTable = await manager.GetRatingsAsync();
			var ratings = ratingsTable.Where(r => String.Equals(item.Id, r.RatedID)).Select(rating => rating.Rating);
			if (ratings.Count() > 0)
			{
				return ratings.Average();
			}
			else
			{
				return 0;
			}
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
			var reviews = await manager.GetRatingsAsync();
			var entityReviews = reviews.Where(r => string.Equals(r.RatedID, item.Id));
			var usersTable = await manager.GetUserAsync();

			foreach (Ratings rating in entityReviews)
			{
				User reviewingUser = usersTable.Where(u => string.Equals(rating.ReviewerID, u.UserId)).ElementAt(0);
				rating.Reviewer = String.Format("{0} {1}", reviewingUser.FirstName, reviewingUser.LastName);
			}

			var size = entityReviews.Count();
			allReviews.ItemsSource = entityReviews;
			allReviews.HeightRequest = size * 140;
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		public async void ViewProfile(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new ProfileViewPage(owner, item, new Request(), true));
			await RefreshItems(false, syncItems: false);
		}

		public async void BorrowItem(object sender, EventArgs e)
		{
			await Navigation.PushModalAsync(new BorrowItemPage(item));
		}
	}
}
