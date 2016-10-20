using System;
namespace minrva
{
	public class RequestMessage
	{

		Boardgames requestedItem;
		User borrower;
		Request request;

		public RequestMessage(Boardgames item, User borrowingUser, Request req)
		{
			requestedItem = item;
			borrower = borrowingUser;
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
	}
}
