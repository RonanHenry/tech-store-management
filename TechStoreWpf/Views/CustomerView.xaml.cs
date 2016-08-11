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
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.ViewModels;

namespace TechStoreWpf.Views
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : Page
    {
        #region Attributes
        private CustomerViewModel customerViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// ViewModel of the view.
        /// </summary>
        public CustomerViewModel CustomerViewModel
        {
            get
            {
                return customerViewModel;
            }
            set
            {
                customerViewModel = value;
            }
        }
        #endregion

        #region Constructors
        public CustomerView()
        {
            InitializeComponent();

            CustomerViewModel = new CustomerViewModel(this);
            DataContext = CustomerViewModel;
        }

        public CustomerView(Customer customer)
            : this()
        {
            CustomerViewModel.Customer = customer;
        }
        #endregion

        #region Methods
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LayoutView layoutView = (LayoutView)Utility.FindParent<Page>(this, "LayoutPage");
            layoutView.StaffMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF333333");
            layoutView.ProductMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF333333");
            layoutView.CustomerMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF565656");
        }
        #endregion
    }
}
