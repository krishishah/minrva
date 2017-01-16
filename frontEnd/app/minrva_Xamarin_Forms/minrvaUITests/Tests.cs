using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;


namespace minrvaUITests
{
	[TestFixture]
	public class Tests
	{
		iOSApp app;

		[SetUp]
		public void BeforeEachTest()
		{
			// TODO: If the iOS app being tested is included in the solution then open
			// the Unit Tests window, right click Test Apps, select Add App Project
			// and select the app projects that should be tested.
			//
			// The iOS project should have the Xamarin.TestCloud.Agent NuGet package
			// installed. To start the Test Cloud Agent the following code should be
			// added to the FinishedLaunching method of the AppDelegate:
			//
			//    #if ENABLE_TEST_CLOUD
			//    Xamarin.Calabash.Start();
			//    #endif
			app = ConfigureApp
				.iOS
				// TODO: Update this path to point to your iOS app and uncomment the
				// code if the app is not included in the solution.
				//.AppBundle ("../../../iOS/bin/iPhoneSimulator/Debug/minrvaUITests.iOS.app")
				.StartApp();
		}

		[Test]
		public void AppLaunches()
		{
			app.Screenshot("First screen.");
		}



		[Test]
		public void UserCanAddItemAndThenViewThisItemOnTheirProfile()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Plus-50@2x.png"));
			app.Screenshot("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Enter Category"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Category");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Name"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Name");
			app.EnterText(x => x.Class("UITextField").Index(1), "Snakes and Ladders");
			app.Tap(x => x.Text("Enter Description"));
			app.Screenshot("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.EnterText(x => x.Class("UITextField").Index(2), "12");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.Screenshot("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Marked("Enter Location"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "Reading");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.Screenshot("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Class("UIButtonLabel").Text("Add Item").Marked("Add Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
			app.Tap(x => x.Marked("Profile"));
			app.Screenshot("Tapped on view with class: UITabBarButton marked: Profile");
			app.Tap(x => x.Marked("Board Game"));
			app.Screenshot("Tapped on view with class: UILabel marked: Board Game");
			app.Tap(x => x.Marked("Delete"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Delete");
		}

		[Test]
		public void UserAddsItemWithAMissingFieldResultingInAnError()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Plus-50@2x.png"));
			app.Screenshot("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Enter Category"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Category");
			app.Tap(x => x.Marked("Book"));
			app.Screenshot("Tapped on view with class: UILabel marked: Book");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Name"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Name");
			app.EnterText(x => x.Class("UITextField").Index(1), "Noughts");
			app.Tap(x => x.Text("“Noughts”"));
			app.Screenshot("Tapped on view with class: UIMorphingLabel marked: “Noughts”");
			app.EnterText(x => x.Class("UITextField").Text("Noughts"), " and Crosses");
			app.Tap(x => x.Text("Enter Description"));
			app.Screenshot("Tapped on view with class: UITextView");
			app.Tap(x => x.Text("“Malorie”"));
			app.Screenshot("Tapped on view with class: UIMorphingLabel marked: “Malorie”");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Location"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "London");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.Screenshot("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Class("UIButtonLabel").Text("Add Item").Marked("Add Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
		}

		[Test]
		public void UserRequestsToBorrowItemAfterViewingLendersProfile()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("Liverpool"));
			app.Screenshot("Tapped on view with class: UILabel marked: Liverpool");
			app.Tap(x => x.Text("View Profile"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: View Profile");
			app.Tap(x => x.Text("Reviews"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Reviews");
			app.Tap(x => x.Text("Back"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Text("Back"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Back");
			app.ScrollDownTo("Borrow Item");
			app.Screenshot("Scrolled to [AppView: Class=[Class: Name=UIButtonLabel], Id=, Text=Borrow Item, Marked=Borrow Item, Css=, XPath=, IndexInTree=-1, Rect=[Rectangle: Left=118.5, Top=527, CenterX=160, CenterY=536, Width=83, Height=18, Bottom=545, Right=201.5]]");
			app.Tap(x => x.Text("Borrow Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Borrow Item");
			app.Tap(x => x.Text("Send borrow request"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
		}



	}
}
