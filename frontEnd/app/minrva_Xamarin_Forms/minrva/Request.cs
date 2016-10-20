using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace minrva
{
	public class Request
	{
		string id;
		string borrower;
		string lender;
		string itemId;
		string startDate;
		string endDate;
		bool accepted;

		User borrowingUser;
		Boardgames requestedItem;

		public User BorrowingUser
		{
			get { return borrowingUser; }
			set { borrowingUser = value; }
		}

		public Boardgames RequestedItem
		{
			get { return requestedItem; }
			set { requestedItem = value; }
		}

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		[JsonProperty(PropertyName = "borrower")]
		public string Borrower
		{
			get { return borrower; }
			set { borrower = value; }
		}

		[JsonProperty(PropertyName = "lender")]
		public string Lender
		{
			get { return lender; }
			set { lender = value; }
		}

		[JsonProperty(PropertyName = "itemId")]
		public string ItemId
		{
			get { return itemId; }
			set { itemId = value; }
		}

		[JsonProperty(PropertyName = "startDate")]
		public string StartDate
		{
			get { return startDate; }
			set { startDate = value; }
		}

		[JsonProperty(PropertyName = "endDate")]
		public string EndDate
		{
			get { return endDate; }
			set { endDate = value; }
		}

		[JsonProperty(PropertyName = "accepted")]
		public bool Accepted
		{
			get { return accepted; }
			set { accepted = value; }
		}
	}
}
