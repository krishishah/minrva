using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class VoucheesList : ContentPage
	{

		TableManager tableManager;

		public VoucheesList()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			RefreshItems(true, syncItems: false);
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

		protected override void OnAppearing()
		{
			DisplayList();

		}

		private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
		{
			DisplayList();
			//await DisplayAlert("Size", vouchNetwork.Count().ToString(), "OK");
		}

		private async void DisplayList()
		{
			var sid = await App.Authenticator.GetUserId();
			var vouchTable = await tableManager.GetVouchAsync();
			var userTable = await tableManager.GetUserAsync();
			var currentUserVouchList = vouchTable.Where(owner => String.Equals(sid, owner.Vouchee));

			List<User> vouchNetwork = new List<User>();

			foreach (Vouch v in currentUserVouchList)
			{
				User vouchee = userTable.Where(u => String.Equals(u.UserId, v.Voucher)).ElementAt(0);
				v.Vouchee = String.Format("{0} {1}", vouchee.FirstName, vouchee.LastName);
				vouchNetwork.Add(vouchee);
			}

			voucheeList.ItemsSource = vouchNetwork;
		}

		async void ClickedBack(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
