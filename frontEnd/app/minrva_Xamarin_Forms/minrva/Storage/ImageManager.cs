using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Collections.Generic;
using Plugin.Media;

namespace minrva
{
	/// <summary>
	/// The image manager is responsible for uploading/downloading and listing images from the Blob Azure Storage
	/// </summary>
	public class ImageManager
	{
		public ImageManager()
		{
		}

		/// <summary>
		/// Gets a reference to the container for storing the images
		/// </summary>
		/// <returns></returns>
		private static CloudBlobContainer GetContainer()
		{
			// Parses the connection string for the Windows Azure Storage Account
			var account = CloudStorageAccount.Parse(Configuration.StorageConnectionString);
			var client = account.CreateCloudBlobClient();

			// Gets a reference to the images container
			var container = client.GetContainerReference("minrvaimages");

			return container;
		}

		private static CloudBlobContainer GetProfilePictureContainer()
		{
			// Parses the connection string for the Windows Azure Storage Account
			var account = CloudStorageAccount.Parse(Configuration.StorageConnectionString);
			var client = account.CreateCloudBlobClient();

			// Gets a reference to the images container
			var container = client.GetContainerReference("profilepictures");

			return container;
		}

		/// <summary>
		/// Uploads a new image to a blob container.
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static async Task<string> UploadImage(Stream image, string name)
		{
			var container = GetContainer();

			// Creates the container if it does not exist
			await container.CreateIfNotExistsAsync();

			// Uploads the image the blob storage
			var imageBlob = container.GetBlockBlobReference(name);
			await imageBlob.UploadFromStreamAsync(image);

			return name;
		}

		public static async Task<string> UploadProfilePicture(Stream image, string name)
		{
			var container = GetProfilePictureContainer();

			// Creates the container if it does not exist
			await container.CreateIfNotExistsAsync();

			//Uploads the image to blob storage
			var imageBlob = container.GetBlockBlobReference(name);
			await imageBlob.UploadFromStreamAsync(image);

			return name;
		}

		/// <summary>
		/// Lists of all the available images in the blob container
		/// </summary>
		/// <returns></returns>
		public static async Task<string[]> ListImages()
		{
			var container = GetContainer();

			// Iterates multiple times to get all the available blobs
			var allBlobs = new List<string>();
			BlobContinuationToken token = null;

			do
			{
				var result = await container.ListBlobsSegmentedAsync(token);
				if (result.Results.Count() > 0)
				{
					var blobs = result.Results.Cast<CloudBlockBlob>().Select(b => b.Name);
					allBlobs.AddRange(blobs);
				}

				token = result.ContinuationToken;
			} while (token != null); // no more blobs to retrieve

			return allBlobs.ToArray();
		}



		/// <summary>
		/// Gets an image from the blob container using the name
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static async Task<byte[]> GetImage(string name)
		{
			var container = GetContainer();

			//Gets the block blob representing the image
			var blob = container.GetBlobReference(name);

			if (await blob.ExistsAsync())
			{
				// Gets the block blob length to initialize the array in memory
				await blob.FetchAttributesAsync();

				byte[] blobBytes = new byte[blob.Properties.Length];

				// Downloads the block blob and stores the content in an array in memory
				await blob.DownloadToByteArrayAsync(blobBytes, 0);

				return blobBytes;
			}

			return null;
		}

		public static async Task<byte[]> GetProfilePicture(string sid)
		{
			var container = GetProfilePictureContainer();

			//Gets the block blob representing the image
			var blob = container.GetBlobReference(sid);

			if (await blob.ExistsAsync())
			{
				// Gets the block blob length to initialize the array in memory
				await blob.FetchAttributesAsync();

				byte[] blobBytes = new byte[blob.Properties.Length];

				// Downloads the block blob and stores the content in an array in memory
				await blob.DownloadToByteArrayAsync(blobBytes, 0);

				return blobBytes;
			}

			return null;
		}

		/// <summary>
		/// Lists all the available item images in the blob container
		/// </summary>
		/// <returns></returns>
		public static async Task<string[]> ListItemImages(string itemId)
		{
			var container = GetContainer();

			// Iterates multiple times to get all the available blobs
			var allBlobs = new List<string>();
			BlobContinuationToken token = null;

			do
			{
				var result = await container.ListBlobsSegmentedAsync(token);
				if (result.Results.Count() > 0)
				{
					var blobs = result.Results.Cast<CloudBlockBlob>().Where(b => b.Name.StartsWith(itemId, StringComparison.Ordinal))
																	 .Select(b => b.Name);
					allBlobs.AddRange(blobs);
				}

				token = result.ContinuationToken;
			} while (token != null); // no more blobs to retrieve

			return allBlobs.ToArray();
		}


		public static string GenerateProfilePicName(string userId)
		{
			return (userId);
		}


		public static async Task<string> GenerateItemPhotoName(string itemId)
		{
			string[] currentItemPhotos = await ListItemImages(itemId);
			return itemId + "_" + currentItemPhotos.Length;
		}
	}
}
