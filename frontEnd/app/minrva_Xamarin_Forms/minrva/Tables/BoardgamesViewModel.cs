using System;
using Xamarin.Forms;

namespace minrva
{
	public class BoardgamesViewModel
	{
		string id;
		string name;
		string description;
		string location;
		string owner;
		int lend_duration;
		bool borrowed;
		double latitude;
		double longitude;
		string category;
		string createdAt;
		ImageSource imageSource;
		double distance;

		//public BoardgamesViewModel(string id, string name, string location, ImageSource itemImageSource, double distance,
		//                          string owner, int lend_duration, bool borrowed, double latitude, double longitude,
		//                           string category, string createdAt, ImageSource)

		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}


		public string Owner
		{
			get { return owner; }
			set { owner = value; }
		}

		public int Lend_duration
		{
			get { return lend_duration; }
			set { lend_duration = value; }
		}

		public string Location
		{
			get { return location; }
			set { location = value; }
		}

		public double Latitude
		{
			get { return latitude; }
			set { latitude = value; }
		}

		public double Longitude
		{
			get { return longitude; }
			set { longitude = value; }
		}

		public string Category
		{
			get { return category; }
			set { category = value; }
		}

		public ImageSource ImageSource
		{
			get { return imageSource; }
			set { imageSource = value; }
		}

		public double Distance
		{
			get { return distance; }
			set { distance = value; }
		}

		public string Description
		{
			get { return description; }
			set { description = value; }
		}

	}
}
