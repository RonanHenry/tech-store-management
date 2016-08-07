using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.DataDefinitions
{
    /// <summary>
    /// Defines CPU data for items generation using Faker.
    /// </summary>
    public class CPUData
    {
        #region Attributes
        private List<string> brands;
        private List<List<string>> names;
        private List<List<string>> sockets;
        private int[] coresAmount;
        #endregion

        #region Properties
        /// <summary>
        /// CPU manufacturers.
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
        /// CPU names based on respective manufacturers.
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
        /// CPU sockets based on respective manufacturers.
        /// </summary>
        public List<List<string>> Sockets
        {
            get
            {
                return sockets;
            }
            set
            {
                sockets = value;
            }
        }

        /// <summary>
        /// CPU's normalized amount of cores.
        /// </summary>
        public int[] CoresAmount
        {
            get
            {
                return coresAmount;
            }
            set
            {
                coresAmount = value;
            }
        }
        #endregion

        #region Constructors
        public CPUData()
        {
            Brands = new List<string>();
            Brands.Add("AMD");
            Brands.Add("Intel");

            Names = new List<List<string>>();
            for (int i = 0; i < Brands.Count; i++)
            {
                List<string> cpuNames = new List<string>();

                switch (i)
                {
                    case 0: // AMD
                        cpuNames.Add("FX-8350");
                        cpuNames.Add("FX-6300");
                        cpuNames.Add("A6-7400K");
                        cpuNames.Add("A8-7600");
                        break;
                    case 1: // Intel
                        cpuNames.Add("Core i3-6100");
                        cpuNames.Add("Core i5-6500");
                        cpuNames.Add("Core i5-6600K");
                        cpuNames.Add("Core i7-6700K");
                        break;
                    default:
                        break;
                }

                Names.Add(cpuNames);
            }

            Sockets = new List<List<string>>();
            for (int i = 0; i < Brands.Count; i++)
            {
                List<string> cpuSockets = new List<string>();

                switch (i)
                {
                    case 0: // AMD
                        cpuSockets.Add("AM1");
                        cpuSockets.Add("AM3");
                        cpuSockets.Add("AM3+");
                        cpuSockets.Add("FM2+");
                        break;
                    case 1: // Intel
                        cpuSockets.Add("LGA 1150");
                        cpuSockets.Add("LGA 1151");
                        cpuSockets.Add("LGA 1155");
                        cpuSockets.Add("LGA 2011");
                        break;
                    default:
                        break;
                }

                Sockets.Add(cpuSockets);
            }

            CoresAmount = new int[] { 2, 3, 4, 6, 8 };
        }
        #endregion

        #region Methods

        #endregion
    }
}
