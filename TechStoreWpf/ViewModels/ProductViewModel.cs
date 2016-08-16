using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TechStoreLibrary.Database;
using TechStoreLibrary.Enums;
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
            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    if (Product.Id == 0) // Saving new product
                    {
                        if (Product is CPU)
                        {
                            await new WebServiceManager<CPU>().PostAsync((CPU)Product);
                        }
                        else if (Product is GPU)
                        {
                            await new WebServiceManager<GPU>().PostAsync((GPU)Product);
                        }
                        else if (Product is Motherboard)
                        {
                            await new WebServiceManager<Motherboard>().PostAsync((Motherboard)Product);
                        }
                        else if (Product is Memory)
                        {
                            await new WebServiceManager<Memory>().PostAsync((Memory)Product);
                        }
                        else if (Product is Storage)
                        {
                            await new WebServiceManager<Storage>().PostAsync((Storage)Product);
                        }
                        else if (Product is PSU)
                        {
                            await new WebServiceManager<PSU>().PostAsync((PSU)Product);
                        }
                        else // Case
                        {
                            await new WebServiceManager<Case>().PostAsync((Case)Product);
                        }
                    }
                    else // Saving updated product
                    {
                        if (Product is CPU)
                        {
                            await new WebServiceManager<CPU>().PutAsync((CPU)Product);
                        }
                        else if (Product is GPU)
                        {
                            await new WebServiceManager<GPU>().PutAsync((GPU)Product);
                        }
                        else if (Product is Motherboard)
                        {
                            await new WebServiceManager<Motherboard>().PutAsync((Motherboard)Product);
                        }
                        else if (Product is Memory)
                        {
                            await new WebServiceManager<Memory>().PutAsync((Memory)Product);
                        }
                        else if (Product is Storage)
                        {
                            await new WebServiceManager<Storage>().PutAsync((Storage)Product);
                        }
                        else if (Product is PSU)
                        {
                            await new WebServiceManager<PSU>().PutAsync((PSU)Product);
                        }
                        else // Case
                        {
                            await new WebServiceManager<Case>().PutAsync((Case)Product);
                        }
                    }
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(App.DataSource))
                    {
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
                    }
                    break;
                default:
                    break;
            }

            string activeTabName = (string)Utility.FindParent<StackPanel>((Button)ProductView.ProductUserControl.SaveProductBtn).Tag;
            ProductView.NavigationService.Navigate(new ProductListView(activeTabName));
        }
        #endregion
    }
}
