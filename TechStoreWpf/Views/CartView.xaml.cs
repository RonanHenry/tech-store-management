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
using TechStoreWpf.ViewModels;

namespace TechStoreWpf.Views
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : Page
    {
        #region Attributes
        private CartViewModel cartViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// ViewModel of the view.
        /// </summary>
        public CartViewModel CartViewModel
        {
            get
            {
                return cartViewModel;
            }
            set
            {
                cartViewModel = value;
            }
        }
        #endregion

        #region Constructors
        public CartView()
        {
            InitializeComponent();

            CartViewModel = new CartViewModel(this);
            DataContext = CartViewModel;
        }
        #endregion

        #region Methods
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LayoutView layoutView = (LayoutView)Utility.FindParent<Page>(this, "LayoutPage");
            layoutView.StaffMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF333333");
            layoutView.ProductMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF333333");
            layoutView.CustomerMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF333333");
            layoutView.CartMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF565656");
        }
        #endregion
    }
}
