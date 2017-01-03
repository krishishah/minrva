using System;
namespace minrva
{
	public class RequestMessage
	{

		Boardgames requestedItem;
		User otherUser;
		string requestType; // "Lend Request" or "Borrow Request"
		string acceptStatus; // "True", "False" or "Pending"
		string updatedAt;
		string notificationView;
		string notificationViewDetail;
		string notificationColour;
		Request request;

		public RequestMessage(Boardgames item, User notifier, string requestType, string acceptStatus,
		                      string updatedAt, string notificationView, string notificationViewDetail, string notificationColour, Request req)
		{
			requestedItem = item;
			otherUser = notifier;
			this.requestType = requestType;
			this.acceptStatus = acceptStatus;
			this.updatedAt = updatedAt;
			this.notificationView = notificationView;
			this.notificationViewDetail = notificationViewDetail;
			this.notificationColour = notificationColour;
			request = req;
		}

		public User OtherUser
		{
			get { return otherUser; }
			set { otherUser = value; }
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

		public string NotificationView
		{
			get { return notificationView; }
			set { notificationView = value; }
		}

		public string NotificationViewDetail
		{
			get { return notificationViewDetail; }
			set { notificationViewDetail = value; }
		}

		public string NotificationColour
		{
			get { return notificationColour; }
			set { notificationColour = value; }
		}
	}
}
