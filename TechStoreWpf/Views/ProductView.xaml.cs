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
    /// Interaction logic for ProductView.xaml
    /// </summary>
    public partial class ProductView : Page
    {
        #region Attributes
        private ProductViewModel productViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// ViewModel of the view.
        /// </summary>
        public ProductViewModel ProductViewModel
        {
            get
            {
                return productViewModel;
            }
            set
            {
                productViewModel = value;
            }
        }
        #endregion

        #region Constructors
        public ProductView(Product product)
        {
            InitializeComponent();

            ProductViewModel = new ProductViewModel(this, product);
            DataContext = ProductViewModel;
        }
        #endregion

        #region Methods
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LayoutView layoutView = (LayoutView)Utility.FindParent<Page>(this, "LayoutPage");
            layoutView.StaffMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF333333");
            layoutView.CustomerMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF333333");
            layoutView.ProductMenu.Background = (Brush)layoutView.BrushConverter.ConvertFrom("#FF565656");

            Product product = ProductViewModel.Product;

            if (product is CPU)
            {
                ProductTitle.Text = "Central Processing Unit (CPU)";
            }
            else if (product is GPU)
            {
                ProductTitle.Text = "Graphics card (GPU)";
            }
            else if (product is Motherboard)
            {
                ProductTitle.Text = "Motherboard";
            }
            else if (product is Memory)
            {
                ProductTitle.Text = "Memory";
            }
            else if (product is Storage)
            {
                ProductTitle.Text = "Storage";
            }
            else if (product is PSU)
            {
                ProductTitle.Text = "Power supply";
            }
            else // Case
            {
                ProductTitle.Text = "Case";
            }
        }
        #endregion
    }
}
