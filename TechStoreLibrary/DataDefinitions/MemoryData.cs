using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines Memory data for items generation using Faker.
    /// </summary>
    public class MemoryData
    {
        #region Attributes
        private List<string> brands;
        private List<List<string>> names;
        private string[] types;
        private int[] capacities;
        private int[] speeds;
        #endregion

        #region Properties
        /// <summary>
        /// Memory manufacturers.
        /// </summary>
        public List<string> Brands
        {
            get
            {
                return brands;
            }
            set
            {
                brands = value;
            }
        }

        /// <summary>
        /// Memory names based on respective brands.
        /// </summary>
        public List<List<string>> Names
        {
            get
            {
                return names;
            }
            set
            {
                names = value;
            }
        }

        /// <summary>
        /// Memory types (DDR3, DDR4).
        /// </summary>
        public string[] Types
        {
            get
            {
                return types;
            }
            set
            {
                types = value;
            }
        }

        /// <summary>
        /// Memory standard capacities.
        /// </summary>
        public int[] Capacities
        {
            get
            {
                return capacities;
            }
            set
            {
                capacities = value;
            }
        }

        /// <summary>
        /// Memory operating frequencies.
        /// </summary>
        public int[] Speeds
        {
            get
            {
                return speeds;
            }
            set
            {
                speeds = value;
            }
        }
        #endregion

        #region Constructors
        public MemoryData()
        {
            Brands = new List<string>();
            Brands.Add("Corsair");
            Brands.Add("G.Skill");
            Brands.Add("Crucial");

            Names = new List<List<string>>();
            for (int i = 0; i < Brands.Count; i++)
            {
                List<string> memoryNames = new List<string>();

                switch (i)
                {
                    case 0: // Corsair
                        memoryNames.Add("Vengeance");
                        memoryNames.Add("Vengeance LPX");
                        memoryNames.Add("Vengeance Pro");
                        memoryNames.Add("Dominator Platinum");
                        break;
                    case 1: // G.Skill
                        memoryNames.Add("Aegis");
                        memoryNames.Add("RipJaws 4");
                        memoryNames.Add("RipJaws 5");
                        memoryNames.Add("Trident Z");
                        break;
                    case 2: // Crucial
                        memoryNames.Add("DR X8");
                        memoryNames.Add("QR X4");
                        memoryNames.Add("SR X4");
                        memoryNames.Add("DR X4");
                        break;
                    default:
                        break;
                }

                Names.Add(memoryNames);
            }

            Types = new string[] { "DDR3", "DDR4" };
            Capacities = new int[] { 1, 2, 4, 6, 8, 12, 16, 24, 32, 64, 128 };
            Speeds = new int[] { 2133, 2666, 2800, 3200, 3600, 3866, 4000 };
        }
        #endregion

        #region Methods

        #endregion
    }
}
