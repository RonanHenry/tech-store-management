﻿using System;
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
    /// Interaction logic for ProductListUserControl.xaml
    /// </summary>
    public partial class ProductListUserControl : UserControl
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
        public ProductListUserControl()
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

        private void AddCPU_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Add a CPU product";
        }

        private void EditCPU_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Edit selected CPU product";
        }

        private void DeleteCPU_MouseEnter(object sender, MouseEventArgs e)
        {
            StatusBar.Text = "Delete selected CPU product";
        }
        #endregion
    }
}