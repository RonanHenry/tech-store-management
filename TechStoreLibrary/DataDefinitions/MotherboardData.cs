using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines Motherboard data for items generation using Faker.
    /// </summary>
    public class MotherboardData
    {
        #region Attributes
        private List<string> brands;
        private List<string> platforms;
        private List<List<List<string>>> names;
        private List<List<string>> chipsets;
        private List<List<string>> cpuSockets;
        private string[] uses;
        #endregion

        #region Properties
        /// <summary>
        /// Motherboard manufacturers.
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
        /// Motherboard platforms (AMD or Intel).
        /// </summary>
        public List<string> Platforms
        {
            get
            {
                return platforms;
            }
            set
            {
                platforms = value;
            }
        }

        /// <summary>
        /// Motherboard names based on respective brands.
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
        /// Motherboard chipset names.
        /// </summary>
        public List<List<string>> Chipsets
        {
            get
            {
                return chipsets;
            }
            set
            {
                chipsets = value;
            }
        }

        /// <summary>
        /// Motherboard CPU socket names.
        /// </summary>
        public List<List<string>> CPUSockets
        {
            get
            {
                return cpuSockets;
            }
            set
            {
                cpuSockets = value;
            }
        }

        /// <summary>
        /// Motheboard use cases (Office, Multimedia, Gaming, Server).
        /// </summary>
        public string[] Uses
        {
            get
            {
                return uses;
            }
            set
            {
                uses = value;
            }
        }
        #endregion

        #region Constructors
        public MotherboardData()
        {
            Brands = new List<string>();
            Brands.Add("ASRock");
            Brands.Add("Asus");
            Brands.Add("Gigabyte");
            Brands.Add("MSI");

            Platforms = new List<string>();
            Platforms.Add("AMD");
            Platforms.Add("Intel");

            Names = new List<List<List<string>>>();
            for (int i = 0; i < Brands.Count; i++)
            {
                List<List<string>> motherboardNames = new List<List<string>>();

                for (int j = 0; j < Platforms.Count; j++)
                {
                    List<string> motherboardPlatformNames = new List<string>();

                    switch (i)
                    {
                        case 0: // ASRock
                            if (j == 0) // AMD
                            {
                                motherboardPlatformNames.Add("990FX Extreme 3");
                                motherboardPlatformNames.Add("FM2A88X Extreme4+");
                            }
                            else // Intel
                            {
                                motherboardPlatformNames.Add("Fatal1ty X99 Professional");
                                motherboardPlatformNames.Add("Z170 Extreme6+");
                            }
                            break;
                        case 1: // Asus
                            if (j == 0) // AMD
                            {
                                motherboardPlatformNames.Add("Sabertooth 990FX R2.0");
                                motherboardPlatformNames.Add("Crosshair V Formula Z");
                            }
                            else // Intel
                            {
                                motherboardPlatformNames.Add("X99-Deluxe II");
                                motherboardPlatformNames.Add("Z170 Premium");
                            }
                            break;
                        case 2: // Gigabyte
                            if (j == 0) // AMD
                            {
                                motherboardPlatformNames.Add("GA-970A-UD3P");
                                motherboardPlatformNames.Add("GA-F2A88XM-D3H");
                            }
                            else // Intel
                            {
                                motherboardPlatformNames.Add("GA-X99-UD4P");
                                motherboardPlatformNames.Add("GA-Z170X-UD3");
                            }
                            break;
                        case 3: // MSI
                            if (j == 0) // AMD
                            {
                                motherboardPlatformNames.Add("970A SLI Krait Edition");
                                motherboardPlatformNames.Add("A88XM-E35 V2");
                            }
                            else // Intel
                            {
                                motherboardPlatformNames.Add("X99A XPower AC");
                                motherboardPlatformNames.Add("Z170A Tomahawk");
                            }
                            break;
                        default:
                            break;
                    }

                    motherboardNames.Add(motherboardPlatformNames);
                }
                
                Names.Add(motherboardNames);
            }

            Chipsets = new List<List<string>>();
            for (int i = 0; i < Platforms.Count; i++)
            {
                List<string> chipsetNames = new List<string>();

                switch (i)
                {
                    case 0: // AMD
                        chipsetNames.Add("990FX");
                        chipsetNames.Add("A88X");
                        chipsetNames.Add("970");
                        chipsetNames.Add("990X");
                        break;
                    case 1: // Intel
                        chipsetNames.Add("H87 Express");
                        chipsetNames.Add("Z97 Express");
                        chipsetNames.Add("Z170 Express");
                        chipsetNames.Add("X99 Express");
                        break;
                    default:
                        break;
                }

                Chipsets.Add(chipsetNames);
            }

            CPUSockets = new List<List<string>>();
            for (int i = 0; i < Platforms.Count; i++)
            {
                List<string> cpuSocketNames = new List<string>();

                switch (i)
                {
                    case 0: // AMD
                        cpuSocketNames.Add("AM2+");
                        cpuSocketNames.Add("AM3");
                        cpuSocketNames.Add("AM3+");
                        cpuSocketNames.Add("FM2+");
                        break;
                    case 1: // Intel
                        cpuSocketNames.Add("1150");
                        cpuSocketNames.Add("1155");
                        cpuSocketNames.Add("1366");
                        cpuSocketNames.Add("2011");
                        break;
                    default:
                        break;
                }

                CPUSockets.Add(cpuSocketNames);
            }

            Uses = new string[] { "Office", "Multimedia", "Gaming", "Server" };
        }
        #endregion

        #region Methods

        #endregion
    }
}
