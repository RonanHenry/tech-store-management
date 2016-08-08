using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.FakerLoader;
using TechStoreLibrary.Models.Base;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines an address.
    /// </summary>
    public class Address : BaseModel, IFakerLoader<Address>
    {
        #region Attributes
        private int id;
        private string street;
        private string zipCode;
        private string city;
        private string country;
        #endregion

        #region Properties
        /// <summary>
        /// Address' id.
        /// </summary>
        [Key]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Address' street name.
        /// </summary>
        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                street = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Address' ZIP code.
        /// </summary>
        public string ZipCode
        {
            get
            {
                return zipCode;
            }
            set
            {
                zipCode = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Address' city.
        /// </summary>
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Address' country.
        /// </summary>
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Address()
        {
        }

        public Address(string street, string zipCode, string city, string country)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random Address item.
        /// </summary>
        /// <returns></returns>
        public Address LoadSingleItem()
        {
            Address address = new Address();
            address.Street = Faker.Address.StreetName();
            address.ZipCode = Faker.Address.USZipCode();
            address.City = Faker.Address.USCity();
            address.Country = Faker.Address.Country();

            return address;
        }

        /// <summary>
        /// Generates a list of random Address items.
        /// </summary>
        /// <returns></returns>
        public List<Address> LoadMultipleItems()
        {
            List<Address> addresses = new List<Address>();

            for (int i = 0; i < Faker.Number.RandomNumber(2, 11); i++)
            {
                addresses.Add(LoadSingleItem());
            }

            return addresses;
        }
        #endregion
    }
}
