using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Foundation;
using UIKit;

using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Diagnostics;

namespace minrva.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IAuthenticate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// Initialize Azure Mobile Apps
			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

			// Initialize Xamarin Forms
			global::Xamarin.Forms.Forms.Init ();

			App.Init(this);

			LoadApplication (new App ());

			UITabBar.Appearance.SelectedImageTintColor = UIColor.Red;

			return base.FinishedLaunching (app, options);
		}

		// Define a authenticated user.
		private MobileServiceUser user;

		public async Task<bool> Authenticate()
		{
			var success = false;
			var message = string.Empty;
			try
			{
				// Sign in with Facebook login using a server-managed flow.
				if (user == null)
				{
					user = await TableManager.DefaultManager.CurrentClient
						.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController,
						MobileServiceAuthenticationProvider.Facebook);
					if (user != null)
					{
						
						message = string.Format("You are now signed-in as {0}.", user.UserId);
						success = true;
					}
				}
			}
			catch (Exception ex)
			{
				message = ex.Message;
			}

			// Display the success or failure message.
			UIAlertView avAlert = new UIAlertView("Sign-in result", message, null, "OK", null);
			avAlert.Show();

			return success;
		}

		public async Task<string> GetUserId()
		{
			Debug.WriteLine("UserId from appDelegate.cs: {0}", user.UserId);
			return user.UserId;
		}

		public async Task<bool> LogoutAsync()
		{
			bool success = false;
			try
			{
				if (user != null)
				{
					foreach (var cookie in NSHttpCookieStorage.SharedStorage.Cookies)
					{
						NSHttpCookieStorage.SharedStorage.DeleteCookie(cookie);
					}

					await TableManager.DefaultManager.CurrentClient.LogoutAsync();
					var logoutAlert = new UIAlertView("Authentication", "You are now logged out " + user.UserId, null, "OK", null);
					logoutAlert.Show();
				}
				user = null;
				success = true;
			}
			catch (Exception ex)
			{
				var logoutAlert = new UIAlertView("Logout failed", ex.Message, null, "OK", null);
				logoutAlert.Show();
			}
			return success;
		}
	}
}

