using System;
namespace minrva
{
	public class ChatDetails
	{

		String lastMessage;
		User recipient;
		bool amIBorrower;

		public ChatDetails(String lastMessage, User recipient, bool amIBorrower)
		{
			this.lastMessage = lastMessage;
			this.recipient = recipient;
		}

		public String LastMessage
		{
			get { return lastMessage; }
			set { lastMessage = value; }
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
