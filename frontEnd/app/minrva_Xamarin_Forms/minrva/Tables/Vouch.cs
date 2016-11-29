using Newtonsoft.Json;

namespace minrva
{
	public class Vouch
	{
		string id;
		string voucher;
		string vouchee;
		string createdAt;
		string updatedAt;

		[JsonProperty(PropertyName = "id")]
		public string Id
		{
			get { return id; }
			set { id = value; }
		}

		[JsonProperty(PropertyName = "voucher")]
		public string Voucher
		{
			get { return voucher; }
			set { voucher = value; }
		}

		[JsonProperty(PropertyName = "vouchee")]
		public string Vouchee
		{
			get { return vouchee; }
			set { vouchee = value; }
		}

		[JsonProperty(PropertyName = "createdAt")]
		public string CreatedAt
		{
			get { return createdAt; }
			set { createdAt = value; }
		}

		[JsonProperty(PropertyName = "updatedAt")]
		public string UpdatedAt
		{
			get { return updatedAt; }
			set { updatedAt = value; }
		}

	}
}

