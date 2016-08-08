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
    /// Defines a Power supply component.
    /// </summary>
    public class PSU : Product, IFakerLoader<PSU>
    {
        #region Attributes
        private string rating;
        private int power;
        private bool isModular;
        #endregion

        #region Properties
        /// <summary>
        /// Power supply's rating (80 PLUS level).
        /// </summary>
        public string Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Power supply's max power.
        /// </summary>
        public int Power
        {
            get
            {
                return power;
            }
            set
            {
                power = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Indicates if the power supply has detachable cables or not.
        /// </summary>
        public bool IsModular
        {
            get
            {
                return isModular;
            }
            set
            {
                isModular = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public PSU()
        {
        }

        public PSU(string brand, string name, string description, string condition, decimal price, string rating, int power, bool isModular)
            : base(brand, name, description, condition, price)
        {
            Rating = rating;
            Power = power;
            IsModular = isModular;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random Power supply item.
        /// </summary>
        /// <returns></returns>
        public PSU LoadSingleItem()
        {
            CommonData commonData = new CommonData();
            PSUData psuData = new PSUData();
            int brandIndex = Faker.Number.RandomNumber(0, psuData.Brands.Count);

            PSU psu = new PSU();
            psu.Brand = psuData.Brands[brandIndex];
            psu.Name = psuData.Names[brandIndex][Faker.Number.RandomNumber(0, psuData.Names[brandIndex].Count)];
            psu.Description = "Some power supply description";
            psu.Condition = commonData.Conditions[Faker.Number.RandomNumber(0, commonData.Conditions.Length)];
            psu.Price = Faker.Number.RandomNumber(50, 500);
            psu.Rating = psuData.Ratings[Faker.Number.RandomNumber(0, psuData.Ratings.Length)];
            psu.Power = psuData.Powers[Faker.Number.RandomNumber(0, psuData.Powers.Length)];
            psu.IsModular = Faker.Number.Bool();

            return psu;
        }

        /// <summary>
        /// Generates a list of random Power supply items.
        /// </summary>
        /// <returns></returns>
        public List<PSU> LoadMultipleItems()
        {
            List<PSU> psus = new List<PSU>();

            for (int i = 0; i < Faker.Number.RandomNumber(0, 11); i++)
            {
                psus.Add(LoadSingleItem());
            }

            return psus;
        }
        #endregion
    }
}
