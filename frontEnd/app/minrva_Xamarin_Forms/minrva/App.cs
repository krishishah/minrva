using System;

using Xamarin.Forms;

using System.Threading.Tasks;

namespace minrva
{

	public interface IAuthenticate
	{
		Task<bool> Authenticate();
	}

	public class App : Application
	{
		public static IAuthenticate Authenticator { get; private set; }

		public static void Init(IAuthenticate authenticator)
		{
			Authenticator = authenticator;
		}

		public App ()
		{
			// The root page of your applicationn
			MainPage = new MainTabContainer();

		//	var welcomeLabel = new Label
		//	{
		//		HorizontalOptions = LayoutOptions.CenterAndExpand
		//	};
		//	var fbButton = new Button
		//	{
		//		Text = "Facebook",
		//		HorizontalOptions = LayoutOptions.CenterAndExpand,
		//		BackgroundColor = Color.FromHex("#3b5998")
		//	};
		//	var googleButton = new Button
		//	{
		//		Text = "Google+",
		//		HorizontalOptions = LayoutOptions.CenterAndExpand,
		//		BackgroundColor = Color.FromHex("#d50f25")
		//	};
		//	var twitterButton = new Button
		//	{
		//		Text = "Twitter",
		//		HorizontalOptions = LayoutOptions.CenterAndExpand,
		//		BackgroundColor = Color.FromHex("#55acee")
		//	};

		//	MainPage = new ContentPage
		//	{
		//		Content = new StackLayout
		//		{
		//			VerticalOptions = LayoutOptions.Center,
		//			Children = {
		//	fbButton,
		//	googleButton,
		//	twitterButton,
		//	welcomeLabel
		//}
		//		}
		//	};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

