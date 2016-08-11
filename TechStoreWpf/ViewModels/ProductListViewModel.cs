using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Input;
using TechStoreLibrary.Database;
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.ViewModels.Base;
using TechStoreWpf.Views;

namespace TechStoreWpf.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        #region Attributes
        private ProductListView productListView;
        private ObservableCollection<CPU> cpus;
        #endregion

        #region Properties
        /// <summary>
        /// Product list view.
        /// </summary>
        public ProductListView ProductListView
        {
            get
            {
                return productListView;
            }
            set
            {
                productListView = value;
            }
        }

        /// <summary>
        /// CPU products list.
        /// </summary>
        public ObservableCollection<CPU> CPUs
        {
            get
            {
                return cpus;
            }
            set
            {
                cpus = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCPUCommand { get; private set; }
        public ICommand EditCPUCommand { get; private set; }
        public ICommand DeleteCPUCommand { get; private set; }
        #endregion

        #region Constructors
        public ProductListViewModel(ProductListView productListView)
        {
            ProductListView = productListView;

            Task.Run(() => LoadProductsAsync());

            AddCPUCommand = new RelayCommand(ExecAddCPU, CanAddProduct);
            EditCPUCommand = new RelayCommand(ExecEditCPU, CanEditCPU);
            DeleteCPUCommand = new RelayCommand(ExecDeleteCPU, CanDeleteCPU);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads all products from data resource.
        /// </summary>
        public async void LoadProductsAsync()
        {
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALMYSQL:
                        CPUs = new ObservableCollection<CPU>(await ctx.DbSetCPUs.ToListAsync());
                        break;
                    default:
                        break;
                }
            }
        }

        private bool CanAddProduct(object obj)
        {
            return true;
        }

        private void ExecAddCPU(object obj)
        {

        }

        /// <summary>
        /// Edition is active only when a CPU is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditCPU(object obj)
        {
            if (ProductListView.ProductListUserControl.CPUList.SelectedIndex > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ExecEditCPU(object obj)
        {

        }

        /// <summary>
        /// Deletion is active only when a CPU is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteCPU(object obj)
        {
            if (ProductListView.ProductListUserControl.CPUList.SelectedIndex > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ExecDeleteCPU(object obj)
        {

        }
        #endregion
    }
}
