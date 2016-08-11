using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStoreWpf.Helpers;
using TechStoreWpf.ViewModels.Base;
using TechStoreWpf.Views;

namespace TechStoreWpf.ViewModels
{
    public class LayoutViewModel : BaseViewModel
    {
        #region Attributes
        private LayoutView layoutView;
        #endregion

        #region Properties
        /// <summary>
        /// Layout view.
        /// </summary>
        public LayoutView LayoutView
        {
            get
            {
                return layoutView;
            }
            set
            {
                layoutView = value;
            }
        }

        public ICommand AddWorkerCommand { get; private set; }
        public ICommand AddCustomerCommand { get; private set; }
        #endregion

        #region Constructors
        public LayoutViewModel(LayoutView layoutView)
        {
            LayoutView = layoutView;

            AddWorkerCommand = new RelayCommand(ExecAddWorker, CanAdd);
            AddCustomerCommand = new RelayCommand(ExecAddCustomer, CanAdd);
        }
        #endregion

        #region Methods
        private bool CanAdd(object obj)
        {
            return true;
        }

        private void ExecAddWorker(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new WorkerView());
        }

        private void ExecAddCustomer(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new CustomerView());
        }
        #endregion
    }
}
