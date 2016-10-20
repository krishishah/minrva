using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace minrva
{
	public class User
	{
		string id;
		string userId;
		string firstName;
		string lastName;
		string email;

		//public UserInformation(string id, string fName, string lName, string emailAddr)
		//{
		//	firstName = fName;
		//	lastName = lName;
		//	email = emailAddr;
		//}

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		[JsonProperty(PropertyName = "userId")]
		public string UserId
		{
			get { return userId; }
			set { userId = value; }
		}

		[JsonProperty(PropertyName = "firstName")]
		public string FirstName
		{
			get { return firstName; }
			set { firstName = value; }
		}

		[JsonProperty(PropertyName = "lastName")]
		public string LastName
		{
			get { return lastName; }
			set { lastName = value; }
		}

		[JsonProperty(PropertyName = "email")]
		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}, {1}", FirstName, LastName);
		}

	}
}

