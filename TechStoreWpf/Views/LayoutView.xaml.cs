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
    /// Interaction logic for Layout.xaml
    /// </summary>
    public partial class LayoutView : Page
    {
        #region Attributes
        private LayoutViewModel layoutViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// ViewModel of the view.
        /// </summary>
        public LayoutViewModel LayoutViewModel
        {
            get
            {
                return layoutViewModel;
            }
            set
            {
                layoutViewModel = value;
            }
        }

        public BrushConverter BrushConverter { get; set; }
        #endregion

        #region Constructors
        public LayoutView()
        {
            InitializeComponent();

            LayoutViewModel = new LayoutViewModel(this);
            DataContext = LayoutViewModel;

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

        private void Control_MouseLeave(object sender, MouseEventArgs e)
        {
            StatusBarTxt.Text = "Ready";
        }

        private void AddWorker_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarTxt.Text = "Add an employee";
        }

        private void AddCustomer_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarTxt.Text = "Add a customer";
        }

        private void AppAbout_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarTxt.Text = "See credits";
        }

        private void AppExit_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBarTxt.Text = "Exit the application";
        }

        private void AppExit_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow.GetWindow(this).Close();
        }

        private void AppAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tech Store Management\n\nMade with love by Ronan Henry", "About");
        }
        #endregion
    }
}
