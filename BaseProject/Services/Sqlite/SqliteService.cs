using BaseProject.Helpers;
using System;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync.Extensions;
using BaseProject.Services.FileSystem;
using System.Linq.Expressions;

namespace BaseProject.Services.Sqlite
{   
    public class SqliteService : ISqliteService
    {
        #region Properties
            private readonly IFileSystem _fileSystem;
            private static readonly AsyncLock Lock = new AsyncLock();
            private SQLiteAsyncConnection _sqlCon;
        #endregion

        #region Constructor
        public SqliteService(IFileSystem filesystem)
        {
            _fileSystem = filesystem;
            _sqlCon = CreateDatabaseConnection();
        }
        #endregion 

        #region Methods
        private SQLiteAsyncConnection CreateDatabaseConnection()
        {
            var databasePath = _fileSystem.GetFilePath(Constants.AppConstants.DatabaseName);
            return new SQLiteAsyncConnection(databasePath);
        }

        private async void CreateDatabase()
        {
            // await _sqlCon.CreateTableAsync<yourModel>(CreateFlags.None).ConfigureAwait(false);
        }

        public async Task CreateDatabaseTables(List<Type> tables)
        {
            foreach (var item in tables)
            {
                try
                {
                    await _sqlCon.CreateTableAsync(item, CreateFlags.None).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }            
            }
        }

        public async Task<List<SQLiteConnection.ColumnInfo>> GetTableInfo(string tableName)
        {
            var items = await _sqlCon.GetTableInfoAsync(tableName);
            return items;
        }

        
        public async Task<T> GetOne<T>(Expression<Func<T, bool>> query) where T : class, new()
        {
            try
            {
                var data = await _sqlCon.Table<T>().Where(query).FirstOrDefaultAsync();
                return data;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
        
        public async Task<List<T>> GetAll<T>(Expression<Func<T, bool>> query) where T : new()
        {
            try
            {
                var data = await _sqlCon.Table<T>().Where(query).ToListAsync().ConfigureAwait(false);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        public async Task<List<T>> GetAll<T>() where T : class,new()
        {
            var items = await _sqlCon.GetAllWithChildrenAsync<T>().ConfigureAwait(false);
            return items;
        }
        
        public async Task<int> Insert(object item) 
        {
            try
            {
                var Result = await _sqlCon.InsertAsync(item).ConfigureAwait(false);
                return Result;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        
        public async Task<int> InsertAll<T>(List<T> items) where T : new()
        {
            var count = await _sqlCon.InsertAllAsync(items).ConfigureAwait(false);
            return count;
        }

        
        public async Task<int> InsertOrReplaceOne<T>(object item) where T : new()
        {
            var count = await _sqlCon.InsertOrReplaceAsync(item);
            return count;
        }

        
        public async Task InsertOrReplaceAll<T>(List<T> items) where T : new()
        {
            await _sqlCon.InsertOrReplaceAllWithChildrenAsync(items);
        }

        
        public async Task<int> Update(object item) 
        {
            var count = await _sqlCon.UpdateAsync(item).ConfigureAwait(false);
            return count;
        }

        
        public async Task<int> UpdateAll<T>(List<T> items) where T : new()
        {
            var count = await _sqlCon.UpdateAllAsync(items).ConfigureAwait(false);
            return count;
        }

        
        public async Task<int> Delete(object item)
        {
            var count = await _sqlCon.DeleteAsync(item).ConfigureAwait(false);
            return count;
        }

        
        public async Task<int> DeleteAll<T>() where T : new()
        {
            var count = await _sqlCon.DeleteAllAsync<T>().ConfigureAwait(false);
            return count;
        }

        
        public async Task DeleteAll<T>(List<T> items) where T : new()
        {
            await _sqlCon.DeleteAllAsync(items);
        }

        
        public async Task<IList<T>> QueryString<T>(string sql) where T : class,new()
        {
            var items = await _sqlCon.QueryAsync<T>(sql, new object[0]);
            return items;
        }

        
        public async Task<int> DropTableAsync<T>() where T : new()
        {
            var count = await _sqlCon.DropTableAsync<T>();
            return count;
        }

        
        public async Task<int> ExecuteAsync(string sqlQuery)
        {
            try
            {
                var item = await _sqlCon.ExecuteAsync(sqlQuery);
                return item;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        #endregion
    }
}
