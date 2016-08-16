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
    /// Defines an item in the shopping cart.
    /// </summary>
    [Table("cartitems")]
    public class CartItem : BaseModel
    {
        #region Attributes
        private int id;
        private Cart cart;
        private Product product;
        private int quantity;
        private decimal price;
        #endregion

        #region Properties
        /// <summary>
        /// Cart item's id;
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

        public int CartId { get; set; }

        /// <summary>
        /// Cart in which the cart item is.
        /// </summary>
        [ForeignKey("CartId")]
        public virtual Cart Cart
        {
            get
            {
                return cart;
            }
            set
            {
                cart = value;
            }
        }

        public int ProductId { get; set; }

        /// <summary>
        /// Cart item's product;
        /// </summary>
        [ForeignKey("ProductId")]
        public virtual Product Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Cart item's quantity;
        /// </summary>
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Cart item's price;
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
        public CartItem()
        {
        }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        #endregion

        #region Methods

        #endregion
    }
}
