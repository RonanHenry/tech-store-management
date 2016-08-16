using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Models;
using TechStoreLibrary.Utilities;

namespace TechStoreLibrary.Database
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class MysqlDbContext : DbContext
    {
        #region Attributes

        #endregion

        #region Properties
        public DbSet<Address> DbSetAddress { get; set; }
        public DbSet<Worker> DbSetWorkers { get; set; }
        public DbSet<Customer> DbSetCustomers { get; set; }
        public DbSet<Case> DbSetCases { get; set; }
        public DbSet<CPU> DbSetCPUs { get; set; }
        public DbSet<GPU> DbSetGPUs { get; set; }
        public DbSet<Memory> DbSetMemories { get; set; }
        public DbSet<Motherboard> DbSetMotherboards { get; set; }
        public DbSet<PSU> DbSetPSUs { get; set; }
        public DbSet<Storage> DbSetStorages { get; set; }
        public DbSet<CartItem> DbSetCartItems { get; set; }
        public DbSet<Cart> DbSetCarts { get; set; }
        #endregion

        #region Constructors
        public MysqlDbContext(ConnectionResource connectionResource)
            : base(EnumString.GetStringValue(connectionResource))
        {
            switch (connectionResource)
            {
                case ConnectionResource.APIMYSQL:
                    InitDatabaseAsync();
                    break;
                case ConnectionResource.LOCALMYSQL:
                    InitDatabaseAsync();
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates the database and fills it with initial data.
        /// </summary>
        public async void InitDatabaseAsync()
        {
            if (Database.CreateIfNotExists())
            {
                DbSetWorkers.AddRange(new Worker().LoadMultipleItems());
                DbSetCustomers.AddRange(new Customer().LoadMultipleItems());
                DbSetCases.AddRange(new Case().LoadMultipleItems());
                DbSetCPUs.AddRange(new CPU().LoadMultipleItems());
                DbSetGPUs.AddRange(new GPU().LoadMultipleItems());
                DbSetMemories.AddRange(new Memory().LoadMultipleItems());
                DbSetMotherboards.AddRange(new Motherboard().LoadMultipleItems());
                DbSetPSUs.AddRange(new PSU().LoadMultipleItems());
                DbSetStorages.AddRange(new Storage().LoadMultipleItems());
                await SaveChangesAsync();
            }
        }
        #endregion
    }
}
