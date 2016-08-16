using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for CustomerUserControl.xaml
    /// </summary>
    public partial class CustomerUserControl : UserControl
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public CustomerUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Utility.FindParent<Page>(this).NavigationService.Navigate(new CustomerListView());
        }

        /// <summary>
        /// Disallows spaces to be entered in the field.
        /// </summary>
        /// <param name="sender">Element firing the event.</param>
        /// <param name="e">Data for KeyUp and KeyDown routed events.</param>
        private void NoSpace_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        /// <summary>
        /// Only allows numbers in the field.
        /// </summary>
        /// <param name="sender">Element firing the event.</param>
        /// <param name="e">Arguments associated with changes to a TextComposition.</param>
        private void CustomerAddressZipCodeTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9]");
        }

        /// <summary>
        /// Only allows numbers and periods in the field.
        /// </summary>
        /// <param name="sender">Element firing the event.</param>
        /// <param name="e">Arguments associated with changes to a TextComposition.</param>
        private void CustomerPriceTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9.]");
        }
        #endregion
    }
}
