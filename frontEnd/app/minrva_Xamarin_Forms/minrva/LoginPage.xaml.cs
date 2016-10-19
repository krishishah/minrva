using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace minrva
{
	public partial class LoginPage : ContentPage
	{
		bool authenticated = false;

		public LoginPage()
		{
			InitializeComponent();
		}

		async void loginButton_Clicked(object sender, EventArgs e)
		{
			if (App.Authenticator != null)
				authenticated = await App.Authenticator.Authenticate();

			// Set syncItems to true to synchronize the data on startup when offline is enabled.
			if (authenticated == true)
				App.Current.MainPage = new MainTabContainer();
		}
	}
}
