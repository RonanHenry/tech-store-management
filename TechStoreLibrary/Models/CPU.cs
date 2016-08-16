using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.DataDefinitions;
using TechStoreLibrary.FakerLoader;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines a CPU component (Central Processing Unit).
    /// </summary>
    [Table("cpus")]
    public class CPU : Product, IFakerLoader<CPU>
    {
        #region Attributes
        private string socketType;
        private int coresAmount;
        private decimal frequency;
        #endregion

        #region Properties
        /// <summary>
        /// CPU's socket name.
        /// </summary>
        public string SocketType
        {
            get
            {
                return socketType;
            }
            set
            {
                socketType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// CPU's amount of cores.
        /// </summary>
        public int CoresAmount
        {
            get
            {
                return coresAmount;
            }
            set
            {
                coresAmount = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// CPU's operating frequency (in GHz).
        /// </summary>
        public decimal Frequency
        {
            get
            {
                return frequency;
            }
            set
            {
                frequency = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public CPU()
        {
        }

        public CPU(string brand, string name, string description, string condition, int stock, decimal price, string socketType, int coresAmount, decimal frequency)
            : base(brand, name, description, condition, stock, price)
        {
            SocketType = socketType;
            CoresAmount = coresAmount;
            Frequency = frequency;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random CPU item.
        /// </summary>
        /// <returns></returns>
        public CPU LoadSingleItem()
        {
            CommonData commonData = new CommonData();
            CPUData cpuData = new CPUData();
            int brandIndex = Faker.Number.RandomNumber(0, cpuData.Brands.Count);

            CPU cpu = new CPU();
            cpu.Brand = cpuData.Brands[brandIndex];
            cpu.Name = cpuData.Names[brandIndex][Faker.Number.RandomNumber(0, cpuData.Names[brandIndex].Count)];
            cpu.Description = "Some CPU description";
            cpu.Condition = commonData.Conditions[Faker.Number.RandomNumber(0, commonData.Conditions.Length)];
            cpu.Stock = Faker.Number.RandomNumber(0, 51);
            cpu.Price = Faker.Number.RandomNumber(30, 450);
            cpu.SocketType = cpuData.Sockets[brandIndex][Faker.Number.RandomNumber(0, cpuData.Sockets[brandIndex].Count)];
            cpu.CoresAmount = cpuData.CoresAmount[Faker.Number.RandomNumber(0, cpuData.CoresAmount.Length)];
            cpu.Frequency = Faker.Number.RandomNumber(1, 5);

            return cpu;
        }

        /// <summary>
        /// Generates a list of random CPU items.
        /// </summary>
        /// <returns></returns>
        public List<CPU> LoadMultipleItems()
        {
            List<CPU> cpus = new List<CPU>();

            for (int i = 0; i < Faker.Number.RandomNumber(10, 51); i++)
            {
                cpus.Add(LoadSingleItem());
            }

            return cpus;
        }
        #endregion
    }
}
