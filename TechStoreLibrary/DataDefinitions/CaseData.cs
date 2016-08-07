using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines cases data for items generation using Faker.
    /// </summary>
    public class CaseData
    {
        #region Attributes
        private List<string> brands;
        private List<List<string>> names;
        private string[] types;
        private string[] frontIOs;
        #endregion

        #region Properties
        /// <summary>
        /// PC cases manufacturers.
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
        /// PC Cases names based on respective manufacturers.
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
        /// PC cases types (Mini tower, Mid tower or Full tower).
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
        /// PC cases front ports (USB, Audio, e-SATA).
        /// </summary>
        public string[] FrontIOs
        {
            get
            {
                return frontIOs;
            }
            set
            {
                frontIOs = value;
            }
        }
        #endregion

        #region Constructors
        public CaseData()
        {
            Brands = new List<string>();
            Brands.Add("Corsair");
            Brands.Add("Cooler Master");
            Brands.Add("NZXT");
            Brands.Add("Fractal Design");
            Brands.Add("Silverstone");

            Names = new List<List<string>>();
            for (int i = 0; i < Brands.Count; i++)
            {
                List<string> brandNames = new List<string>();

                switch (i)
                {
                    case 0: // Corsair
                        brandNames.Add("Obsidian 900D");
                        brandNames.Add("Obsidian 750D");
                        brandNames.Add("Graphite 230T");
                        brandNames.Add("Carbide 330R");
                        break;
                    case 1: // Cooler Master
                        brandNames.Add("MasterCase Pro 5");
                        brandNames.Add("N200");
                        brandNames.Add("HAF 912");
                        brandNames.Add("Elite 130");
                        break;
                    case 2: // NZXT
                        brandNames.Add("Phantom 240");
                        brandNames.Add("Manta");
                        brandNames.Add("H440");
                        brandNames.Add("Source 210");
                        break;
                    case 3: // Fractal Design
                        brandNames.Add("Define R5");
                        brandNames.Add("Arc Midi R2");
                        brandNames.Add("Node 304");
                        brandNames.Add("Define S");
                        break;
                    case 4: // Silverstone
                        brandNames.Add("Raven RVZ02B");
                        brandNames.Add("Sugo SG05BB-LITE");
                        brandNames.Add("Milo ML08B-H");
                        brandNames.Add("Fortress FT03B");
                        break;
                    default:
                        break;
                }

                Names.Add(brandNames);
            }

            Types = new string[] { "Mini tower", "Mid tower", "Full tower" };
            FrontIOs = new string[] { "USB / Audio", "USB / Audio / e-SATA" };
        }
        #endregion

        #region Methods

        #endregion
    }
}
