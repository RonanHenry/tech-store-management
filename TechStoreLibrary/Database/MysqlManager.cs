using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Utilities;

namespace TechStoreLibrary.Database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MysqlManager<TModel> : DbContext where TModel : class
    {
        #region Attributes

        #endregion

        #region Properties
        public DbSet<TModel> DbSetT { get; set; }
        #endregion

        #region Constructors
        public MysqlManager(ConnectionResource connectionResource)
            : base(EnumString.GetStringValue(connectionResource))
        {
            MysqlDbContext mysqlDbContext = new MysqlDbContext(connectionResource);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Inserts a single item in the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<TModel> Insert(TModel item)
        {
            DbSetT.Add(item);
            await SaveChangesAsync();
            return item;
        }

        /// <summary>
        /// Inserts multiple items in the database.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> Insert(IEnumerable<TModel> items)
        {
            foreach (var item in items)
            {
                DbSetT.Add(item);
            }
            await SaveChangesAsync();
            return items;
        }

        /// <summary>
        /// Gets an item from the database by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TModel> Get(int id)
        {
            return await DbSetT.FindAsync(id) as TModel;
        }

        /// <summary>
        /// Gets all items from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> Get()
        {
            DbSet<TModel> tmp = default(DbSet<TModel>);
            List<TModel> items = new List<TModel>();
            await Task.Factory.StartNew(() =>
            {
                tmp = base.Set<TModel>();
            });
            items.AddRange(tmp);
            return items;
        }

        /// <summary>
        /// Updates a single item in the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<TModel> Update(TModel item)
        {
            await Task.Factory.StartNew(() =>
            {
                Entry<TModel>(item).State = EntityState.Modified;
            });
            await SaveChangesAsync();
            return item;
        }

        /// <summary>
        /// Updates multiple items in the database.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> Update(IEnumerable<TModel> items)
        {
            await Task.Factory.StartNew(() =>
            {
                foreach (var item in items)
                {
                    Entry<TModel>(item).State = EntityState.Modified;
                }
            });
            await SaveChangesAsync();
            return items;
        }

        /// <summary>
        /// Deletes a single item from the database.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<int> Delete(TModel item)
        {
            await Task.Factory.StartNew(() =>
            {
                DbSetT.Attach(item);
                DbSetT.Remove(item);
            });
            return await SaveChangesAsync();
        }

        /// <summary>
        /// Deletes multiple items from the database.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task<int> Delete(IEnumerable<TModel> items)
        {
            await Task.Factory.StartNew(() =>
            {
                DbSetT.Attach((items as List<TModel>)[0]);
                DbSetT.RemoveRange(items);
            });
            return await SaveChangesAsync();
        }
        #endregion
    }
}
