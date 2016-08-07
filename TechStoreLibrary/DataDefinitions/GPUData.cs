using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines GPU data for items generation using Faker.
    /// </summary>
    public class GPUData
    {
        #region Attributes
        private List<string> brands;
        private List<string> chipsetMakers;
        private List<List<string>> names;
        private int[] memoryAmount;
        #endregion

        #region Properties
        /// <summary>
        /// GPU manufacturers.
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
        /// GPU chipset manufacturers.
        /// </summary>
        public List<string> ChipsetMakers
        {
            get
            {
                return chipsetMakers;
            }
            set
            {
                chipsetMakers = value;
            }
        }

        /// <summary>
        /// GPU names based on respective chipset manufacturers.
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
        /// GPU onboard video memory.
        /// </summary>
        public int[] MemoryAmount
        {
            get
            {
                return memoryAmount;
            }
            set
            {
                memoryAmount = value;
            }
        }
        #endregion

        #region Constructors
        public GPUData()
        {
            Brands = new List<string>();
            Brands.Add("Asus");
            Brands.Add("MSI");
            Brands.Add("Sapphire");
            Brands.Add("Gigabyte");

            ChipsetMakers = new List<string>();
            ChipsetMakers.Add("AMD");
            ChipsetMakers.Add("NVidia");

            Names = new List<List<string>>();
            for (int i = 0; i < ChipsetMakers.Count; i++)
            {
                List<string> gpuNames = new List<string>();

                switch (i)
                {
                    case 0: // AMD
                        gpuNames.Add("Radeon HD 6950");
                        gpuNames.Add("Radeon R9 380X");
                        gpuNames.Add("Radeon R7 360");
                        gpuNames.Add("Radeon R9 Fury X");
                        break;
                    case 1: // NVidia
                        gpuNames.Add("Geforce GTX 970");
                        gpuNames.Add("Geforce GTX 1070");
                        gpuNames.Add("Geforce GTX 1080");
                        gpuNames.Add("Geforce GTX Titan X");
                        break;
                    default:
                        break;
                }

                Names.Add(gpuNames);
            }

            MemoryAmount = new int[] { 1, 2, 3, 4, 8, 16 };
        }
        #endregion

        #region Methods

        #endregion
    }
}
