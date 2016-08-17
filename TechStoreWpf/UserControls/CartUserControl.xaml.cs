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
    /// Interaction logic for CartUserControl.xaml
    /// </summary>
    public partial class CartUserControl : UserControl
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
        public CartUserControl()
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

        private void CartAddProduct_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Add selected product to cart";
        }

        private void CartRemoveProduct_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Remove selected product from cart";
        }

        private void CartUpQuantity_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Increase quantity of selected product";
        }

        private void CartDownQuantity_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Decrease quantity of selected product";
        }

        private void CartValidate_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Proceed to payment";
        }
        #endregion
    }
}
