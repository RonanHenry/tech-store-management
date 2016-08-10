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
    /// Interaction logic for WorkerListView.xaml
    /// </summary>
    public sealed partial class WorkerListView : Page
    {
        #region Attributes
        private WorkerListViewModel workerListViewModel;
        #endregion

        #region Properties
        /// <summary>
        /// ViewModel of the view.
        /// </summary>
        public WorkerListViewModel WorkerListViewModel
        {
            get
            {
                return workerListViewModel;
            }
            set
            {
                workerListViewModel = value;
            }
        }
        #endregion

        #region Constructors
        public WorkerListView()
        {
            InitializeComponent();

            WorkerListViewModel = new WorkerListViewModel(this);
            DataContext = WorkerListViewModel;
        }
        #endregion

        #region Methods

        #endregion

    }
}
