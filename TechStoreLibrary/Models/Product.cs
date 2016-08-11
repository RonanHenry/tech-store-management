using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Models.Base;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines a product to be sold in the store
    /// </summary>
    public abstract class Product : BaseModel
    {
        #region Attributes
        private int id;
        private string brand;
        private string name;
        private string description;
        private string condition;
        private int stock;
        private decimal price;
        #endregion

        #region Properties
        /// <summary>
        /// Product's id.
        /// </summary>
        [Key]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Product's brand.
        /// </summary>
        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                brand = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Product's name.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Product's description.
        /// </summary>
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Product's condition (New or Refurbished).
        /// </summary>
        public string Condition
        {
            get
            {
                return condition;
            }
            set
            {
                condition = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Product's stock amount.
        /// </summary>
        public int Stock
        {
            get
            {
                return stock;
            }
            set
            {
                stock = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Product's price.
        /// </summary>
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Product()
        {
        }

        public Product(string brand, string name, string description, string condition, int stock, decimal price)
        {
            Brand = brand;
            Name = name;
            Description = description;
            Condition = condition;
            Stock = stock;
            Price = price;
        }
        #endregion

        #region Methods

        #endregion
    }
}
