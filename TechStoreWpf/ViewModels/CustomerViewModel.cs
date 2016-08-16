using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
    public class CustomerViewModel : BaseViewModel
    {
        #region Attributes
        private CustomerView customerView;
        private Customer customer;
        #endregion

        #region Properties
        /// <summary>
        /// Customer view.
        /// </summary>
        public CustomerView CustomerView
        {
            get
            {
                return customerView;
            }
            set
            {
                customerView = value;
            }
        }

        /// <summary>
        /// Instance of the Customer model.
        /// </summary>
        public Customer Customer
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

        public ICommand SaveCustomerCommand { get; private set; }
        #endregion

        #region Constructors
        public CustomerViewModel(CustomerView customerView)
        {
            CustomerView = customerView;
            Customer = new Customer();
            Customer.Address = new Address();

            SaveCustomerCommand = new RelayCommand(ExecSaveCustomerAsync, CanSaveCustomer);
        }
        #endregion

        #region Methods
        private bool CanSaveCustomer(object obj)
        {
            return true;
        }

        /// <summary>
        /// Adds or updates a customer.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecSaveCustomerAsync(object obj)
        {
            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    WebServiceManager<Customer> webServiceManager = new WebServiceManager<Customer>();
                    if (Customer.Id == 0) // Saving new customer
                    {
                        await webServiceManager.PostAsync(Customer);
                    }
                    else // Saving updated customer
                    {
                        await webServiceManager.PutAsync(Customer);
                    }
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        if (Customer.Id == 0) // Saving new customer
                        {
                            ctx.DbSetCustomers.Add(Customer);
                            await ctx.SaveChangesAsync();
                        }
                        else // Saving updated customer
                        {
                            ctx.Entry(Customer).State = EntityState.Modified;
                            ctx.Entry(Customer.Address).State = EntityState.Modified;
                            await ctx.SaveChangesAsync();
                        }
                    }
                    break;
                default:
                    break;
            }
            
            CustomerView.NavigationService.Navigate(new CustomerListView());
        }
        #endregion
    }
}
