using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace minrva
{
	public partial class ChatPage : ContentPage
	{
		public String Username { get; set;}

		public ChatPage(String name, String itemId)
		{
			InitializeComponent();
			Username = name;
			BindingContext = this;
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}
	}
}
