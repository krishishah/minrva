using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace minrva
{
	public partial class RankTablePage : ContentPage
	{
		public RankTablePage()
		{
			InitializeComponent();
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		//public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		//{

		//}

		//public async void OnRefresh(object sender, EventArgs e)
		//{

		//}
	}
}
