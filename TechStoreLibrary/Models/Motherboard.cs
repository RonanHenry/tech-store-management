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
    /// Defines a Motherboard component.
    /// </summary>
    public class Motherboard : Product, IFakerLoader<Motherboard>
    {
        #region Attributes
        private string platform;
        private string chipset;
        private string cpuSocket;
        private string usage;
        #endregion

        #region Properties
        /// <summary>
        /// Motherboard's platform (AMD or Intel).
        /// </summary>
        public string Platform
        {
            get
            {
                return platform;
            }
            set
            {
                platform = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Motherboard's chipset name.
        /// </summary>
        public string Chipset
        {
            get
            {
                return chipset;
            }
            set
            {
                chipset = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Motherboard's CPU socket.
        /// </summary>
        public string CPUSocket
        {
            get
            {
                return cpuSocket;
            }
            set
            {
                cpuSocket = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Motherboard's usage recommendation.
        /// </summary>
        public string Usage
        {
            get
            {
                return usage;
            }
            set
            {
                usage = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Motherboard()
        {
        }

        public Motherboard(string brand, string name, string description, string condition, decimal price, string platform, string chipset, string cpuSocket, string usage)
            : base(brand, name, description, condition, price)
        {
            Platform = platform;
            Chipset = chipset;
            CPUSocket = cpuSocket;
            Usage = usage;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random Motherboard item.
        /// </summary>
        /// <returns></returns>
        public Motherboard LoadSingleItem()
        {
            CommonData commonData = new CommonData();
            MotherboardData motherboardData = new MotherboardData();
            int brandIndex = Faker.Number.RandomNumber(0, motherboardData.Brands.Count);
            int platformIndex = Faker.Number.RandomNumber(0, motherboardData.Platforms.Count);

            Motherboard motherboard = new Motherboard();
            motherboard.Brand = motherboardData.Brands[brandIndex];
            motherboard.Platform = motherboardData.Platforms[platformIndex];
            motherboard.Name = motherboardData.Names[brandIndex][platformIndex][Faker.Number.RandomNumber(0, motherboardData.Names[brandIndex][platformIndex].Count)];
            motherboard.Description = "Some motherboard description";
            motherboard.Condition = commonData.Conditions[Faker.Number.RandomNumber(0, commonData.Conditions.Length)];
            motherboard.Price = Faker.Number.RandomNumber(50, 650);
            motherboard.Chipset = motherboardData.Chipsets[platformIndex][Faker.Number.RandomNumber(0, motherboardData.Chipsets[platformIndex].Count)];
            motherboard.CPUSocket = motherboardData.CPUSockets[platformIndex][Faker.Number.RandomNumber(0, motherboardData.CPUSockets[platformIndex].Count)];
            motherboard.Usage = motherboardData.Uses[Faker.Number.RandomNumber(0, motherboardData.Uses.Length)];

            return motherboard;
        }

        /// <summary>
        /// Generates a list of random Motherboard items.
        /// </summary>
        /// <returns></returns>
        public List<Motherboard> LoadMultipleItems()
        {
            List<Motherboard> motherboards = new List<Motherboard>();

            for (int i = 0; i < Faker.Number.RandomNumber(0, 11); i++)
            {
                motherboards.Add(LoadSingleItem());
            }

            return motherboards;
        }
        #endregion
    }
}
