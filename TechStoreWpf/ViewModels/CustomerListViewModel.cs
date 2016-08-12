using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStoreLibrary.Database;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.ViewModels.Base;
using TechStoreWpf.Views;

namespace TechStoreWpf.ViewModels
{
    public class CustomerListViewModel : BaseViewModel
    {
        #region Attributes
        private CustomerListView customerListView;
        private ObservableCollection<Customer> customers;
        #endregion

        #region Properties
        /// <summary>
        /// Customer list view.
        /// </summary>
        public CustomerListView CustomerListView
        {
            get
            {
                return customerListView;
            }
            set
            {
                customerListView = value;
            }
        }

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

        public ICommand AddCustomerCommand { get; private set; }
        public ICommand EditCustomerCommand { get; private set; }
        public ICommand DeleteCustomerCommand { get; private set; }
        #endregion

        #region Constructors
        public CustomerListViewModel(CustomerListView customerListView)
        {
            CustomerListView = customerListView;

            Task.Run(() => LoadCustomersAsync());

            AddCustomerCommand = new RelayCommand(ExecAddCustomer, CanAddCustomer);
            EditCustomerCommand = new RelayCommand(ExecEditCustomer, CanEditCustomer);
            DeleteCustomerCommand = new RelayCommand(ExecDeleteCustomerAsync, CanDeleteCustomer);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads all customers.
        /// </summary>
        public async void LoadCustomersAsync()
        {
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case ConnectionResource.LOCALMYSQL:
                        Customers = new ObservableCollection<Customer>(await ctx.DbSetCustomers.Include(c => c.Address).ToListAsync());
                        break;
                    default:
                        break;
                }
            }
        }

        private bool CanAddCustomer(object obj)
        {
            return true;
        }

        /// <summary>
        /// Navigates to the customer form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddCustomer(object obj)
        {
            CustomerListView.NavigationService.Navigate(new CustomerView());
        }

        /// <summary>
        /// Edition is active only when a customer is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditCustomer(object obj)
        {
            if (CustomerListView.CustomerListUserControl.CustomerList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the customer form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditCustomer(object obj)
        {
            CustomerView customerView = new CustomerView((Customer)CustomerListView.CustomerListUserControl.CustomerList.SelectedItem);
            CustomerListView.NavigationService.Navigate(customerView);
        }

        /// <summary>
        /// Deletion is active only when a customer is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteCustomer(object obj)
        {
            if (CustomerListView.CustomerListUserControl.CustomerList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected customer.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeleteCustomerAsync(object obj)
        {
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case ConnectionResource.LOCALMYSQL:
                        Customer customer = (Customer)CustomerListView.CustomerListUserControl.CustomerList.SelectedItem;
                        Customers.Remove(customer);
                        ctx.DbSetCustomers.Attach(customer);
                        ctx.DbSetCustomers.Remove(customer);
                        await ctx.SaveChangesAsync();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
