using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace minrva
{
	public partial class Profile : ContentPage
	{
		public Profile()
		{
			InitializeComponent();
		}

		async void Clicked_Logout(object sender, EventArgs e)
		{
			bool loggedOut = false;

			if (App.Authenticator != null)
				loggedOut = await App.Authenticator.LogoutAsync();

			if (loggedOut)
				App.Current.MainPage = new LoginPage();
			else
				await DisplayAlert("Logout Error", "You have failed to log out.", "OK");
		}
	}

}
