using System;
namespace minrva
{
	public class RequestMessage
	{

		Boardgames requestedItem;
		User borrower;
		string requestType; // "Lend Request" or "Borrow Request"
		string acceptStatus; // "True", "False" or "Pending"
		string updatedAt;
		Request request;

		public RequestMessage(Boardgames item, User borrowingUser, string requestType, string acceptStatus,
		                      string updatedAt, Request req)
		{
			requestedItem = item;
			borrower = borrowingUser;
			this.requestType = requestType;
			this.acceptStatus = acceptStatus;
			this.updatedAt = updatedAt;
			request = req;
		}

		public User Borrower
		{
			get { return borrower; }
			set { borrower = value; }
		}

		public Boardgames RequestedItem
		{
			get { return requestedItem; }
			set { requestedItem = value; }
		}

		public Request Request
		{
			get { return request; }
			set { request = value; }
		}

		public string RequestType
		{
			get { return requestType; }
			set { requestType = value; }
		}

		public string AcceptStatus
		{
			get { return acceptStatus; }
			set { acceptStatus = value; }
		}

		public string UpdatedAt
		{
			get { return updatedAt; }
			set { updatedAt = value; }
		}
	}
}
