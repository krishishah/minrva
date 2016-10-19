﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace minrva.Droid
{
	[Activity (Label = "minrva.Droid",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@android:style/Theme.Holo.Light")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAuthenticate
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Initialize Azure Mobile Apps
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init (this, bundle);

			// Initialize the authenticator before loading the app.
			App.Init((IAuthenticate)this);

			// Load the main application
			LoadApplication (new App ());
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
				user = await BoardgamesManager.DefaultManager.CurrentClient.LoginAsync(this,
					MobileServiceAuthenticationProvider.Facebook);
				if (user != null)
				{
					message = string.Format("you are now signed-in as {0}. and your MobileServiceAuthToken is {1}",
					                        user.UserId, user.MobileServiceAuthenticationToken);
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
	}
}




