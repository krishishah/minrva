using System;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;

namespace minrva
{
	public class Chat
	{
		string id;
		string borrower;
		string lender;
		bool deleted;

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


		[JsonProperty(PropertyName = "deleted")]
		public bool Deleted
		{
			get { return deleted; }
			set { deleted = value; }
		}

		[Version]
		public string Version { get; set; }
	}
}

