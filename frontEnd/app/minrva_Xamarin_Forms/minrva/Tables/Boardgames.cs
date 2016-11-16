using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace minrva
{
	public class Boardgames
	{
		string id;
		string name;
		string description;
		int lend_duration;
		string location;
		string owner;
		bool borrowed;
		double latitude;
		double longitude;
		string category;
		string createdAt;
		double distance;

		[JsonProperty(PropertyName = "distance")]
		public double Distance
		{
			get { return distance; }
			set { distance = value; }
		}

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		[JsonProperty(PropertyName = "name")]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		[JsonProperty(PropertyName = "description")]
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		[JsonProperty(PropertyName = "lend_duration")]
		public int Lend_duration
		{
			get { return lend_duration; }
			set { lend_duration = value; }
		}

		[JsonProperty(PropertyName = "location")]
		public string Location
		{
			get { return location; }
			set { location = value; }
		}

		[JsonProperty(PropertyName = "latitude")]
		public double Latitude
		{
			get { return latitude; }
			set { latitude = value; }
		}

		[JsonProperty(PropertyName = "longitude")]
		public double Longitude
		{
			get { return longitude; }
			set { longitude = value; }
		}

		[JsonProperty(PropertyName = "owner")]
		public string Owner
		{
			get { return owner; }
			set { owner = value; }
		}

		[JsonProperty(PropertyName = "borrowed")]
		public bool Borrowed
		{
			get { return borrowed; }
			set { borrowed = value; }
		}

		[JsonProperty(PropertyName = "category")]
		public string Category
		{
			get { return category; }
			set { category = value; }
		}

		[JsonProperty(PropertyName = "createdAt")]
		public string CreatedAt
		{
			get { return createdAt; }
			set { createdAt = value; }
		}

		[Version]
		public string Version { get; set; }
	}
}

