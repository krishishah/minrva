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



		//[Test]
		//public void CheckIfBorrowRequestIsSuccessful()
		//{
		//	app.Tap(x => x.Marked("Sign-in"));
		//	app.Screenshot("Tapped on view with class: UIButton marked: Sign-in");
		//	app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
		//	app.Screenshot("Tapped on view with class: UIWebView");
		//	app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
		//	app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
		//	app.Screenshot("Tapped on view with class: UIWebView");
		//	app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
		//	app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
		//	app.Screenshot("Tapped on view with class: UIWebView");
		//	app.Tap(x => x.Marked("OK"));
		//	app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: OK");
		//	app.Tap(x => x.Class("UITableViewCellContentView"));
		//	app.Screenshot("Tapped on view with class: UITableViewCellContentView");
		//	app.Tap(x => x.Marked("Borrow"));
		//	app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Borrow");
		//	app.Tap(x => x.Text("Send borrow request"));
		//	app.Screenshot("Tapped on view with class: UIButtonLabel marked: Send borrow request");
		//	app.Tap(x => x.Marked("Okay"));
		//	app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
		//}

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
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("OK"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: OK");
			app.Tap(x => x.Id("minrva_icon.png"));
			app.Screenshot("Tapped on view with class: UIImageView");
			app.ScrollDown();
			app.Screenshot("Swiped up");
			app.Tap(x => x.Text("Borrow Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Borrow Item");
			app.Tap(x => x.Text("Send borrow request"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
		}



		[Test]
		public void CheckIfItemAddedByUserIsShownOnProfile()
		{
			app.Tap(x => x.Text("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Sign-in");
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
			app.EnterText(x => x.Class("UITextField").Index(1), "Trivial Pursuit");
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.EnterText(x => x.Class("UITextField").Index(2), "5");
			app.Tap(x => x.Marked("Enter Location"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "London");
			app.PressEnter();
			app.Tap(x => x.Text("Add Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.Screenshot("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Class("UITableViewCellContentView").Index(5));
			app.Screenshot("Tapped on view with class: UITableViewCellContentView");
			app.Tap(x => x.Marked("Delete"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Delete");
		}





		[Test]
		public void CheckIfAUserCanRequestToBorrowAnItemAnotherUserAdded()
		{
			app.Tap(x => x.Text("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Sign-in");
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
			app.ScrollDown();
			app.Screenshot("Swiped up");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Name"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Name");
			app.EnterText(x => x.Class("UITextField").Index(1), "Signs");
			app.Tap(x => x.Text("Enter Description"));
			app.Screenshot("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.Screenshot("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.EnterText(x => x.Class("UITextField").Index(2), "20");
			app.Tap(x => x.Marked("Enter Location"));
			app.Screenshot("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "Sheffield");
			app.PressEnter();
			app.Tap(x => x.Text("Add Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
			app.Tap(x => x.Text("Profile"));
			app.Screenshot("Tapped on view with class: UITabBarButtonLabel marked: Profile");
			app.Tap(x => x.Text("Logout"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Logout");
			app.Tap(x => x.Marked("OK"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: OK");
			app.Tap(x => x.Text("Sign-in"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_1"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_1"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_5"));
			app.Screenshot("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("OK"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: OK");
			app.Tap(x => x.Class("UITableViewCellContentView").Index(3));
			app.Screenshot("Tapped on view with class: UITableViewCellContentView");
			app.ScrollDownTo("Borrow Item");
			app.Screenshot("ScrollToEvent[AppView: Class=[Class: Name=UIButtonLabel], Id=, Text=Borrow Item, Marked=Borrow Item, Css=, XPath=, IndexInTree=-1, Rect=[Rectangle: Left=118.5, Top=552.5, CenterX=160, CenterY=561.5, Width=83, Height=18, Bottom=570.5, Right=201.5]]");
			app.Tap(x => x.Text("Borrow Item"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Borrow Item");
			app.Tap(x => x.Text("Send borrow request"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.Screenshot("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
			app.ScrollUp();
			app.Screenshot("Swipped down");
			app.ScrollUp();
			app.Screenshot("Swipped down");
			app.Tap(x => x.Class("UIStatusBarServiceItemView"));
			app.Screenshot("Tapped on view with class: UIStatusBarServiceItemView");
			app.Tap(x => x.Text("Back"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.Screenshot("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Text("Logout"));
			app.Screenshot("Tapped on view with class: UIButtonLabel marked: Logout");
		}


	}
}
