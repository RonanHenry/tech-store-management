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
    /// Defines a worker.
    /// </summary>
    [Table("workers")]
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
            WorkerData workerData = new WorkerData();

            Worker worker = new Worker();
            worker.FirstName = Faker.Name.FirstName();
            worker.LastName = Faker.Name.LastName();
            worker.Address = new Address().LoadSingleItem();
            worker.Job = workerData.Jobs[Faker.Number.RandomNumber(0, workerData.Jobs.Length)];
            worker.Email = string.Format("{0}.{1}@tech-store.com", worker.FirstName.ToLower(), worker.LastName.ToLower());

            return worker;
        }

        /// <summary>
        /// Generates a random list of Worker items;
        /// </summary>
        /// <returns></returns>
        public List<Worker> LoadMultipleItems()
        {
            List<Worker> workers = new List<Worker>();

            for (int i = 0; i < Faker.Number.RandomNumber(10, 51); i++)
            {
                workers.Add(LoadSingleItem());
            }

            return workers;
        }
        #endregion
    }
}
