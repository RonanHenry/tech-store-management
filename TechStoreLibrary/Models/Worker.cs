using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.FakerLoader;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines a worker.
    /// </summary>
    public class Worker : Person, IFakerLoader<Worker>
    {
        #region Attributes
        private string job;
        private string email;
        #endregion

        #region Properties
        /// <summary>
        /// Worker's job position.
        /// </summary>
        public string Job
        {
            get
            {
                return job;
            }
            set
            {
                job = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Worker's staff email address.
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Worker()
        {
        }

        public Worker(string firstName, string lastName, Address address, string job, string email)
            : base(firstName, lastName, address)
        {
            Job = job;
            Email = email;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random Worker item;
        /// </summary>
        /// <returns></returns>
        public Worker LoadSingleItem()
        {
            Worker worker = new Worker();
            worker.FirstName = Faker.Name.FirstName();
            worker.LastName = Faker.Name.LastName();
            worker.Address = new Address().LoadSingleItem();
            worker.Job = Faker.Name.FullName();
            worker.Email = string.Format("{0}.{1}@tech-store.com", worker.FirstName, worker.LastName);

            return worker;
        }

        /// <summary>
        /// Generates a random list of Worker items;
        /// </summary>
        /// <returns></returns>
        public List<Worker> LoadMultipleItems()
        {
            List<Worker> workers = new List<Worker>();

            for (int i = 0; i < Faker.Number.RandomNumber(2, 10); i++)
            {
                workers.Add(LoadSingleItem());
            }

            return workers;
        }
        #endregion
    }
}
