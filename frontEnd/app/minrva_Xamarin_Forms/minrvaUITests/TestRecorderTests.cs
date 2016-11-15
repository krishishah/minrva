using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
[TestFixture]
public class RecorderTest {
	public iOSApp app;
	[SetUp]
	public void Setup() {
		app = ConfigureApp.iOS.AppBundle("/Users/Ashwitha/minrva/frontEnd/app/minrva_Xamarin_Forms/iOS/bin/iPhoneSimulator/Debug/xtr-minrva.iOS.app").StartApp();
	}

	[Test]
	public void NewTest() {
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

}
