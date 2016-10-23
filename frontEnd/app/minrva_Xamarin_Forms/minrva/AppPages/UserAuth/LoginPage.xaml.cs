using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class LoginPage : ContentPage
	{
		bool authenticated = false;
		TableManager manager;

		public LoginPage()
		{
			InitializeComponent();
			manager = TableManager.DefaultManager;
		}

		public async Task<bool> hasRegistered()
		{
			var usersTable = await manager.GetUserAsync();
			var userId = await App.Authenticator.GetUserId();
			Debug.WriteLine("UserId:{0}",userId);
			var results = usersTable.Where(a => a.UserId == userId);
			Debug.WriteLine("resultsCount: {0}", results.Count());
			return (results.Count() != 0);
		

		}

		async void loginButton_Clicked(object sender, EventArgs e)
		{
			if (App.Authenticator != null)
				authenticated = await App.Authenticator.Authenticate();
			// Set syncItems to true to synchronize the data on startup when offline is enabled.
			if (authenticated == true)
			{
				var registered = await hasRegistered();
				if (registered)
					App.Current.MainPage = new MainTabContainer();
				else
					App.Current.MainPage = new RegisterPage();
			}
		}
	}
}
