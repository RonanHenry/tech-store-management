using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines common data for items generation using Faker.
    /// </summary>
    public class CommonData
    {
        #region Attributes
        private string[] conditions;
        #endregion

        #region Properties
        /// <summary>
        /// PC component conditions (New or Refurbished).
        /// </summary>
        public string[] Conditions
        {
            get
            {
                return conditions;
            }
            set
            {
                conditions = value;
            }
        }
        #endregion

        #region Constructors
        public CommonData()
        {
            Conditions = new string[] { "New", "Refurbished" };
        }
        #endregion

        #region Methods

        #endregion
    }
}
