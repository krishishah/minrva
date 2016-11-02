using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace minrva
{
	public partial class MessagePage : ContentPage
	{
		string descriptionPlaceholder = "Enter Message";
		public String Username { get; set;}

		public MessagePage(String name, String itemId)
		{
			InitializeComponent();
			Username = name;
			BindingContext = this;
		}

		public async void BackButtonCommand(object sender, EventArgs e)
		{
			await Navigation.PopModalAsync();
		}

		void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
		{
			if (Equals(newMessage.Text, descriptionPlaceholder))
			{
				newMessage.Text = string.Empty;
				newMessage.TextColor = Color.Black;
			}
		}
	}
}
