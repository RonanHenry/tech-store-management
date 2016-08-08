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
    /// Defines a GPU component (Graphical Processing Unit).
    /// </summary>
    public class GPU : Product, IFakerLoader<GPU>
    {
        #region Attributes
        private string chipsetMaker;
        private int memoryAmount;
        private bool vrReady;
        #endregion

        #region Properties
        /// <summary>
        /// GPU's chipset manufacturer (AMD or NVidia).
        /// </summary>
        public string ChipsetMaker
        {
            get
            {
                return chipsetMaker;
            }
            set
            {
                chipsetMaker = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// GPU's onboard video memory (in GB).
        /// </summary>
        public int MemoryAmount
        {
            get
            {
                return memoryAmount;
            }
            set
            {
                memoryAmount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Indicates if the card is VR capable or not.
        /// </summary>
        public bool VrReady
        {
            get
            {
                return vrReady;
            }
            set
            {
                vrReady = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public GPU()
        {
        }

        public GPU(string brand, string name, string description, string condition, decimal price, string chipsetMaker, int memoryAmount, bool vrReady)
            : base(brand, name, description, condition, price)
        {
            ChipsetMaker = chipsetMaker;
            MemoryAmount = memoryAmount;
            VrReady = vrReady;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random GPU item.
        /// </summary>
        /// <returns></returns>
        public GPU LoadSingleItem()
        {
            CommonData commonData = new CommonData();
            GPUData gpuData = new GPUData();
            int chipsetMakerIndex = Faker.Number.RandomNumber(0, gpuData.ChipsetMakers.Count);

            GPU gpu = new GPU();
            gpu.Brand = gpuData.Brands[Faker.Number.RandomNumber(0, gpuData.Brands.Count)];
            gpu.ChipsetMaker = gpuData.ChipsetMakers[chipsetMakerIndex];
            gpu.Name = gpuData.Names[chipsetMakerIndex][Faker.Number.RandomNumber(0, gpuData.Names[chipsetMakerIndex].Count)];
            gpu.Description = "Some GPU description";
            gpu.Condition = commonData.Conditions[Faker.Number.RandomNumber(0, commonData.Conditions.Length)];
            gpu.Price = Faker.Number.RandomNumber(60, 1020);
            gpu.MemoryAmount = gpuData.MemoryAmount[Faker.Number.RandomNumber(0, gpuData.MemoryAmount.Length)];
            gpu.VrReady = Faker.Number.Bool();

            return gpu;
        }

        /// <summary>
        /// Generates a list of random GPU items.
        /// </summary>
        /// <returns></returns>
        public List<GPU> LoadMultipleItems()
        {
            List<GPU> gpus = new List<GPU>();

            for (int i = 0; i < Faker.Number.RandomNumber(10, 51); i++)
            {
                gpus.Add(LoadSingleItem());
            }

            return gpus;
        }
        #endregion
    }
}
