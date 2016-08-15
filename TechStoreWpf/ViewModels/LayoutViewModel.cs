using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStoreLibrary.Models;
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
        public ICommand AddCPUCommand { get; private set; }
        public ICommand AddGPUCommand { get; private set; }
        public ICommand AddMotherboardCommand { get; private set; }
        public ICommand AddMemoryCommand { get; private set; }
        public ICommand AddStorageCommand { get; private set; }
        public ICommand AddPSUCommand { get; private set; }
        public ICommand AddCaseCommand { get; private set; }
        #endregion

        #region Constructors
        public LayoutViewModel(LayoutView layoutView)
        {
            LayoutView = layoutView;

            AddWorkerCommand = new RelayCommand(ExecAddWorker, CanAdd);
            AddCustomerCommand = new RelayCommand(ExecAddCustomer, CanAdd);
            AddCPUCommand = new RelayCommand(ExecAddCPU, CanAdd);
            AddGPUCommand = new RelayCommand(ExecAddGPU, CanAdd);
            AddMotherboardCommand = new RelayCommand(ExecAddMotherboard, CanAdd);
            AddMemoryCommand = new RelayCommand(ExecAddMemory, CanAdd);
            AddStorageCommand = new RelayCommand(ExecAddStorage, CanAdd);
            AddPSUCommand = new RelayCommand(ExecAddPSU, CanAdd);
            AddCaseCommand = new RelayCommand(ExecAddCase, CanAdd);
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

        private void ExecAddCPU(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new ProductView(new CPU()));
        }

        private void ExecAddGPU(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new ProductView(new GPU()));
        }

        private void ExecAddMotherboard(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new ProductView(new Motherboard()));
        }

        private void ExecAddMemory(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new ProductView(new Memory()));
        }

        private void ExecAddStorage(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new ProductView(new Storage()));
        }

        private void ExecAddPSU(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new ProductView(new PSU()));
        }

        private void ExecAddCase(object obj)
        {
            LayoutView.ContentFrame.NavigationService.Navigate(new ProductView(new Case()));
        }
        #endregion
    }
}
