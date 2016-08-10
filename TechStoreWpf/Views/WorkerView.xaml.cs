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
using TechStoreLibrary.Models;
using TechStoreWpf.ViewModels;

namespace TechStoreWpf.Views
{
    /// <summary>
    /// Interaction logic for WorkerView.xaml
    /// </summary>
    public partial class WorkerView : Page
    {
        #region Attributes
        private WorkerViewModel workerViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// ViewModel of the view.
        /// </summary>
        public WorkerViewModel WorkerViewModel
        {
            get
            {
                return workerViewModel;
            }
            set
            {
                workerViewModel = value;
            }
        }
        #endregion

        #region Constructors
        public WorkerView()
        {
            InitializeComponent();

            WorkerViewModel = new WorkerViewModel(this);
            DataContext = WorkerViewModel;
        }

        public WorkerView(Worker worker)
            : this()
        {
            WorkerViewModel.Worker = worker;
        }
        #endregion

        #region Methods

        #endregion
    }
}
