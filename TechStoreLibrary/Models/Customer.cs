using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.FakerLoader;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines a customer.
    /// </summary>
    public class Customer : Person, IFakerLoader<Customer>
    {
        #region Attributes
        private decimal money;
        #endregion

        #region Properties
        /// <summary>
        /// Customer's amount of money to buy products.
        /// </summary>
        public decimal Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Customer()
        {
        }

        public Customer(string firstName, string lastName, Address address, decimal money)
            : base(firstName, lastName, address)
        {
            Money = money;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random Customer item;
        /// </summary>
        /// <returns></returns>
        public Customer LoadSingleItem()
        {
            Customer customer = new Customer();
            customer.FirstName = Faker.Name.FirstName();
            customer.LastName = Faker.Name.LastName();
            customer.Address = new Address().LoadSingleItem();
            customer.Money = Faker.Number.RandomNumber(50, 1200);

            return customer;
        }

        /// <summary>
        /// Generates a random list of Customer items;
        /// </summary>
        /// <returns></returns>
        public List<Customer> LoadMultipleItems()
        {
            List<Customer> customers = new List<Customer>();

            for (int i = 0; i < Faker.Number.RandomNumber(2, 11); i++)
            {
                customers.Add(LoadSingleItem());
            }

            return customers;
        }
        #endregion
    }
}
