using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines Power supply data for items generation using Faker.
    /// </summary>
    public class PSUData
    {
        #region Attributes
        private List<string> brands;
        private List<List<string>> names;
        private string[] ratings;
        private int[] powers;
        #endregion

        #region Properties
        /// <summary>
        /// Power supply manufacturers.
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
        /// Power supply names based on respective brands.
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
        /// Power supply ratings (80 PLUS)
        /// </summary>
        public string[] Ratings
        {
            get
            {
                return ratings;
            }
            set
            {
                ratings = value;
            }
        }

        /// <summary>
        /// Power supply powers (in W).
        /// </summary>
        public int[] Powers
        {
            get
            {
                return powers;
            }
            set
            {
                powers = value;
            }
        }
        #endregion

        #region Constructors
        public PSUData()
        {
            Brands = new List<string>();
            Brands.Add("Corsair");
            Brands.Add("Be Quiet!");
            Brands.Add("Silverstone");
            Brands.Add("Zalman");

            Names = new List<List<string>>();
            for (int i = 0; i < Brands.Count; i++)
            {
                List<string> psuNames = new List<string>();

                switch (i)
                {
                    case 0: // Corsair
                        psuNames.Add("AX860i");
                        psuNames.Add("CS850");
                        psuNames.Add("HX850i");
                        psuNames.Add("AX1500i");
                        break;
                    case 1: // Be Quiet!
                        psuNames.Add("Dark Power Pro 11");
                        psuNames.Add("Power Zone");
                        psuNames.Add("Pure Power 9");
                        psuNames.Add("SFX Power 2");
                        break;
                    case 2: // Silverstone
                        psuNames.Add("SFX ST45DF-G v2.0");
                        psuNames.Add("SFX SX-600-G");
                        psuNames.Add("SFX-L SX500-LG");
                        psuNames.Add("Strider ST45SF");
                        break;
                    case 3: // Zalman
                        psuNames.Add("ZM1000-EBT");
                        psuNames.Add("ZM850-EBT");
                        psuNames.Add("ZM700-GV");
                        psuNames.Add("ZM700-TX");
                        break;
                    default:
                        break;
                }

                Names.Add(psuNames);
            }

            Ratings = new string[] {
                "None",
                "80 PLUS",
                "80 PLUS Bronze",
                "80 PLUS Silver",
                "80 PLUS Gold",
                "80 PLUS Platinum",
                "80 PLUS Titanium"
            };

            Powers = new int[] { 400, 600, 760, 850, 860, 1000, 1200, 1500 };
        }
        #endregion

        #region Methods

        #endregion
    }
}
