using System;
using System.Collections.Generic;
using System.Text;

namespace minrva
{
	public static class Configuration
	{
		/// <summary>
		/// Azure Storage Connection String. UseDevelopmentStorage=true points to the storage emulator.
		/// </summary>
		public const string StorageConnectionString = "SharedAccessSignature=sv=2015-12-11&ss=b&srt=sco&sp=rwdlacup&st=2016-11-14T18%3A35%3A00Z&se=2017-03-15T18%3A35%3A00Z&sig=DAxq3WrK6sAgyXdT5FIGcv9CLfmzm8FA%2FBUP9oeqyH0%3D;BlobEndpoint=https://minrva.blob.core.windows.net";
	}
}
