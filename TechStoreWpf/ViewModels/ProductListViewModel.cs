using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Input;
using TechStoreLibrary.Database;
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.ViewModels.Base;
using TechStoreWpf.Views;
using TechStoreLibrary.Enums;

namespace TechStoreWpf.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        #region Attributes
        private ProductListView productListView;
        private ObservableCollection<CPU> cpus;
        private ObservableCollection<GPU> gpus;
        private ObservableCollection<Motherboard> motherboards;
        private ObservableCollection<Memory> rams;
        private ObservableCollection<Storage> storageComponents;
        private ObservableCollection<PSU> psus;
        private ObservableCollection<Case> cases;
        #endregion

        #region Properties
        /// <summary>
        /// Product list view.
        /// </summary>
        public ProductListView ProductListView
        {
            get
            {
                return productListView;
            }
            set
            {
                productListView = value;
            }
        }

        /// <summary>
        /// CPU products list.
        /// </summary>
        public ObservableCollection<CPU> CPUs
        {
            get
            {
                return cpus;
            }
            set
            {
                cpus = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// GPU products list.
        /// </summary>
        public ObservableCollection<GPU> GPUs
        {
            get
            {
                return gpus;
            }
            set
            {
                gpus = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Storage products list.
        /// </summary>
        public ObservableCollection<Storage> StorageComponents
        {
            get
            {
                return storageComponents;
            }
            set
            {
                storageComponents = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Motherboard products list.
        /// </summary>
        public ObservableCollection<Motherboard> Motherboards
        {
            get
            {
                return motherboards;
            }
            set
            {
                motherboards = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Memory products list.
        /// </summary>
        public ObservableCollection<Memory> Rams
        {
            get
            {
                return rams;
            }
            set
            {
                rams = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// PSU products list.
        /// </summary>
        public ObservableCollection<PSU> PSUs
        {
            get
            {
                return psus;
            }
            set
            {
                psus = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Case products list.
        /// </summary>
        public ObservableCollection<Case> Cases
        {
            get
            {
                return cases;
            }
            set
            {
                cases = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCPUCommand { get; private set; }
        public ICommand EditCPUCommand { get; private set; }
        public ICommand DeleteCPUCommand { get; private set; }

        public ICommand AddGPUCommand { get; private set; }
        public ICommand EditGPUCommand { get; private set; }
        public ICommand DeleteGPUCommand { get; private set; }

        public ICommand AddMotherboardCommand { get; private set; }
        public ICommand EditMotherboardCommand { get; private set; }
        public ICommand DeleteMotherboardCommand { get; private set; }

        public ICommand AddMemoryCommand { get; private set; }
        public ICommand EditMemoryCommand { get; private set; }
        public ICommand DeleteMemoryCommand { get; private set; }

        public ICommand AddStorageCommand { get; private set; }
        public ICommand EditStorageCommand { get; private set; }
        public ICommand DeleteStorageCommand { get; private set; }

        public ICommand AddPSUCommand { get; private set; }
        public ICommand EditPSUCommand { get; private set; }
        public ICommand DeletePSUCommand { get; private set; }

        public ICommand AddCaseCommand { get; private set; }
        public ICommand EditCaseCommand { get; private set; }
        public ICommand DeleteCaseCommand { get; private set; }
        #endregion

        #region Constructors
        public ProductListViewModel()
        {
        }

        public ProductListViewModel(ProductListView productListView)
        {
            ProductListView = productListView;

            Task.Run(() => LoadProductsAsync());

            AddCPUCommand = new RelayCommand(ExecAddCPU, CanAddProduct);
            EditCPUCommand = new RelayCommand(ExecEditCPU, CanEditCPU);
            DeleteCPUCommand = new RelayCommand(ExecDeleteCPUAsync, CanDeleteCPU);

            AddGPUCommand = new RelayCommand(ExecAddGPU, CanAddProduct);
            EditGPUCommand = new RelayCommand(ExecEditGPU, CanEditGPU);
            DeleteGPUCommand = new RelayCommand(ExecDeleteGPUAsync, CanDeleteGPU);

            AddMotherboardCommand = new RelayCommand(ExecAddMotherboard, CanAddProduct);
            EditMotherboardCommand = new RelayCommand(ExecEditMotherboard, CanEditMotherboard);
            DeleteMotherboardCommand = new RelayCommand(ExecDeleteMotherboardAsync, CanDeleteMotherboard);

            AddMemoryCommand = new RelayCommand(ExecAddMemory, CanAddProduct);
            EditMemoryCommand = new RelayCommand(ExecEditMemory, CanEditMemory);
            DeleteMemoryCommand = new RelayCommand(ExecDeleteMemoryAsync, CanDeleteMemory);

            AddStorageCommand = new RelayCommand(ExecAddStorage, CanAddProduct);
            EditStorageCommand = new RelayCommand(ExecEditStorage, CanEditStorage);
            DeleteStorageCommand = new RelayCommand(ExecDeleteStorageAsync, CanDeleteStorage);

            AddPSUCommand = new RelayCommand(ExecAddPSU, CanAddProduct);
            EditPSUCommand = new RelayCommand(ExecEditPSU, CanEditPSU);
            DeletePSUCommand = new RelayCommand(ExecDeletePSUAsync, CanDeletePSU);

            AddCaseCommand = new RelayCommand(ExecAddCase, CanAddProduct);
            EditCaseCommand = new RelayCommand(ExecEditCase, CanEditCase);
            DeleteCaseCommand = new RelayCommand(ExecDeleteCaseAsync, CanDeleteCase);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads all products.
        /// </summary>
        public async void LoadProductsAsync(bool inStockOnly = false)
        {
            App.SetConnectionResource();

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    if (inStockOnly)
                    {
                        CPUs = new ObservableCollection<CPU>(await new WebServiceManager<CPU>().GetAllAsync(true));
                        GPUs = new ObservableCollection<GPU>(await new WebServiceManager<GPU>().GetAllAsync(true));
                        Motherboards = new ObservableCollection<Motherboard>(await new WebServiceManager<Motherboard>().GetAllAsync(true));
                        Rams = new ObservableCollection<Memory>(await new WebServiceManager<Memory>().GetAllAsync(true));
                        StorageComponents = new ObservableCollection<Storage>(await new WebServiceManager<Storage>().GetAllAsync(true));
                        PSUs = new ObservableCollection<PSU>(await new WebServiceManager<PSU>().GetAllAsync(true));
                        Cases = new ObservableCollection<Case>(await new WebServiceManager<Case>().GetAllAsync(true));
                    }
                    else
                    {
                        CPUs = new ObservableCollection<CPU>(await new WebServiceManager<CPU>().GetAllAsync());
                        GPUs = new ObservableCollection<GPU>(await new WebServiceManager<GPU>().GetAllAsync());
                        Motherboards = new ObservableCollection<Motherboard>(await new WebServiceManager<Motherboard>().GetAllAsync());
                        Rams = new ObservableCollection<Memory>(await new WebServiceManager<Memory>().GetAllAsync());
                        StorageComponents = new ObservableCollection<Storage>(await new WebServiceManager<Storage>().GetAllAsync());
                        PSUs = new ObservableCollection<PSU>(await new WebServiceManager<PSU>().GetAllAsync());
                        Cases = new ObservableCollection<Case>(await new WebServiceManager<Case>().GetAllAsync());
                    }
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        if (inStockOnly)
                        {
                            CPUs = new ObservableCollection<CPU>(await ctx.DbSetCPUs.Where(cpu => cpu.Stock > 0).ToListAsync());
                            GPUs = new ObservableCollection<GPU>(await ctx.DbSetGPUs.Where(gpu => gpu.Stock > 0).ToListAsync());
                            Motherboards = new ObservableCollection<Motherboard>(await ctx.DbSetMotherboards.Where(mb => mb.Stock > 0).ToListAsync());
                            Rams = new ObservableCollection<Memory>(await ctx.DbSetMemories.Where(ram => ram.Stock > 0).ToListAsync());
                            StorageComponents = new ObservableCollection<Storage>(await ctx.DbSetStorages.Where(storage => storage.Stock > 0).ToListAsync());
                            PSUs = new ObservableCollection<PSU>(await ctx.DbSetPSUs.Where(psu => psu.Stock > 0).ToListAsync());
                            Cases = new ObservableCollection<Case>(await ctx.DbSetCases.Where(pcCase => pcCase.Stock > 0).ToListAsync());
                        }
                        else
                        {
                            CPUs = new ObservableCollection<CPU>(await ctx.DbSetCPUs.ToListAsync());
                            GPUs = new ObservableCollection<GPU>(await ctx.DbSetGPUs.ToListAsync());
                            Motherboards = new ObservableCollection<Motherboard>(await ctx.DbSetMotherboards.ToListAsync());
                            Rams = new ObservableCollection<Memory>(await ctx.DbSetMemories.ToListAsync());
                            StorageComponents = new ObservableCollection<Storage>(await ctx.DbSetStorages.ToListAsync());
                            PSUs = new ObservableCollection<PSU>(await ctx.DbSetPSUs.ToListAsync());
                            Cases = new ObservableCollection<Case>(await ctx.DbSetCases.ToListAsync());
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private bool CanAddProduct(object obj)
        {
            return true;
        }

        /// <summary>
        /// Navigates to the CPU form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddCPU(object obj)
        {
            ProductListView.NavigationService.Navigate(new ProductView(new CPU()));
        }

        /// <summary>
        /// Edition is active only when a CPU is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditCPU(object obj)
        {
            if (ProductListView.ProductListUserControl.CPUList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the cpu form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditCPU(object obj)
        {
            ProductView productView = new ProductView((CPU)ProductListView.ProductListUserControl.CPUList.SelectedItem);
            ProductListView.NavigationService.Navigate(productView);
        }

        /// <summary>
        /// Deletion is active only when a CPU is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteCPU(object obj)
        {
            if (ProductListView.ProductListUserControl.CPUList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected CPU product.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeleteCPUAsync(object obj)
        {
            CPU cpu = (CPU)ProductListView.ProductListUserControl.CPUList.SelectedItem;
            CPUs.Remove(cpu);

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    await new WebServiceManager<CPU>().DeleteAsync(cpu);
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.DbSetCPUs.Attach(cpu);
                        ctx.DbSetCPUs.Remove(cpu);
                        await ctx.SaveChangesAsync();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Navigates to the GPU form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddGPU(object obj)
        {
            ProductListView.NavigationService.Navigate(new ProductView(new GPU()));
        }

        /// <summary>
        /// Edition is active only when a GPU is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditGPU(object obj)
        {
            if (ProductListView.ProductListUserControl.GPUList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the GPU form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditGPU(object obj)
        {
            ProductView productView = new ProductView((GPU)ProductListView.ProductListUserControl.GPUList.SelectedItem);
            ProductListView.NavigationService.Navigate(productView);
        }

        /// <summary>
        /// Deletion is active only when a GPU is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteGPU(object obj)
        {
            if (ProductListView.ProductListUserControl.GPUList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected GPU.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeleteGPUAsync(object obj)
        {
            GPU gpu = (GPU)ProductListView.ProductListUserControl.GPUList.SelectedItem;
            GPUs.Remove(gpu);

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    await new WebServiceManager<GPU>().DeleteAsync(gpu);
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.DbSetGPUs.Attach(gpu);
                        ctx.DbSetGPUs.Remove(gpu);
                        await ctx.SaveChangesAsync();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Navigates to the motherboard form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddMotherboard(object obj)
        {
            ProductListView.NavigationService.Navigate(new ProductView(new Motherboard()));
        }

        /// <summary>
        /// Edition is active only when a motherboard is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditMotherboard(object obj)
        {
            if (ProductListView.ProductListUserControl.MotherboardList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the motherboard form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditMotherboard(object obj)
        {
            ProductView productView = new ProductView((Motherboard)ProductListView.ProductListUserControl.MotherboardList.SelectedItem);
            ProductListView.NavigationService.Navigate(productView);
        }

        /// <summary>
        /// Deletion is active only when a Motherboard is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteMotherboard(object obj)
        {
            if (ProductListView.ProductListUserControl.MotherboardList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected motherboard.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeleteMotherboardAsync(object obj)
        {
            Motherboard motherboard = (Motherboard)ProductListView.ProductListUserControl.MotherboardList.SelectedItem;
            Motherboards.Remove(motherboard);

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    await new WebServiceManager<Motherboard>().DeleteAsync(motherboard);
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.DbSetMotherboards.Attach(motherboard);
                        ctx.DbSetMotherboards.Remove(motherboard);
                        await ctx.SaveChangesAsync();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Navigates to the memory form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddMemory(object obj)
        {
            ProductListView.NavigationService.Navigate(new ProductView(new Memory()));
        }

        /// <summary>
        /// Edition is active only when a memory component is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditMemory(object obj)
        {
            if (ProductListView.ProductListUserControl.MemoryList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the memory form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditMemory(object obj)
        {
            ProductView productView = new ProductView((Memory)ProductListView.ProductListUserControl.MemoryList.SelectedItem);
            ProductListView.NavigationService.Navigate(productView);
        }

        /// <summary>
        /// Deletion is active only when a memory component is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteMemory(object obj)
        {
            if (ProductListView.ProductListUserControl.MemoryList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected memory.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeleteMemoryAsync(object obj)
        {
            Memory memory = (Memory)ProductListView.ProductListUserControl.MemoryList.SelectedItem;
            Rams.Remove(memory);

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    await new WebServiceManager<Memory>().DeleteAsync(memory);
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.DbSetMemories.Attach(memory);
                        ctx.DbSetMemories.Remove(memory);
                        await ctx.SaveChangesAsync();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Navigates to the storage form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddStorage(object obj)
        {
            ProductListView.NavigationService.Navigate(new ProductView(new Storage()));
        }

        /// <summary>
        /// Edition is active only when a storage component is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditStorage(object obj)
        {
            if (ProductListView.ProductListUserControl.StorageList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the storage form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditStorage(object obj)
        {
            ProductView productView = new ProductView((Storage)ProductListView.ProductListUserControl.StorageList.SelectedItem);
            ProductListView.NavigationService.Navigate(productView);
        }

        /// <summary>
        /// Deletion is active only when a storage component is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteStorage(object obj)
        {
            if (ProductListView.ProductListUserControl.StorageList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected storage component.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeleteStorageAsync(object obj)
        {
            Storage storageComponent = (Storage)ProductListView.ProductListUserControl.StorageList.SelectedItem;
            StorageComponents.Remove(storageComponent);

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    await new WebServiceManager<Storage>().DeleteAsync(storageComponent);
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.DbSetStorages.Attach(storageComponent);
                        ctx.DbSetStorages.Remove(storageComponent);
                        await ctx.SaveChangesAsync();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Navigates to the power supply form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddPSU(object obj)
        {
            ProductListView.NavigationService.Navigate(new ProductView(new PSU()));
        }

        /// <summary>
        /// Edition is active only when a power supply is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditPSU(object obj)
        {
            if (ProductListView.ProductListUserControl.PSUList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the power supply form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditPSU(object obj)
        {
            ProductView productView = new ProductView((PSU)ProductListView.ProductListUserControl.PSUList.SelectedItem);
            ProductListView.NavigationService.Navigate(productView);
        }

        /// <summary>
        /// Deletion is active only when a power supply is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeletePSU(object obj)
        {
            if (ProductListView.ProductListUserControl.PSUList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected power supply.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeletePSUAsync(object obj)
        {
            PSU psu = (PSU)ProductListView.ProductListUserControl.PSUList.SelectedItem;
            PSUs.Remove(psu);

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    await new WebServiceManager<PSU>().DeleteAsync(psu);
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.DbSetPSUs.Attach(psu);
                        ctx.DbSetPSUs.Remove(psu);
                        await ctx.SaveChangesAsync();
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Navigates to the case form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddCase(object obj)
        {
            ProductListView.NavigationService.Navigate(new ProductView(new Case()));
        }

        /// <summary>
        /// Edition is active only when a case is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditCase(object obj)
        {
            if (ProductListView.ProductListUserControl.CaseList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the case form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditCase(object obj)
        {
            ProductView productView = new ProductView((Case)ProductListView.ProductListUserControl.CaseList.SelectedItem);
            ProductListView.NavigationService.Navigate(productView);
        }

        /// <summary>
        /// Deletion is active only when a case is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteCase(object obj)
        {
            if (ProductListView.ProductListUserControl.CaseList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected case.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeleteCaseAsync(object obj)
        {
            Case pcCase = (Case)ProductListView.ProductListUserControl.CaseList.SelectedItem;
            Cases.Remove(pcCase);

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    await new WebServiceManager<Case>().DeleteAsync(pcCase);
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.DbSetCases.Attach(pcCase);
                        ctx.DbSetCases.Remove(pcCase);
                        await ctx.SaveChangesAsync();
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
