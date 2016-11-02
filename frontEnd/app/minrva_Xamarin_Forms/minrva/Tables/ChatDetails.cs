using System;
namespace minrva
{
	public class ChatDetails
	{

		Boardgames requestedItem;
		User recipient;

		public ChatDetails(Boardgames requestedItem, User recipient)
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
	}
}
