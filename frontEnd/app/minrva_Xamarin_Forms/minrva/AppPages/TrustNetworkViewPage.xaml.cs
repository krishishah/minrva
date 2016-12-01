using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System.IO;

namespace minrva
{
	public partial class TrustNetworkViewPage : ContentPage
	{

		TableManager tableManager;
		User profOwner;

		public TrustNetworkViewPage(User profOwner)
		{
			InitializeComponent();
			tableManager = TableManager.DefaultManager;
			this.profOwner = profOwner;
			displayTrustNetwork();

		}

		private async void displayTrustNetwork()
		{
			trustNetworkList.ItemsSource = await createUserFeedView(await createTrustNetwork());
		}

		//public async void OnSelected(object sender, SelectedItemChangedEventArgs e)
		//{
		//	await Navigation.PushModalAsync(new ProfileViewPage(owner, item, new Request(), true));
		//}

		public async Task<List<User>> createTrustNetwork()
		{
			string sid = await App.Authenticator.GetUserId();
			var vouchTable = await tableManager.GetVouchAsync();
			var userTable = await tableManager.GetUserAsync();

			var currentUserVouchList = vouchTable.Where(owner => String.Equals(sid, owner.Voucher));

			List<User> vouchNetwork = new List<User>();

			foreach (Vouch v in currentUserVouchList)
			{
				var nestedVouchList = vouchTable.Where(vouch => String.Equals(v.Vouchee, vouch.Voucher));

				if (String.Equals(v.Vouchee, profOwner.UserId))
				{
					User voucher = userTable.Where(u => String.Equals(u.UserId, v.Voucher)).ElementAt(0);
					vouchNetwork.Add(voucher);
				}

				else
				{
					foreach (Vouch z in nestedVouchList)
					{
						if (String.Equals(z.Vouchee, profOwner.UserId))
						{
							User voucher = userTable.Where(u => String.Equals(u.UserId, z.Voucher)).ElementAt(0);
							vouchNetwork.Add(voucher);
						}
					}
				}
			}
			return vouchNetwork;
		}

		private async Task<List<UserFeedViewModel>> createUserFeedView(IEnumerable<User> list)
		{

			List<UserFeedViewModel> feedViewList = new List<UserFeedViewModel>();

			foreach (User x in list)
			{
				UserFeedViewModel listElement = new UserFeedViewModel();

				listElement.Id = x.Id;
				listElement.Name = String.Format("{0} {1}", x.FirstName, x.LastName);

				byte[] itemImageBytes = await ImageManager.GetImage(String.Format("{0}", x.Id));
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
			await Navigation.PushModalAsync(new ProfileViewPage(owner, null, null, true));
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
