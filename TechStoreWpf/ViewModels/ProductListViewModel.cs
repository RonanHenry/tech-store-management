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
        #endregion

        #region Constructors
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
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads all products.
        /// </summary>
        public async void LoadProductsAsync()
        {
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALMYSQL:
                        CPUs = new ObservableCollection<CPU>(await ctx.DbSetCPUs.ToListAsync());
                        GPUs = new ObservableCollection<GPU>(await ctx.DbSetGPUs.ToListAsync());
                        Motherboards = new ObservableCollection<Motherboard>(await ctx.DbSetMotherboards.ToListAsync());
                        Rams = new ObservableCollection<Memory>(await ctx.DbSetMemories.ToListAsync());
                        StorageComponents = new ObservableCollection<Storage>(await ctx.DbSetStorages.ToListAsync());
                        break;
                    default:
                        break;
                }
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
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALMYSQL:
                        CPU cpu = (CPU)ProductListView.ProductListUserControl.CPUList.SelectedItem;
                        CPUs.Remove(cpu);
                        ctx.DbSetCPUs.Attach(cpu);
                        ctx.DbSetCPUs.Remove(cpu);
                        await ctx.SaveChangesAsync();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Navigates to the GPU form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddGPU(object obj)
        {

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
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALMYSQL:
                        GPU gpu = (GPU)ProductListView.ProductListUserControl.GPUList.SelectedItem;
                        GPUs.Remove(gpu);
                        ctx.DbSetGPUs.Attach(gpu);
                        ctx.DbSetGPUs.Remove(gpu);
                        await ctx.SaveChangesAsync();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Navigates to the motherboard form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddMotherboard(object obj)
        {

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
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALMYSQL:
                        Motherboard motherboard = (Motherboard)ProductListView.ProductListUserControl.MotherboardList.SelectedItem;
                        Motherboards.Remove(motherboard);
                        ctx.DbSetMotherboards.Attach(motherboard);
                        ctx.DbSetMotherboards.Remove(motherboard);
                        await ctx.SaveChangesAsync();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Navigates to the memory form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddMemory(object obj)
        {

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
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALMYSQL:
                        Memory memory = (Memory)ProductListView.ProductListUserControl.MemoryList.SelectedItem;
                        Rams.Remove(memory);
                        ctx.DbSetMemories.Attach(memory);
                        ctx.DbSetMemories.Remove(memory);
                        await ctx.SaveChangesAsync();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Navigates to the storage form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddStorage(object obj)
        {

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
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case TechStoreLibrary.Enums.ConnectionResource.LOCALMYSQL:
                        Storage storageComponent = (Storage)ProductListView.ProductListUserControl.StorageList.SelectedItem;
                        StorageComponents.Remove(storageComponent);
                        ctx.DbSetStorages.Attach(storageComponent);
                        ctx.DbSetStorages.Remove(storageComponent);
                        await ctx.SaveChangesAsync();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}
