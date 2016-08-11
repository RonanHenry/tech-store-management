using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechStoreWpf.ViewModels;

namespace TechStoreWpf.Views
{
    /// <summary>
    /// Interaction logic for CustomerListView.xaml
    /// </summary>
    public partial class CustomerListView : Page
    {
        #region Attributes
        private CustomerListViewModel customerListViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// ViewModel of the view.
        /// </summary>
        public CustomerListViewModel CustomerListViewModel
        {
            get
            {
                return customerListViewModel;
            }
            set
            {
                customerListViewModel = value;
            }
        }
        #endregion

        #region Constructors
        public CustomerListView()
        {
            InitializeComponent();

            CustomerListViewModel = new CustomerListViewModel(this);
            DataContext = CustomerListViewModel;
        }
        #endregion

        #region Methods

        #endregion
    }
}
