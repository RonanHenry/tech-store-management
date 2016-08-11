using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TechStoreWpf.Views;

namespace TechStoreWpf.UserControls
{
    /// <summary>
    /// Interaction logic for WorkerListUserControl.xaml
    /// </summary>
    public partial class WorkerListUserControl : UserControl
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
        public WorkerListUserControl()
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

        private void AddWorker_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Add an employee";
        }
        #endregion

        private void EditWorker_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Edit selected employee";
        }

        private void DeleteWorker_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Delete selected employee";
        }
    }
}
