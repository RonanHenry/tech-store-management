using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines workers data for items generation using Faker.
    /// </summary>
    public class WorkerData
    {
        #region Attributes
        private string[] jobs;
        #endregion

        #region Properties
        /// <summary>
        /// Worker jobs.
        /// </summary>
        public string[] Jobs
        {
            get
            {
                return jobs;
            }
            set
            {
                jobs = value;
            }
        }
        #endregion

        #region Constructors
        public WorkerData()
        {
            Jobs = new string[] { "Owner", "Seller", "Stock manager" };
        }
        #endregion

        #region Methods

        #endregion
    }
}
