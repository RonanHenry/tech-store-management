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
using TechStoreWpf.Helpers;
using TechStoreWpf.Views;

namespace TechStoreWpf.UserControls
{
    /// <summary>
    /// Interaction logic for CustomerListUserControl.xaml
    /// </summary>
    public partial class CustomerListUserControl : UserControl
    {
        #region Attributes
        private TextBlock statusBar;
        #endregion

        #region Properties
        /// <summary>
        /// Bottom status bar text.
        /// </summary>
        public TextBlock StatusBar
        {
            get
            {
                return statusBar;
            }
            set
            {
                statusBar = value;
            }
        }
        #endregion

        #region Constructors
        public CustomerListUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LayoutView layoutView = (LayoutView)Utility.FindParent<Page>(this, "LayoutPage");
            StatusBar = layoutView.StatusBarTxt;
        }

        private void Control_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Ready";
        }

        private void AddCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Add a customer";
        }

        private void EditCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Edit selected customer";
        }

        private void DeleteCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Delete selected customer";
        }
        #endregion
    }
}
