using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

namespace minrva
{
	public partial class Profile : ContentPage
	{

		TableManager tableManager;
		string owner;
		string sid;

		public Profile()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			displayUserName();
			displayLendBorrowCount();
		}

		async Task displayUserName()
		{
			var usersTable = await tableManager.GetUserAsync();
			string sid = await App.Authenticator.GetUserId();
			var user = usersTable.Where(u => String.Equals(sid, u.UserId)).ElementAt(0);
			Name.Text = String.Format("{0} {1}", user.FirstName, user.LastName);
		}

		async Task displayLendBorrowCount()
		{
			string sid = await App.Authenticator.GetUserId();
			var requestTable = await tableManager.GetRequestAsync();
			int lendCount = requestTable.Where(user => String.Equals(sid, user.Lender)).Count();
			int borrowCount = requestTable.Where(user => String.Equals(sid, user.Borrower)).Count();
			LendBorrow.Text = String.Format("L:{0} | B:{1}", lendCount, borrowCount);
		}


		async Task AddItem(Request item)
		{
			await tableManager.SaveRequestAsync(item);
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
