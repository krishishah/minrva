//using System;
//namespace minrva
//{
//	public static class SocialLogin
//	{
//		public SocialLogin()
//		{
//		}

//		private static readonly MobileServiceClient Client =
//		new MobileServiceClient("http://minrva.azurewebsites.net", "YOUR_APP_KEY");

//		private static async Task<MobileServiceUser> Authenticate(
//			MobileServiceAuthenticationProvider provider)
//		{
//			try
//			{
//#if __IOS__
//            return await Client.LoginAsync(
//                UIKit.UIApplication.SharedApplication
//                    .KeyWindow.RootViewController, 
//                    provider);
//#elif WINDOWS_PHONE
//            return await Client.LoginAsync(provider);
//#else
//				return await Client.LoginAsync(
//					Xamarin.Forms.Forms.Context,
//					provider);
//#endif
//			}
//			catch (Exception ex)
//			{
//				return null;
//			}
//		}

//		public static async Task<MobileServiceUser> AuthenticateGoogle()
//		{
//			return await
//			 Authenticate(MobileServiceAuthenticationProvider.Google);
//		}

//		public static async Task<MobileServiceUser> AuthenticateTwitter()
//		{
//			return await
//			 Authenticate(MobileServiceAuthenticationProvider.Twitter);
//		}

//		public static async Task<MobileServiceUser> AuthenticateFacebook()
//		{
//			return await
//			 Authenticate(MobileServiceAuthenticationProvider.Facebook);
//		}
//	}
//}
