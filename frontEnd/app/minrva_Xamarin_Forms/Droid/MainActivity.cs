using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Android.Webkit;

namespace minrva.Droid
{
	[Activity(Label = "minrva.Droid",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Material.Light")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Initialize Azure Mobile Apps
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init(this, bundle);

			Xamarin.FormsMaps.Init(this, bundle);

			// Initialize the authenticator before loading the app.
			App.Init((IAuthenticate)this);

			// Load the main application
			LoadApplication(new App());
		}

		// Define a authenticated user.
		private MobileServiceUser user;

		//private static async Task<SocialLoginResult> GetUserData()
		//{
		//	return await Client.InvokeApiAsync<SocialLoginResult>("getextrauserinfo", HttpMethod.Get, null);
		//}

		public async Task<bool> Authenticate()
		{
			var success = false;
			var message = string.Empty;
			try
			{
				// Sign in with Facebook login using a server-managed flow.
				user = await TableManager.DefaultManager.CurrentClient.LoginAsync(this,
					MobileServiceAuthenticationProvider.Facebook);
				if (user != null)
				{
					message = string.Format("you are now signed-in as {0}",
											user.UserId);
					success = true;
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}


			// Display the success or failure message.
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetMessage(message);
			builder.SetTitle("Sign-in result");
			builder.Create().Show();

			return success;
		}

		public async Task<string> GetUserId()
		{
			while (user.UserId == null)
			{
			}

			return user.UserId;
		}

		public async Task<bool> LogoutAsync()
		{
			bool success = false;

			try
			{
				if (user != null)
				{
					CookieManager.Instance.RemoveAllCookie();
					await TableManager.DefaultManager.CurrentClient.LogoutAsync();

					var message = string.Format("You are now logged out - {0}", user.UserId);

					AlertDialog.Builder builder = new AlertDialog.Builder(this);
					builder.SetMessage(message);
					builder.SetTitle("Sign-in result");
					builder.Create().Show();
				}
				user = null;
				success = true;
			}
			catch (Exception ex)
			{
				AlertDialog.Builder builder = new AlertDialog.Builder(this);
				builder.SetMessage("You have failed to log out.");
				builder.SetTitle("Logout failed");
				builder.Create().Show();
			}

			return success;
		}
	}
}



