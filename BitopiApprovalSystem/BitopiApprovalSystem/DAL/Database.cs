using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.Threading.Tasks;


namespace BitopiApprovalSystem.DAL
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
    public static class FileHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }
    }
    public class BitopiDatabase
    {
        readonly SQLiteAsyncConnection db;

        public BitopiDatabase(string Path)
        {
            db = new SQLiteAsyncConnection(Path);
            db.CreateTableAsync<RecentPR>().Wait();
            db.CreateTableAsync<RecentHistory>().Wait();
        }
        public Task<List<RecentPR>> RecentPRs
        {
            get
            {
                return db.Table<RecentPR>().ToListAsync();
            }
        }
        public Task<RecentHistory> RecentHistory
        {
            get
            {
                return db.Table<RecentHistory>().FirstOrDefaultAsync();
            }
        }
        public Task<int> SaveRecentHistory(RecentHistory item)

        {
            if (item.ID == 0)
            {
                return db.InsertAsync(item);
            }
            else
            {
                return db.UpdateAsync(item);
            }
        }
        public Task<int> SaveRecentPR(RecentPR item)
        {
            if (item.ID == 0)
            {
                return db.InsertAsync(item);
            }
            else
            {
                return db.UpdateAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(RecentPR item)
        {
            return db.DeleteAsync(item);
        }
        
    }
    public class DBAccess
    {
        static BitopiDatabase database;

        public static BitopiDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new BitopiDatabase(FileHelper.GetLocalFilePath("bitopiDB.db3"));
                }
                return database;
            }


        }
    }
}