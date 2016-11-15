﻿using System;

using Xamarin.Forms;

using System.Threading.Tasks;

namespace minrva
{ 

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
			MainPage = new LoginPage();
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

