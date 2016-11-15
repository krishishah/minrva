/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
//#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
#endif

namespace minrva
{
	public partial class TableManager
	{
		static TableManager defaultInstance = new TableManager();
		MobileServiceClient client;

#if OFFLINE_SYNC_ENABLED
        IMobileServiceSyncTable<User> userTable;
#else
		IMobileServiceTable<User> userTable;
		IMobileServiceTable<Boardgames> boardgamesTable;
		IMobileServiceTable<Request> requestTable;
		IMobileServiceTable<Message> messageTable;
		IMobileServiceTable<Chat> chatTable;
		IMobileServiceTable<Ratings> ratingsTable;


#endif

		const string offlineDbPath = @"localstore.db";

		private TableManager()
		{
			this.client = new MobileServiceClient(Constants.ApplicationURL);

#if OFFLINE_SYNC_ENABLED
            var store = new MobileServiceSQLiteStore(offlineDbPath);
            store.DefineTable<User>();

            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);

            this.userTable = client.GetSyncTable<User>();
#else
			this.userTable = client.GetTable<User>();
			this.boardgamesTable = client.GetTable<Boardgames>();
			this.requestTable = client.GetTable<Request>();
			this.messageTable = client.GetTable<Message>();
			this.chatTable = client.GetTable<Chat>();
			this.ratingsTable = client.GetTable<Ratings>();
#endif
		}

		public static TableManager DefaultManager
		{
			get
			{
				return defaultInstance;
			}
			private set
			{
				defaultInstance = value;
			}
		}

		public MobileServiceClient CurrentClient
		{
			get { return client; }
		}

		public bool IsOfflineEnabled
		{
			get { return userTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<User>; }
		}

		public async Task<ObservableCollection<Ratings>> GetRatingsAsync(bool syncItems = false)
		{
			try
			{
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
				IEnumerable<Ratings> items = await ratingsTable
					.ToEnumerableAsync();

				return new ObservableCollection<Ratings>(items);
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task<ObservableCollection<Chat>> GetChatAsync(bool syncItems = false)
		{
			try
			{
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
				IEnumerable<Chat> items = await chatTable
					.ToEnumerableAsync();

				return new ObservableCollection<Chat>(items);
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task<ObservableCollection<User>> GetUserAsync(bool syncItems = false)
		{
			try
			{
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
				IEnumerable<User> items = await userTable
					.ToEnumerableAsync();

				return new ObservableCollection<User>(items);
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task<ObservableCollection<Boardgames>> GetBoardgamesAsync(bool syncItems = false)
		{
			try
			{
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
				IEnumerable<Boardgames> items = await boardgamesTable
					.ToEnumerableAsync();

				return new ObservableCollection<Boardgames>(items);
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task<ObservableCollection<Request>> GetRequestAsync(bool syncItems = false)
		{
			try
			{
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
				IEnumerable<Request> items = await requestTable
					.ToEnumerableAsync();

				return new ObservableCollection<Request>(items);
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task<ObservableCollection<Message>> GetMessageAsync(bool syncItems = false)
		{
			try
			{
#if OFFLINE_SYNC_ENABLED
                if (syncItems)
                {
                    await this.SyncAsync();
                }
#endif
				IEnumerable<Message> items = await messageTable
					.ToEnumerableAsync();

				return new ObservableCollection<Message>(items);
			}
			catch (MobileServiceInvalidOperationException msioe)
			{
				Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"Sync error: {0}", e.Message);
			}
			return null;
		}

		public async Task SaveRatingsAsync(Ratings item)
		{
			if (item.Id == null)
			{
				await ratingsTable.InsertAsync(item);
			}
			else
			{
				await ratingsTable.UpdateAsync(item);
			}
		}

		public async Task SaveUserAsync(User item)
		{
			if (item.Id == null)
			{
				await userTable.InsertAsync(item);
			}
			else
			{
				await userTable.UpdateAsync(item);
			}
		}

		public async Task SaveMessageAsync(Message item)
		{
			if (item.Id == null)
			{
				await messageTable.InsertAsync(item);
			}
			else
			{
				await messageTable.UpdateAsync(item);
			}
		}

		public async Task SaveChatAsync(Chat item)
		{
			if (item.Id == null)
			{
				await chatTable.InsertAsync(item);
			}
			else
			{
				await chatTable.UpdateAsync(item);
			}
		}

		public async Task SaveBoardgamesAsync(Boardgames item)
		{
			if (item.Id == null)
			{
				await boardgamesTable.InsertAsync(item);
			}
			else
			{
				await boardgamesTable.UpdateAsync(item);
			}
		}

		public async Task SaveRequestAsync(Request item)
		{
			if (item.Id == null)
			{
				await requestTable.InsertAsync(item);
			}
			else
			{
				await requestTable.UpdateAsync(item);
			}
		}

		public async Task DeleteRatingsAsync(Ratings item)
		{
			await ratingsTable.DeleteAsync(item);
		}

		public async Task DeleteUserAsync(User item)
		{
			await userTable.DeleteAsync(item);
		}

		public async Task DeleteMessageAsync(Message item)
		{
			await messageTable.DeleteAsync(item);
		}

		public async Task DeleteChatAsync(Chat item)
		{
			await chatTable.DeleteAsync(item);
		}

		public async Task DeleteBoardgamesAsync(Boardgames item)
		{
			await boardgamesTable.DeleteAsync(item);
		}

		public async Task DeleteRequestAsync(Request item)
		{
			await requestTable.DeleteAsync(item);
		}

#if OFFLINE_SYNC_ENABLED
        public async Task SyncAsync()
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;

            try
            {
                await this.client.SyncContext.PushAsync();

                await this.userTable.PullAsync(
                    //The first parameter is a query name that is used internally by the client SDK to implement incremental sync.
                    //Use a different query name for each unique query in your program
                    "allUser",
                    this.userTable.CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }
#endif
	}
}

