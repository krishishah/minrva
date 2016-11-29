using System;
using Xamarin.Forms;

namespace minrva
{

	public class UserFeedViewModel
	{
		string id;
		string name;
		ImageSource imageSource;

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public ImageSource ImageSource
		{
			get { return imageSource; }
			set { imageSource = value; }
		}

		public string Id
		{
			get { return id; }
			set { id = value; }
		}
	}


}
