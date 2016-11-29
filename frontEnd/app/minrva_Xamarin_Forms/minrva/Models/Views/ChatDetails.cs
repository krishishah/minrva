using System;
namespace minrva
{
	public class ChatDetails
	{

		Boardgames requestedItem;
		User recipient;
		bool amIBorrower;

		public ChatDetails(Boardgames requestedItem, User recipient, bool amIBorrower)
		{
			this.requestedItem = requestedItem;
			this.recipient = recipient;
		}

		public Boardgames RequestedItem
		{
			get { return requestedItem; }
			set { requestedItem = value; }
		}

		public User Recipient
		{
			get { return recipient; }
			set { recipient = value; }
		}

		public bool AmIBorrower
		{
			get { return amIBorrower; }
			set { amIBorrower = value; }
		}
	}
}
