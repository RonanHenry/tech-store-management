using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TechStoreLibrary.Database;
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.ViewModels.Base;
using TechStoreWpf.Views;

namespace TechStoreWpf.ViewModels
{
    public class ProductViewModel : BaseViewModel
    {
        #region Attributes
        private ProductView productView;
        private Product product;
        #endregion

        #region Properties
        /// <summary>
        /// Product view.
        /// </summary>
        public ProductView ProductView
        {
            get
            {
                return productView;
            }
            set
            {
                productView = value;
            }
        }

        /// <summary>
        /// Instance of a product (CPU, GPU, Motherboard, etc...)
        /// </summary>
        public Product Product
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

        public ICommand SaveProductCommand { get; private set; }
        #endregion

        #region Constructors
        public ProductViewModel(ProductView productView, Product product)
        {
            ProductView = productView;
            Product = product;

            SaveProductCommand = new RelayCommand(ExecSaveProductAsync, CanSaveProduct);
        }
        #endregion

        #region Methods
        private bool CanSaveProduct(object obj)
        {
            return true;
        }

        /// <summary>
        /// Adds or updates a product.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecSaveProductAsync(object obj)
        {
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALMYSQL:
                        if (Product.Id == 0) // Saving new product
                        {
                            if (Product is CPU)
                            {
                                ctx.DbSetCPUs.Add((CPU)Product);
                            }
                            else if (Product is GPU)
                            {
                                ctx.DbSetGPUs.Add((GPU)Product);
                            }
                            else if (Product is Motherboard)
                            {
                                ctx.DbSetMotherboards.Add((Motherboard)Product);
                            }
                            else if (Product is Memory)
                            {
                                ctx.DbSetMemories.Add((Memory)Product);
                            }
                            else if (Product is Storage)
                            {
                                ctx.DbSetStorages.Add((Storage)Product);
                            }
                            else if (Product is PSU)
                            {
                                ctx.DbSetPSUs.Add((PSU)Product);
                            }
                            else // Case
                            {
                                ctx.DbSetCases.Add((Case)Product);
                            }
                            await ctx.SaveChangesAsync();
                        }
                        else // Saving updated product
                        {
                            ctx.Entry(Product).State = EntityState.Modified;
                            await ctx.SaveChangesAsync();
                        }
                        break;
                    default:
                        break;
                }

                string activeTabName = (string)Utility.FindParent<StackPanel>((Button)ProductView.ProductUserControl.SaveProductBtn).Tag;
                ProductView.NavigationService.Navigate(new ProductListView(activeTabName));
            }
        }
        #endregion
    }
}
