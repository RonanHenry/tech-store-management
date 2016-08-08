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

namespace TechStoreWpf.Views
{
    /// <summary>
    /// Interaction logic for Layout.xaml
    /// </summary>
    public partial class Layout : Page
    {
        #region Attributes

        #endregion

        #region Properties
        public BrushConverter BrushConverter { get; set; }
        #endregion

        #region Constructors
        public Layout()
        {
            InitializeComponent();
            BrushConverter = new BrushConverter();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the worker list view in the frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StaffMenu_Click(object sender, RoutedEventArgs e)
        {
            CustomerMenu.Background = (Brush)BrushConverter.ConvertFrom("#FF333333");
            StaffMenu.Background = (Brush)BrushConverter.ConvertFrom("#FF565656");
            ContentFrame.NavigationService.Navigate(new WorkerListView());
        }
        #endregion

        /// <summary>
        /// Loads the customer list view in the frame.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerMenu_Click(object sender, RoutedEventArgs e)
        {
            StaffMenu.Background = (Brush)BrushConverter.ConvertFrom("#FF333333");
            CustomerMenu.Background = (Brush)BrushConverter.ConvertFrom("#FF565656");
            ContentFrame.NavigationService.Navigate(new CustomerListView());
        }
    }
}
