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
		public void CheckIfBorrowRequestIsSuccessful()
		{

			app.Tap(x => x.Marked("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("OK"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: OK");
			app.Tap(x => x.Class("UITableViewCellContentView"));
			app.Screenshot("Tapped on view with class: UITableViewCellContentView");
			app.Tap(x => x.Marked("Borrow"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Borrow");
			app.Tap(x => x.Text("Send borrow request"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Okay");

		}


		[Test]
		public void CheckIfItemIsAddedAndAppearsOnUserProfile()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("OK"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: OK");
			app.Tap(x => x.Id("Plus-50@2x.png"));
			app.Screenshot("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Enter Category"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Category");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Name"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Name");
			app.EnterText(x => x.Class("UITextField").Index(1), "Lord of the Rings");
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.EnterText(x => x.Class("UITextField").Index(2), "8");
			app.Tap(x => x.Marked("Enter Location"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "Leeds");
			app.PressEnter();
			app.Tap(x => x.Text("Add Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.Screenshot("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Class("UITableViewCellContentView"));
			app.Screenshot("Tapped on view with class: UITableViewCellContentView");
			app.Tap(x => x.Marked("Ok"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
		}

		[Test]
		public void CheckIfErrorAppearsWhenFieldInAddItemIsNotCompleted()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("OK"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: OK");
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
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.Tap(x => x.Text("Enter Description"));
			app.Screenshot("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.EnterText(x => x.Class("UITextField").Index(2), "6");
			app.Tap(x => x.Marked("Enter Location"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "London");
			app.PressEnter();
			app.Tap(x => x.Text("Add Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
		}





	}
}
