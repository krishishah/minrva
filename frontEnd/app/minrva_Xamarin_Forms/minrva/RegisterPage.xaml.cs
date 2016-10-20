using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class RegisterPage : ContentPage
	{
		UserManager manager;


		public RegisterPage()
		{
			InitializeComponent();
			manager = UserManager.DefaultManager;
		}

		// Data methods
		async Task AddItem(User item)
		{
			await manager.SaveTaskAsync(item);
		}

		async void registerButton_Clicked(object sender, EventArgs e)
		{
			string userId = await App.Authenticator.GetUserId();

			if (firstNameEntry.Text == null || lastNameEntry.Text == null ||
			   emailEntry.Text == null)
			{
				//Throw some error and make user fill in form
			}

			var user = new User {UserId = userId, FirstName = firstNameEntry.Text, 
				            LastName = lastNameEntry.Text, Email = emailEntry.Text };

			await AddItem(user);
			App.Current.MainPage = new MainTabContainer();

		}
	}
}
