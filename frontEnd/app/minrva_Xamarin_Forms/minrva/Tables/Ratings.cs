using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace minrva
{
	public class Ratings
	{
		string id;
		bool isItem;
		float rating;
		string review;
		string ratedID;
		string reviewerID;

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		[JsonProperty(PropertyName = "isItem")]
		public bool IsItem
		{
			get { return isItem; }
			set { isItem = value; }
		}

		[JsonProperty(PropertyName = "rating")]
		public float Rating
		{
			get { return rating; }
			set { rating = value; }
		}

		[JsonProperty(PropertyName = "review")]
		public string Review
		{
			get { return review; }
			set { review = value; }
		}

		[JsonProperty(PropertyName = "ratedID")]
		public string RatedID
		{
			get { return ratedID; }
			set { ratedID = value; }
		}

		[JsonProperty(PropertyName = "reviewerID")]
		public string ReviewerID
		{
			get { return reviewerID; }
			set { reviewerID = value; }
		}

	}
}
