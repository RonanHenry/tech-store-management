using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines Storage data for items generation using Faker.
    /// </summary>
    public class StorageData
    {
        #region Attributes
        private List<string> types;
        private List<List<string>> brands;
        private List<List<List<string>>> names;
        private List<List<string>> speeds;
        private int[] capacities;
        #endregion

        #region Properties
        /// <summary>
        /// Storage types (SSD or HDD).
        /// </summary>
        public List<string> Types
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
        /// Storage manufacturers based on the type.
        /// </summary>
        public List<List<string>> Brands
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
        /// Storage component names based on the type and the manufacturer.
        /// </summary>
        public List<List<List<string>>> Names
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
        /// Storage speeds based on the type (Read/Write in Mo/s for SSDs and RPMs for HDDs).
        /// </summary>
        public List<List<string>> Speeds
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

        /// <summary>
        /// Storage capacities (in GB).
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
        #endregion

        #region Constructors
        public StorageData()
        {
            Types = new List<string>();
            Types.Add("SSD");
            Types.Add("HDD");

            Brands = new List<List<string>>();
            for (int i = 0; i < Types.Count; i++)
            {
                List<string> brandNames = new List<string>();

                switch (i)
                {
                    case 0: // SSD
                        brandNames.Add("Samsung");
                        brandNames.Add("Kingston");
                        brandNames.Add("Corsair");
                        brandNames.Add("Crucial");
                        break;
                    case 1: // HDD
                        brandNames.Add("Seagate Technology");
                        brandNames.Add("Western Digital");
                        brandNames.Add("Hitachi");
                        brandNames.Add("Lenovo");
                        break;
                    default:
                        break;
                }

                Brands.Add(brandNames);
            }

            Names = new List<List<List<string>>>();
            for (int i = 0; i < Types.Count; i++)
            {
                List<List<string>> storageTypeNames = new List<List<string>>();

                for (int j = 0; j < 4; j++)
                {
                    List<string> storageTypeBrandNames = new List<string>();

                    switch (i)
                    {
                        case 0: // SSD
                            switch (j)
                            {
                                case 0: // Samsung
                                    storageTypeBrandNames.Add("850 EVO");
                                    storageTypeBrandNames.Add("850 PRO");
                                    storageTypeBrandNames.Add("950 PRO M.2");
                                    storageTypeBrandNames.Add("PM863");
                                    break;
                                case 1: // Kingston
                                    storageTypeBrandNames.Add("UV400");
                                    storageTypeBrandNames.Add("KC380");
                                    storageTypeBrandNames.Add("KC400");
                                    storageTypeBrandNames.Add("SSDNow M.2");
                                    break;
                                case 2: // Corsair
                                    storageTypeBrandNames.Add("Force LE");
                                    storageTypeBrandNames.Add("Force LS");
                                    storageTypeBrandNames.Add("Neutron XT");
                                    storageTypeBrandNames.Add("Neutron XTi");
                                    break;
                                case 3: // Crucial
                                    storageTypeBrandNames.Add("BX100");
                                    storageTypeBrandNames.Add("M500");
                                    storageTypeBrandNames.Add("MX200");
                                    storageTypeBrandNames.Add("MX300");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 1: // HDD
                            switch (j)
                            {
                                case 0: // Seagate Technology
                                    storageTypeBrandNames.Add("Barracuda");
                                    storageTypeBrandNames.Add("Constellation");
                                    storageTypeBrandNames.Add("Desktop");
                                    storageTypeBrandNames.Add("Enterprise");
                                    break;
                                case 1: // Western Digital
                                    storageTypeBrandNames.Add("Black");
                                    storageTypeBrandNames.Add("Blue");
                                    storageTypeBrandNames.Add("Gold");
                                    storageTypeBrandNames.Add("Caviar");
                                    break;
                                case 2: // Hitachi
                                    storageTypeBrandNames.Add("Deskstar");
                                    storageTypeBrandNames.Add("Travelstar");
                                    storageTypeBrandNames.Add("Ultrastar 7K4000");
                                    storageTypeBrandNames.Add("Ultrastar He10");
                                    break;
                                case 3: // Lenovo
                                    storageTypeBrandNames.Add("ThinkServer");
                                    storageTypeBrandNames.Add("Simple-Swap");
                                    storageTypeBrandNames.Add("81Y9730");
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }

                    storageTypeNames.Add(storageTypeBrandNames);
                }

                Names.Add(storageTypeNames);
            }

            Speeds = new List<List<string>>();
            for (int i = 0; i < Types.Count; i++)
            {
                List<string> storageTypeSpeeds = new List<string>();

                switch (i)
                {
                    case 0: // SSD
                        storageTypeSpeeds.Add("Read: 540 / Write: 520");
                        storageTypeSpeeds.Add("Read: 550 / Write: 520");
                        storageTypeSpeeds.Add("Read: 2500 / Write: 1500");
                        break;
                    case 1: // HDD
                        storageTypeSpeeds.Add("5400 RPM");
                        storageTypeSpeeds.Add("7200 RPM");
                        storageTypeSpeeds.Add("10000 RPM");
                        break;
                    default:
                        break;
                }

                Speeds.Add(storageTypeSpeeds);
            }

            Capacities = new int[] { 128, 256, 512, 1000, 2000, 3000, 4000 };
        }
        #endregion

        #region Methods

        #endregion
    }
}
