using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Models.Base;

namespace TechStoreLibrary.Models
{
    /// <summary>
    /// Defines a shopping cart.
    /// </summary>
    [Table("carts")]
    public class Cart : BaseModel
    {
        #region Attributes
        private int id;
        private Customer customer;
        private List<CartItem> items;
        private decimal total;
        #endregion

        #region Properties
        // <summary>
        /// Cart's id;
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

        public int CustomerId { get; set; }

        /// <summary>
        /// Cart's customer.
        /// </summary>
        [ForeignKey("CustomerId")]
        public virtual Customer Customer
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Cart's list of products.
        /// </summary>
        public virtual List<CartItem> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Cart's total price.
        /// </summary>
        public decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                total = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public Cart()
        {
        }

        public Cart(Customer customer)
        {
            Customer = customer;
        }

        public Cart(Customer customer, List<CartItem> items)
            : this(customer)
        {
            Items = items;
        }
        #endregion

        #region Methods

        #endregion
    }
}
