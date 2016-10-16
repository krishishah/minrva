using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace minrva
{
	public partial class BrowsePage : ContentPage
	{
		Label resultsLabel;
		SearchBar searchBar;
		public BrowsePage()
		{
			resultsLabel = new Label
			{
				Text = "Result will appear here.",
				VerticalOptions = LayoutOptions.FillAndExpand,
				FontSize = 25
			};

			searchBar = new SearchBar
			{
				Placeholder = "Browse items, users, categories",
				SearchCommand = new Command(() => { 
					resultsLabel.Text = "You searched for " + searchBar.Text; 
				})
			};

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Start,
				Children = {
					searchBar,
					new ScrollView
					{
						Content = resultsLabel,
						VerticalOptions = LayoutOptions.FillAndExpand
					}
				},
				Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5)
			};
		}
	}
}
