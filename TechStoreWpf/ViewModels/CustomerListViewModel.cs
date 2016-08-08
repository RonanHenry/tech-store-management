using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Database;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Models;
using TechStoreWpf.ViewModels.Base;

namespace TechStoreWpf.ViewModels
{
    public class CustomerListViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<Customer> customers;
        #endregion

        #region Properties
        /// <summary>
        /// List of customers.
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
        #endregion

        #region Constructors
        public CustomerListViewModel()
        {
            Task.Run(() => LoadCustomers());
        }
        #endregion

        #region Methods
        public async void LoadCustomers()
        {
            MysqlManager<Customer> customerManager = new MysqlManager<Customer>(ConnectionResource.LOCALMYSQL);
            Customers = new ObservableCollection<Customer>(await customerManager.Get());
        }
        #endregion
    }
}
