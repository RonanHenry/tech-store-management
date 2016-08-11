using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.FakerLoader;
using TechStoreLibrary.DataDefinitions;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines a PC case.
    /// </summary>
    public class Case : Product, IFakerLoader<Case>
    {
        #region Attributes
        private string type;
        private string frontIO;
        private bool hasWindow;
        #endregion

        #region Properties
        /// <summary>
        /// Case's type (Mini tower, Mid tower or Full tower).
        /// </summary>
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Case's front ports (USB, Audio, e-SATA)
        /// </summary>
        public string FrontIO
        {
            get
            {
                return frontIO;
            }
            set
            {
                frontIO = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Indicates if the case has a side panel window or not.
        /// </summary>
        public bool HasWindow
        {
            get
            {
                return hasWindow;
            }
            set
            {
                hasWindow = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Case()
        {
        }

        public Case(string brand, string name, string description, int stock, decimal price, string type, string condition, string frontIO, bool hasWindow)
            : base(brand, name, description, condition, stock, price)
        {
            Type = type;
            FrontIO = frontIO;
            HasWindow = hasWindow;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Generates a random Case item.
        /// </summary>
        /// <returns></returns>
        public Case LoadSingleItem()
        {
            CommonData commonData = new CommonData();
            CaseData caseData = new CaseData();
            int brandIndex = Faker.Number.RandomNumber(0, caseData.Brands.Count);

            Case pcCase = new Case();
            pcCase.Brand = caseData.Brands[brandIndex];
            pcCase.Name = caseData.Names[brandIndex][Faker.Number.RandomNumber(0, caseData.Names[brandIndex].Count)];
            pcCase.Description = "Some case description";
            pcCase.Condition = commonData.Conditions[Faker.Number.RandomNumber(0, commonData.Conditions.Length)];
            pcCase.Stock = Faker.Number.RandomNumber(0, 51);
            pcCase.Price = Faker.Number.RandomNumber(40, 350);
            pcCase.Type = caseData.Types[Faker.Number.RandomNumber(0, caseData.Types.Length)];
            pcCase.FrontIO = caseData.FrontIOs[Faker.Number.RandomNumber(0, caseData.FrontIOs.Length)];
            pcCase.HasWindow = Faker.Number.Bool();

            return pcCase;
        }

        /// <summary>
        /// Generates a list of random Case items.
        /// </summary>
        /// <returns></returns>
        public List<Case> LoadMultipleItems()
        {
            List<Case> pcCases = new List<Case>();

            for (int i = 0; i < Faker.Number.RandomNumber(10, 51); i++)
            {
                pcCases.Add(LoadSingleItem());
            }

            return pcCases;
        }
        #endregion
    }
}
