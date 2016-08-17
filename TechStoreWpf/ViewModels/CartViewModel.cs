using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStoreLibrary.Database;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.UserControls;
using TechStoreWpf.Views;

namespace TechStoreWpf.ViewModels
{
    public class CartViewModel : ProductListViewModel
    {
        #region Attributes
        private CartView cartView;
        private ObservableCollection<Customer> customers;
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
        /// List of registered customers.
        /// </summary>
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public ICommand CustomerSelectionChangedCommand { get; private set; }
        public ICommand AddToCartCommand { get; private set; }
        public ICommand UpQuantityCommand { get; private set; }
        public ICommand DownQuantityCommand { get; private set; }
        public ICommand RemoveFromCartCommand { get; private set; }
        public ICommand ProceedToPaymentCommand { get; private set; }
        #endregion

        #region Constructors
        public CartViewModel(CartView cartView)
        {
            CartView = cartView;
            Cart = new Cart();
            Cart.Items = new ObservableCollection<CartItem>();

            RemoveUnusedControls();
            Task.Run(() =>
            {
                LoadProductsAsync(true);
                LoadCustomersAsync();
            });

            CustomerSelectionChangedCommand = new RelayCommand(ExecCustomerChanged, CanChangeCustomer);
            AddToCartCommand = new RelayCommand(ExecAddToCart, CanAddToCart);
            UpQuantityCommand = new RelayCommand(ExecUpQuantity, CanUpQuantity);
            DownQuantityCommand = new RelayCommand(ExecDownQuantity, CanDownQuantity);
            RemoveFromCartCommand = new RelayCommand(ExecRemoveFromCart, CanRemoveFromCart);
            ProceedToPaymentCommand = new RelayCommand(ExecProceedToPaymentAsync, CanProceedToPayment);
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

        /// <summary>
        /// Loads all customers.
        /// </summary>
        public async void LoadCustomersAsync()
        {
            App.SetConnectionResource();

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    Customers = new ObservableCollection<Customer>(await new WebServiceManager<Customer>().GetAllAsync());
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        Customers = new ObservableCollection<Customer>(await ctx.DbSetCustomers.Include(c => c.Address).ToListAsync());
                    }
                    break;
                default:
                    break;
            }
        }

        private bool CanChangeCustomer(object obj)
        {
            return true;
        }

        /// <summary>
        /// Sets the customer to purchase products in the cart.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecCustomerChanged(object obj)
        {
            Cart.Customer = (Customer)CartView.CartUC.CartCustomer.SelectedItem;
        }

        /// <summary>
        /// Addition to the cart only possible if a product is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanAddToCart(object obj)
        {
            ProductListUserControl productListUC = CartView.CartUC.ProductsUC;

            if (productListUC.CPUList.SelectedIndex != -1 || 
                productListUC.GPUList.SelectedIndex != -1 || 
                productListUC.MotherboardList.SelectedIndex != -1 || 
                productListUC.MemoryList.SelectedIndex != -1 || 
                productListUC.StorageList.SelectedIndex != -1 || 
                productListUC.PSUList.SelectedIndex != -1 ||
                productListUC.CaseList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds the selected product to the cart's items.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddToCart(object obj)
        {
            ProductListUserControl productListUC = CartView.CartUC.ProductsUC;
            CartItem cartItem = new CartItem();
            cartItem.Cart = Cart;
            cartItem.Quantity = 1;

            if (productListUC.CPUList.SelectedIndex != -1) // CPU
            {
                CPU cpu = (CPU)productListUC.CPUList.SelectedItem;
                CPUs.Remove(cpu);
                cartItem.Product = cpu;
                cartItem.Price = cpu.Price;
            }
            else if (productListUC.GPUList.SelectedIndex != -1) // GPU
            {
                GPU gpu = (GPU)productListUC.GPUList.SelectedItem;
                GPUs.Remove(gpu);
                cartItem.Product = gpu;
                cartItem.Price = gpu.Price;
            }
            else if (productListUC.MotherboardList.SelectedIndex != -1) // Motherboard
            {
                Motherboard motherboard = (Motherboard)productListUC.MotherboardList.SelectedItem;
                Motherboards.Remove(motherboard);
                cartItem.Product = motherboard;
                cartItem.Price = motherboard.Price;
            }
            else if (productListUC.MemoryList.SelectedIndex != -1) // Memory
            {
                Memory memory = (Memory)productListUC.MemoryList.SelectedItem;
                Rams.Remove(memory);
                cartItem.Product = memory;
                cartItem.Price = memory.Price;
            }
            else if (productListUC.StorageList.SelectedIndex != -1) // Storage
            {
                Storage storage = (Storage)productListUC.StorageList.SelectedItem;
                StorageComponents.Remove(storage);
                cartItem.Product = storage;
                cartItem.Price = storage.Price;
            }
            else if (productListUC.PSUList.SelectedIndex != -1) // PSU
            {
                PSU psu = (PSU)productListUC.PSUList.SelectedItem;
                PSUs.Remove(psu);
                cartItem.Product = psu;
                cartItem.Price = psu.Price;
            }
            else // Case
            {
                Case pcCase = (Case)productListUC.CaseList.SelectedItem;
                Cases.Remove(pcCase);
                cartItem.Product = pcCase;
                cartItem.Price = pcCase.Price;
            }

            cartItem.Product.Stock--;
            Cart.Total += cartItem.Price;
            Cart.Items.Add(cartItem);
        }

        /// <summary>
        /// A product must be selected in the cart and its available stock
        /// must be higher than 0 to increase its quantity.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanUpQuantity(object obj)
        {
            if (CartView.CartUC.Cart.SelectedIndex != -1 && ((CartView.CartUC.Cart.SelectedItem as CartItem).Product as Product).Stock > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Increases the quantity of the selected product in the cart.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecUpQuantity(object obj)
        {
            CartItem cartItem = (CartItem)CartView.CartUC.Cart.SelectedItem;
            cartItem.Quantity++;
            cartItem.Product.Stock--;
            cartItem.Price += cartItem.Product.Price;
            Cart.Total += cartItem.Product.Price;
        }

        /// <summary>
        /// A product must be selected in the cart and its quantity
        /// must be higher than 1.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDownQuantity(object obj)
        {
            if (CartView.CartUC.Cart.SelectedIndex != -1 && (CartView.CartUC.Cart.SelectedItem as CartItem).Quantity > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Decreases the quantity of the selected product in the cart.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecDownQuantity(object obj)
        {
            CartItem cartItem = (CartItem)CartView.CartUC.Cart.SelectedItem;
            cartItem.Quantity--;
            cartItem.Product.Stock++;
            cartItem.Price -= cartItem.Product.Price;
            Cart.Total -= cartItem.Product.Price;
        }

        /// <summary>
        /// A product must be selected to be removed from the cart.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool CanRemoveFromCart(object obj)
        {
            if (CartView.CartUC.Cart.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Removes a product from the cart.
        /// </summary>
        /// <param name="obj"></param>
        public void ExecRemoveFromCart(object obj)
        {
            CartItem cartItem = (CartItem)CartView.CartUC.Cart.SelectedItem;
            cartItem.Product.Stock += cartItem.Quantity;
            Cart.Total -= cartItem.Price;
            Cart.Items.Remove(cartItem);
            cartItem.Cart = null;

            if (cartItem.Product is CPU)
            {
                CPUs.Add((CPU)cartItem.Product);
            }
            else if (cartItem.Product is GPU)
            {
                GPUs.Add((GPU)cartItem.Product);
            }
            else if (cartItem.Product is Motherboard)
            {
                Motherboards.Add((Motherboard)cartItem.Product);
            }
            else if (cartItem.Product is Memory)
            {
                Rams.Add((Memory)cartItem.Product);
            }
            else if (cartItem.Product is Storage)
            {
                StorageComponents.Add((Storage)cartItem.Product);
            }
            else if (cartItem.Product is PSU)
            {
                PSUs.Add((PSU)cartItem.Product);
            }
            else // Case
            {
                Cases.Add((Case)cartItem.Product);
            }
        }

        /// <summary>
        /// Payment can only be done if a customer is selected, 
        /// the cart is not empty and the total price of all products
        /// is less than or equal to the customer's money.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanProceedToPayment(object obj)
        {
            if (Cart.Customer != null && Cart.Items.Count != 0 && Cart.Total <= Cart.Customer.Money)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Processes the payment.
        /// Stock of products decreased, customer gets charged.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecProceedToPaymentAsync(object obj)
        {
            Cart.Customer.Money -= Cart.Total;

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    // API code
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.Entry(Cart.Customer).State = EntityState.Modified;
                        foreach (var item in Cart.Items)
                        {
                            ctx.Entry(item.Product).State = EntityState.Modified;
                        }
                        ctx.DbSetCarts.Add(Cart);
                        ctx.DbSetCartItems.AddRange(Cart.Items);
                        await ctx.SaveChangesAsync();
                    }
                    break;
                default:
                    break;
            }

            foreach (var item in Cart.Items)
            {
                if (item.Product is CPU)
                {
                    CPUs.Add((CPU)item.Product);
                }
                else if (item.Product is GPU)
                {
                    GPUs.Add((GPU)item.Product);
                }
                else if (item.Product is Motherboard)
                {
                    Motherboards.Add((Motherboard)item.Product);
                }
                else if (item.Product is Memory)
                {
                    Rams.Add((Memory)item.Product);
                }
                else if (item.Product is Storage)
                {
                    StorageComponents.Add((Storage)item.Product);
                }
                else if (item.Product is PSU)
                {
                    PSUs.Add((PSU)item.Product);
                }
                else // Case
                {
                    Cases.Add((Case)item.Product);
                }
            }
            Cart.Items.Clear();
            Cart.Total = 0;
        }
        #endregion
    }
}
