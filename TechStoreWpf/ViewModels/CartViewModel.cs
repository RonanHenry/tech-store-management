using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Models;
using TechStoreWpf.UserControls;
using TechStoreWpf.ViewModels.Base;
using TechStoreWpf.Views;

namespace TechStoreWpf.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        #region Attributes
        private CartView cartView;
        private Cart cart;
        #endregion

        #region Properties
        /// <summary>
        /// Cart view.
        /// </summary>
        public CartView CartView
        {
            get
            {
                return cartView;
            }
            set
            {
                cartView = value;
            }
        }

        /// <summary>
        /// Instance of the Cart model.
        /// </summary>
        public Cart Cart
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
        #endregion

        #region Constructors
        public CartViewModel(CartView cartView)
        {
            CartView = cartView;
            Cart = new Cart();

            RemoveUnusedControls();
        }
        #endregion

        #region Methods
        public void RemoveUnusedControls()
        {
            ProductListUserControl productListUC = CartView.CartUC.ProductsUC;

            // CPU
            productListUC.CPUGrid.Children.Remove(productListUC.CPUTitle);
            productListUC.CPUGrid.Children.Remove(productListUC.CPUBtns);

            // GPU
            productListUC.GPUGrid.Children.Remove(productListUC.GPUTitle);
            productListUC.GPUGrid.Children.Remove(productListUC.GPUBtns);

            // Motherboard
            productListUC.MotherboardGrid.Children.Remove(productListUC.MotherboardTitle);
            productListUC.MotherboardGrid.Children.Remove(productListUC.MotherboardBtns);

            // Memory
            productListUC.MemoryGrid.Children.Remove(productListUC.MemoryTitle);
            productListUC.MemoryGrid.Children.Remove(productListUC.MemoryBtns);

            // Storage
            productListUC.StorageGrid.Children.Remove(productListUC.StorageTitle);
            productListUC.StorageGrid.Children.Remove(productListUC.StorageBtns);

            // PSU
            productListUC.PSUGrid.Children.Remove(productListUC.PSUTitle);
            productListUC.PSUGrid.Children.Remove(productListUC.PSUBtns);

            // Case
            productListUC.CaseGrid.Children.Remove(productListUC.CaseTitle);
            productListUC.CaseGrid.Children.Remove(productListUC.CaseBtns);
        }
        #endregion
    }
}
