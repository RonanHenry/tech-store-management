using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.DataDefinitions;
using TechStoreLibrary.FakerLoader;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines a Storage component (SSD or HDD).
    /// </summary>
    public class Storage : Product, IFakerLoader<Storage>
    {
        #region Attributes
        private string type;
        private string speed;
        private int capacity;
        #endregion

        #region Properties
        /// <summary>
        /// Storage's type (SSD or HDD).
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Storage's speed based on the type (Read/Write speeds in MO/s for an SSD and RPM for an HDD)
        /// </summary>
        public string Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Storage's capacity (in GB).
        /// </summary>
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Storage()
        {
        }

        public Storage(string brand, string name, string description, string condition, int stock, decimal price, string type, string speed, int capacity)
            : base(brand, name, description, condition, stock, price)
        {
            Type = type;
            Speed = speed;
            Capacity = capacity;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random Storage item.
        /// </summary>
        /// <returns></returns>
        public Storage LoadSingleItem()
        {
            CommonData commonData = new CommonData();
            StorageData storageData = new StorageData();
            int typeIndex = Faker.Number.RandomNumber(0, storageData.Types.Count);
            int brandIndex = Faker.Number.RandomNumber(0, storageData.Brands[typeIndex].Count);

            Storage storageComponent = new Storage();
            storageComponent.Type = storageData.Types[typeIndex];
            storageComponent.Brand = storageData.Brands[typeIndex][brandIndex];
            storageComponent.Name = storageData.Names[typeIndex][brandIndex][Faker.Number.RandomNumber(0, storageData.Names[typeIndex][brandIndex].Count)];
            storageComponent.Description = "Some storage description";
            storageComponent.Condition = commonData.Conditions[Faker.Number.RandomNumber(0, commonData.Conditions.Length)];
            storageComponent.Stock = Faker.Number.RandomNumber(0, 51);
            storageComponent.Price = Faker.Number.RandomNumber(40, 1100);
            storageComponent.Speed = storageData.Speeds[typeIndex][Faker.Number.RandomNumber(0, storageData.Speeds[typeIndex].Count)];
            storageComponent.Capacity = storageData.Capacities[Faker.Number.RandomNumber(0, storageData.Capacities.Length)];

            return storageComponent;
        }

        /// <summary>
        /// Generates a list of random Storage items.
        /// </summary>
        /// <returns></returns>
        public List<Storage> LoadMultipleItems()
        {
            List<Storage> storageComponents = new List<Storage>();

            for (int i = 0; i < Faker.Number.RandomNumber(10, 51); i++)
            {
                storageComponents.Add(LoadSingleItem());
            }

            return storageComponents;
        }
        #endregion
    }
}
