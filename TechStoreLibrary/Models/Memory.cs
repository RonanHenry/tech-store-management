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
    /// Defines a Random Access Memory component (RAM).
    /// </summary>
    public class Memory : Product, IFakerLoader<Memory>
    {
        #region Attributes
        private string type;
        private int capacity;
        private int speed;
        private int latency;
        #endregion

        #region Properties
        /// <summary>
        /// Memory's type (DDR3 or DDR4).
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
        /// Memory's capacity (in GB).
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

        /// <summary>
        /// Memory's operating frequency (in MHz).
        /// </summary>
        public int Speed
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
        /// Memory's Column Access Strobe (CAS) latency.
        /// Represents the delay time between the moment a memory controller tells 
        /// the memory to access a particular momory column on a RAM module and the 
        /// moment the data from the given array location is available on the module's output pins.
        /// </summary>
        public int Latency
        {
            get
            {
                return latency;
            }
            set
            {
                latency = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Memory()
        {
        }

        public Memory(string brand, string name, string description, string condition, decimal price, string type, int speed, int latency)
            : base(brand, name, description, condition, price)
        {
            Type = type;
            Speed = speed;
            Latency = latency;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random Memory item.
        /// </summary>
        /// <returns></returns>
        public Memory LoadSingleItem()
        {
            CommonData commonData = new CommonData();
            MemoryData memoryData = new MemoryData();
            int brandIndex = Faker.Number.RandomNumber(0, memoryData.Brands.Count);

            Memory memory = new Memory();
            memory.Brand = memoryData.Brands[brandIndex];
            memory.Name = memoryData.Names[brandIndex][Faker.Number.RandomNumber(0, memoryData.Names[brandIndex].Count)];
            memory.Description = "Some memory description";
            memory.Condition = commonData.Conditions[Faker.Number.RandomNumber(0, commonData.Conditions.Length)];
            memory.Price = Faker.Number.RandomNumber(30, 800);
            memory.Type = memoryData.Types[Faker.Number.RandomNumber(0, memoryData.Types.Length)];
            memory.Capacity = memoryData.Capacities[Faker.Number.RandomNumber(0, memoryData.Capacities.Length)];
            memory.Speed = memoryData.Speeds[Faker.Number.RandomNumber(0, memoryData.Speeds.Length)];
            memory.Latency = Faker.Number.RandomNumber(2, 19);

            return memory;
        }

        /// <summary>
        /// Generates a list of random Memory items.
        /// </summary>
        /// <returns></returns>
        public List<Memory> LoadMultipleItems()
        {
            List<Memory> memories = new List<Memory>();

            for (int i = 0; i < Faker.Number.RandomNumber(10, 51); i++)
            {
                memories.Add(LoadSingleItem());
            }

            return memories;
        }
        #endregion
    }
}
