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
			app.WaitForElement("First screen.");
		}



		[Test]
		public void UserCanAddItemAndThenViewThisItemOnTheirProfile()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Plus-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Enter Category"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Category");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Name"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Name");
			app.EnterText(x => x.Class("UITextField").Index(1), "Snakes and Ladders");
			app.Tap(x => x.Text("Enter Description"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.EnterText(x => x.Class("UITextField").Index(2), "12");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.WaitForElement("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Marked("Enter Location"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "Reading");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.WaitForElement("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Class("UIButtonLabel").Text("Add Item").Marked("Add Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
			app.Tap(x => x.Marked("Profile"));
			app.WaitForElement("Tapped on view with class: UITabBarButton marked: Profile");
			app.Tap(x => x.Marked("Board Game"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Board Game");
			app.Tap(x => x.Marked("Delete"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Delete");
		}

		[Test]
		public void UserAddsItemWithAMissingFieldResultingInAnError()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Plus-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Enter Category"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Category");
			app.Tap(x => x.Marked("Book"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Book");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Name"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Name");
			app.EnterText(x => x.Class("UITextField").Index(1), "Noughts");
			app.Tap(x => x.Text("“Noughts”"));
			app.WaitForElement("Tapped on view with class: UIMorphingLabel marked: “Noughts”");
			app.EnterText(x => x.Class("UITextField").Text("Noughts"), " and Crosses");
			app.Tap(x => x.Text("Enter Description"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Text("“Malorie”"));
			app.WaitForElement("Tapped on view with class: UIMorphingLabel marked: “Malorie”");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Location"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "London");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.WaitForElement("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Class("UIButtonLabel").Text("Add Item").Marked("Add Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
		}

		[Test]
		public void UserRequestsToBorrowItemAfterViewingLendersProfile()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("Liverpool"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Liverpool");
			app.Tap(x => x.Text("View Profile"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: View Profile");
			app.Tap(x => x.Text("Reviews"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Reviews");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
			app.ScrollDownTo("Borrow Item");
			app.WaitForElement("Scrolled to [AppView: Class=[Class: Name=UIButtonLabel], Id=, Text=Borrow Item, Marked=Borrow Item, Css=, XPath=, IndexInTree=-1, Rect=[Rectangle: Left=118.5, Top=527, CenterX=160, CenterY=536, Width=83, Height=18, Bottom=545, Right=201.5]]");
			app.Tap(x => x.Text("Borrow Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Borrow Item");
			app.Tap(x => x.Text("Send borrow request"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
		}

		[Test]
		public void UserRequestsToBorrowItemAfterViewingLendersProfileThenChangesMindAndDeletesRequestFromNotificationsPage()
		{
			app.Tap(x => x.Text("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Class("UIImageView").Index(22));
			app.WaitForElement("Tapped on view with class: UIImageView");
			app.ScrollDownTo("Borrow Item");
			app.WaitForElement("Scrolled to [AppView: Class=[Class: Name=UIButtonLabel], Id=, Text=Borrow Item, Marked=Borrow Item, Css=, XPath=, IndexInTree=-1, Rect=[Rectangle: Left=118.5, Top=527, CenterX=160, CenterY=536, Width=83, Height=18, Bottom=545, Right=201.5]]");
			app.Tap(x => x.Text("Borrow Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Borrow Item");
			app.Tap(x => x.Text("Send borrow request"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
			app.ScrollUpTo("Back");
			app.WaitForElement("Scrolled to [AppView: Class=[Class: Name=UIButtonLabel], Id=, Text=Back, Marked=Back, Css=, XPath=, IndexInTree=-1, Rect=[Rectangle: Left=10, Top=23, CenterX=27, CenterY=32, Width=34, Height=18, Bottom=41, Right=44]]");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Id("Notifications-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Borrow Request: Finding Nemo"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Borrow Request: Finding Nemo");
			app.Tap(x => x.Marked("Yes"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Yes");
		}

		[Test]
		public void UserCanSearchAndRequestToBorrowItemFromFeedByCategory()
		{
			app.Tap(x => x.Text("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("Choose category"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Choose category");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Class("UIImageView").Index(22));
			app.WaitForElement("Tapped on view with class: UIImageView");
			app.ScrollDownTo("Borrow Item");
			app.WaitForElement("Scrolled to [AppView: Class=[Class: Name=UIButtonLabel], Id=, Text=Borrow Item, Marked=Borrow Item, Css=, XPath=, IndexInTree=-1, Rect=[Rectangle: Left=118.5, Top=527, CenterX=160, CenterY=536, Width=83, Height=18, Bottom=545, Right=201.5]]");
			app.Tap(x => x.Text("Borrow Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Borrow Item");
			app.Tap(x => x.Text("Send borrow request"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
		}

		[Test]
		public void UserCanRequestToBorrowItemFromFeedBySearchingForAnItem()
		{
			app.Tap(x => x.Text("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Text("Search"));
			app.WaitForElement("Tapped on view with class: UISearchBarTextFieldLabel marked: Search");
			app.EnterText(x => x.Marked("Search"), "Finding Nemo");
			app.Tap(x => x.Class("UIImageView").Index(23));
			app.WaitForElement("Tapped on view with class: UIImageView");
			app.ScrollDownTo("Borrow Item");
			app.WaitForElement("Scrolled to [AppView: Class=[Class: Name=UIButtonLabel], Id=, Text=Borrow Item, Marked=Borrow Item, Css=, XPath=, IndexInTree=-1, Rect=[Rectangle: Left=118.5, Top=527, CenterX=160, CenterY=536, Width=83, Height=18, Bottom=545, Right=201.5]]");
			app.Tap(x => x.Text("Borrow Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Borrow Item");
			app.Tap(x => x.Text("Send borrow request"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
		}

		[Test]
		public void UserCanUpdateProfilePicture()
		{
			app.Tap(x => x.Text("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Text("Update"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Update");
			app.Tap(x => x.Marked("5"));
			app.WaitForElement("Tapped on view with class: UILabel marked: 5");
			app.Tap(x => x.Class("PUPhotoView"));
			app.WaitForElement("Tapped on view with class: PUPhotoView");
		}

		[Test]
		public void LenderAddsAnItemAndUserRequestsToBorrowThisItemAfterViewingTheirProfile()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Plus-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Enter Category"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Category");
			app.Tap(x => x.Marked("DVD"));
			app.WaitForElement("Tapped on view with class: UILabel marked: DVD");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Name"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Name");
			app.EnterText(x => x.Class("UITextField").Index(1), "Despicable Me");
			app.Tap(x => x.Text("Enter Description"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Text("Minions"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.EnterText(x => x.Class("UITextField").Index(2), "21");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.WaitForElement("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Marked("Enter Location"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "Bristol");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.WaitForElement("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Class("UIButtonLabel").Text("Add Item").Marked("Add Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.ScrollUp();
			app.WaitForElement("Swipped down");
			app.Tap(x => x.Text("Logout"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Logout");
			app.TouchAndHold(x => x.Marked("Sign-in"));
			app.WaitForElement("Long press on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("minrva_icon.png"));
			app.WaitForElement("Tapped on view with class: UIImageView");
			app.Tap(x => x.Text("View Profile"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: View Profile");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Text("Borrow Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Borrow Item");
			app.Tap(x => x.Text("Send borrow request"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send borrow request");
			app.Tap(x => x.Marked("Okay"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Okay");
		}

		[Test]
		public void LenderReceivesBorrowRequestNotificationAndAcceptsBorrower()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Notifications-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Minrva wants to borrow your item"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Minrva wants to borrow your item");
			app.Tap(x => x.Marked("View Profile"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: View Profile");
			app.Tap(x => x.Text("Accept"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Accept");
		}

		[Test]
		public void BorrowerStartsChatWithLenderAfterSeeingRequestWasAcceptedAndLenderRespondsToThisChat()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Speech Bubble-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer").Index(1));
			app.WaitForElement("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Text("Enter Message"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Text("Send Message"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send Message");
			app.Tap(x => x.Text("Items"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Items");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Text("Logout"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Logout");
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Speech Bubble-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Hey! Thanks for accepting! Where shall we meet?"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Hey! Thanks for accepting! Where shall we meet?");
			app.Tap(x => x.Text("Enter Message"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Text("Send Message"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send Message");
			app.Tap(x => x.Text("Items"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Items");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
		}

		[Test]
	public void LenderMarksItemAsReturnedAndCompletesReviewForBorrower()
	{
		app.Tap(x => x.Marked("Sign-in"));
		app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
		app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
		app.WaitForElement("Tapped on view with class: UIWebView");
		app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
		app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
		app.WaitForElement("Tapped on view with class: UIWebView");
		app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
		app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
		app.WaitForElement("Tapped on view with class: UIWebView");
		app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
		app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
		app.Tap(x => x.Marked("DVD").Index(1));
		app.WaitForElement("Tapped on view with class: UILabel marked: DVD");
		app.Tap(x => x.Marked("Mark As Returned"));
		app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Mark As Returned");
		app.Tap(x => x.Class("SFRatingItem").Index(8));
		app.WaitForElement("Tapped on view with class: SFRatingItem");
		app.Tap(x => x.Text("Leave a review for this user"));
		app.WaitForElement("Tapped on view with class: UITextView");
		app.Tap(x => x.Marked("Done"));
		app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
		app.Tap(x => x.Text("Send Review"));
		app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send Review");
		app.Tap(x => x.Marked("Ok"));
		app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
	}

		[Test]
		public void BorrowerClicksOnReturnedNotificationAndCompletesReviewForLenderAndItemBorrowed()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Notifications-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Minrva - Returned"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Minrva - Returned");
			app.Tap(x => x.Class("SFRatingItem").Index(3));
			app.WaitForElement("Tapped on view with class: SFRatingItem");
			app.Tap(x => x.Text("Leave a review for this user"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Class("SFRatingItem").Index(8));
			app.WaitForElement("Tapped on view with class: SFRatingItem");
			app.Tap(x => x.Text("Leave a review for this item"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Text("Send Review"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Send Review");
			app.Tap(x => x.Marked("Ok"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
		}

		[Test]
		public void LenderVouchesForBorrowerAndBorrowerSeesVouchNotificationAndThenChecksVouchesOnProfile()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("Search"));
			app.WaitForElement("Tapped on view with class: UISearchBarTextField marked: Search");
			app.EnterText(x => x.Marked("Search"), "M");
			app.EnterText(x => x.Marked("Search"), "inrva");
			app.Tap(x => x.Id("minrva_icon.png"));
			app.WaitForElement("Tapped on view with class: UIImageView");
			app.Tap(x => x.Text("Vouch"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Vouch");
			app.Tap(x => x.Marked("OK"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: OK");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Text("Logout"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Logout");
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Notifications-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer").Index(1));
			app.WaitForElement("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Text("Vouches"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Vouches");
			app.ScrollUp();
			app.WaitForElement("Swipped down");
			app.Tap(x => x.Class("UIImageView").Index(4));
			app.WaitForElement("Tapped on view with class: UIImageView");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
		}

		[Test]
		public void LenderVisitsTrustNetworkOnProfileToVouchForPeopleThatBorrowerHasVouchedFor()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Text("Trust Network"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Trust Network");
			app.Tap(x => x.Id("minrva_icon.png"));
			app.WaitForElement("Tapped on view with class: UIImageView");
			app.Tap(x => x.Class("UIImageView").Index(3));
			app.WaitForElement("Tapped on view with class: UIImageView");
			app.Tap(x => x.Text("Vouch"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Vouch");
			app.Tap(x => x.Marked("OK"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: OK");
		}

		[Test]
		public void UserLendsOutAnItemAndSeesGemStoneRankChange()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Id("Plus-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Marked("Enter Category"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Category");
			app.Tap(x => x.Marked("Book"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Book");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Name"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Name");
			app.EnterText(x => x.Class("UITextField").Index(1), "Of Mice and Men");
			app.Tap(x => x.Text("Enter Description"));
			app.WaitForElement("Tapped on view with class: UITextView");
			app.Tap(x => x.Marked("Done"));
			app.WaitForElement("Tapped on view with class: UIToolbarTextButton marked: Done");
			app.Tap(x => x.Marked("Enter Number of Days Willing to Lend"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Number of Days Willing to Lend");
			app.EnterText(x => x.Class("UITextField").Index(2), "12");
			app.Tap(x => x.Class("Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer"));
			app.WaitForElement("Tapped on view with class: Xamarin_Forms_Platform_iOS_Platform_DefaultRenderer");
			app.Tap(x => x.Marked("Enter Location"));
			app.WaitForElement("Tapped on view with class: UITextFieldLabel marked: Enter Location");
			app.EnterText(x => x.Class("UITextField").Index(3), "London");
			app.PressEnter();
			app.Tap(x => x.Class("UIButtonLabel").Text("Add Item").Marked("Add Item"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Add Item");
			app.Tap(x => x.Marked("Ok"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: Ok");
			app.Tap(x => x.Id("Gender Neutral User-50@2x.png"));
			app.WaitForElement("Tapped on view with class: UITabBarSwappableImageView");
			app.Tap(x => x.Text("Rank Table"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Rank Table");
			app.Tap(x => x.Text("Back"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Back");
		}

		[Test]
		public void BorrowerCanSeeItemsAvailableFromMapOnFeedAndClickingOnPinGivesItemNameAndDescription()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "otheraccount9898@hotmail.co.uk");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Marked("Map"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Map");
			app.Tap(x => x.Marked("Of Mice and Men, American Dream"));
			app.WaitForElement("Tapped on view with class: MKPinAnnotationView marked: Of Mice and Men, American Dream");
			app.Tap(x => x.ClassFull("_MKSmallCalloutPassthroughButton"));
			app.WaitForElement("Tapped on view with class: _MKSmallCalloutPassthroughButton");
		}

		[Test]
		public void LenderReceivesBorrowRequestAndRejectsUser()
		{
			app.Tap(x => x.Marked("Sign-in"));
			app.WaitForElement("Tapped on view with class: UIButton marked: Sign-in");
			app.Tap(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT._56bg._4u9z._5ruq"), "minrvatester2@gmail.com");
			app.Tap(x => x.Class("UIWebView").Css("INPUT#u_0_2"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.EnterText(x => x.Class("UIWebView").Css("INPUT#u_0_2"), "Imperial09!");
			app.Tap(x => x.Class("UIWebView").Css("BUTTON#u_0_6"));
			app.WaitForElement("Tapped on view with class: UIWebView");
			app.Tap(x => x.Text("Notifications"));
			app.WaitForElement("Tapped on view with class: UITabBarButtonLabel marked: Notifications");
			app.Tap(x => x.Marked("Minrva wants to borrow your item"));
			app.WaitForElement("Tapped on view with class: UILabel marked: Minrva wants to borrow your item");
			app.Tap(x => x.Marked("View Profile"));
			app.WaitForElement("Tapped on view with class: _UIAlertControllerActionView marked: View Profile");
			app.Tap(x => x.Text("Reject"));
			app.WaitForElement("Tapped on view with class: UIButtonLabel marked: Reject");
		}





	}

