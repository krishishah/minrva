using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace minrva
{

	public partial class NewMessagePage : ContentPage
	{
		public String Username { get; set;}


		public NewMessagePage(String name, String itemID)
		{
			InitializeComponent();
			Username = name;
			BindingContext = this;

		}


	}

}