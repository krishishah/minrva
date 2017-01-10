using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace minrva
{
	public partial class FirstLayerPage : ContentPage
	{
		TableManager tableManager;

		public FirstLayerPage()
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			title.Text = "Trust Network First Layer: \n Your Vouchees";
			displayTrustNetwork();

		}

		private async void displayTrustNetwork()
		{
			vouchees.ItemsSource = await createUserListView(await createTrustNetwork());
		}


		public async Task<List<User>> createTrustNetwork()
		{
			string sid = await App.Authenticator.GetUserId();
			var vouchTable = await tableManager.GetVouchAsync();
			var userTable = await tableManager.GetUserAsync();

			var currentUserVouchList = vouchTable.Where(owner => String.Equals(sid, owner.Voucher));

			List<User> vouchList = new List<User>();

			foreach (Vouch v in currentUserVouchList)
			{
				User vouchee = userTable.Where(u => String.Equals(u.UserId, v.Vouchee)).ElementAt(0);
				vouchList.Add(vouchee);
			}

			return vouchList;
		}

		private async Task<List<UserFeedViewModel>> createUserListView(IEnumerable<User> list)
		{

			List<UserFeedViewModel> feedViewList = new List<UserFeedViewModel>();

			foreach (User x in list)
			{
				UserFeedViewModel listElement = new UserFeedViewModel();

				listElement.Id = x.Id;
				listElement.Name = String.Format("{0} {1}", x.FirstName, x.LastName);

				byte[] itemImageBytes = await ImageManager.GetProfilePicture(x.UserId);
				listElement.ImageSource = "minrva_icon.png";

				if (itemImageBytes != null)
					listElement.ImageSource = ImageSource.FromStream(() => new MemoryStream(itemImageBytes));

				feedViewList.Add(listElement);

			}

			return feedViewList;

		}

		private async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var item = e.SelectedItem as UserFeedViewModel;
			var userTable = await tableManager.GetUserAsync();
			User owner = userTable.Where(x => String.Equals(item.Id, x.Id)).ElementAt(0);
			await Navigation.PushModalAsync(new SecondLayerPage(owner));
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
