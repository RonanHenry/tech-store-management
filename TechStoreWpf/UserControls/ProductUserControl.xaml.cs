using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechStoreLibrary.DataDefinitions;
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.Views;

namespace TechStoreWpf.UserControls
{
    /// <summary>
    /// Interaction logic for ProductUserControl.xaml
    /// </summary>
    public partial class ProductUserControl : UserControl
    {
        #region Attributes
        private ProductView productView;
        #endregion

        #region Properties
        /// <summary>
        /// UserControl's view.
        /// </summary>
        public ProductView ProductView
        {
            get
            {
                return productView;
            }
            set
            {
                productView = value;
            }
        }
        #endregion

        #region Constructors
        public ProductUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ProductView = (ProductView)Utility.FindParent<Page>(this);
            SaveProductBtn.DataContext = ProductView.ProductViewModel;
            LoadSpecificControls();
        }

        /// <summary>
        /// Loads appropriate controls depending on the type of the product.
        /// </summary>
        public void LoadSpecificControls()
        {
            Product product = ProductView.ProductViewModel.Product;

            if (product is CPU)
            {
                CPUData cpuData = new CPUData();
                CPU cpuProduct = (CPU)product;

                // Fills the CPU brand ComboBox
                foreach (var brand in cpuData.Brands)
                {
                    ComboBoxItem brandItem = new ComboBoxItem();
                    brandItem.Content = brand;
                    ProductBrandTxt.Items.Add(brandItem);
                }

                // Creates and adds the CPU SocketType Label to the view
                Label cpuSocketTypeLbl = new Label();
                cpuSocketTypeLbl.Content = "Socket";
                cpuSocketTypeLbl.HorizontalAlignment = HorizontalAlignment.Right;
                cpuSocketTypeLbl.VerticalAlignment = VerticalAlignment.Center;
                cpuSocketTypeLbl.FontSize = 14;
                Grid.SetRow(cpuSocketTypeLbl, 1);
                Grid.SetColumn(cpuSocketTypeLbl, 0);
                ProductGrid.Children.Add(cpuSocketTypeLbl);

                // Creates, binds the data and adds the CPU SocketType TextBox to the view
                TextBox cpuSocketTypeTextBox = new TextBox();
                cpuSocketTypeTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                cpuSocketTypeTextBox.VerticalAlignment = VerticalAlignment.Center;
                cpuSocketTypeTextBox.FontSize = 14;
                cpuSocketTypeTextBox.Padding = new Thickness(5);
                cpuSocketTypeTextBox.Margin = new Thickness(0, 0, 10, 0);
                Binding cpuSocketTypeBinding = new Binding()
                {
                    Path = new PropertyPath("SocketType")
                };
                cpuSocketTypeTextBox.SetBinding(TextBox.TextProperty, cpuSocketTypeBinding);
                Grid.SetRow(cpuSocketTypeTextBox, 1);
                Grid.SetColumn(cpuSocketTypeTextBox, 1);
                Grid.SetColumnSpan(cpuSocketTypeTextBox, 3);
                ProductGrid.Children.Add(cpuSocketTypeTextBox);

                // Creates and adds the CPU CoresAmount Label to the view
                Label cpuCoresAmountLbl = new Label();
                cpuCoresAmountLbl.Content = "Cores";
                cpuCoresAmountLbl.HorizontalAlignment = HorizontalAlignment.Right;
                cpuCoresAmountLbl.VerticalAlignment = VerticalAlignment.Center;
                cpuCoresAmountLbl.FontSize = 14;
                Grid.SetRow(cpuCoresAmountLbl, 1);
                Grid.SetColumn(cpuCoresAmountLbl, 4);
                ProductGrid.Children.Add(cpuCoresAmountLbl);

                // Creates, binds the data and adds the CPU CoresAmount TextBox to the view
                TextBox cpuCoresAmountTextBox = new TextBox();
                cpuCoresAmountTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                cpuCoresAmountTextBox.VerticalAlignment = VerticalAlignment.Center;
                cpuCoresAmountTextBox.FontSize = 14;
                cpuCoresAmountTextBox.Padding = new Thickness(5);
                cpuCoresAmountTextBox.Margin = new Thickness(0, 0, 10, 0);
                Binding cpuCoresAmountBinding = new Binding()
                {
                    Path = new PropertyPath("CoresAmount")
                };
                cpuCoresAmountTextBox.SetBinding(TextBox.TextProperty, cpuCoresAmountBinding);
                cpuCoresAmountTextBox.PreviewKeyDown += NoSpace_PreviewKeyDown;
                cpuCoresAmountTextBox.PreviewTextInput += OnlyNumbers_PreviewTextInput;
                Grid.SetRow(cpuCoresAmountTextBox, 1);
                Grid.SetColumn(cpuCoresAmountTextBox, 5);
                ProductGrid.Children.Add(cpuCoresAmountTextBox);

                // Creates and adds the CPU Frequency Label to the view
                Label cpuFrequencyLbl = new Label();
                cpuFrequencyLbl.Content = "Clock speed (GHz)";
                cpuFrequencyLbl.HorizontalAlignment = HorizontalAlignment.Right;
                cpuFrequencyLbl.VerticalAlignment = VerticalAlignment.Center;
                cpuFrequencyLbl.FontSize = 14;
                Grid.SetRow(cpuFrequencyLbl, 1);
                Grid.SetColumn(cpuFrequencyLbl, 6);
                ProductGrid.Children.Add(cpuFrequencyLbl);

                // Creates, binds the data and adds the CPU Frequency TextBox to the view
                TextBox cpuFrequencyTextBox = new TextBox();
                cpuFrequencyTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                cpuFrequencyTextBox.VerticalAlignment = VerticalAlignment.Center;
                cpuFrequencyTextBox.FontSize = 14;
                cpuFrequencyTextBox.Padding = new Thickness(5);
                Binding cpuFrequencyBinding = new Binding()
                {
                    Path = new PropertyPath("Frequency")
                };
                cpuFrequencyTextBox.SetBinding(TextBox.TextProperty, cpuFrequencyBinding);
                cpuFrequencyTextBox.PreviewKeyDown += NoSpace_PreviewKeyDown;
                cpuFrequencyTextBox.PreviewTextInput += OnlyNumbersPeriods_PreviewTextInput;
                Grid.SetRow(cpuFrequencyTextBox, 1);
                Grid.SetColumn(cpuFrequencyTextBox, 7);
                ProductGrid.Children.Add(cpuFrequencyTextBox);

                // Sets active tab of the product list after save or cancel actions
                ProductBtns.Tag = cpuProduct.GetType().Name;
            }
            else if (product is GPU)
            {
                GPUData gpuData = new GPUData();
                GPU gpuProduct = (GPU)product;

                // Creates and adds the GPU ChipsetMaker Label to the view
                Label gpuChipsetMakerLbl = new Label();
                gpuChipsetMakerLbl.Content = "Chipset Maker";
                gpuChipsetMakerLbl.HorizontalAlignment = HorizontalAlignment.Right;
                gpuChipsetMakerLbl.VerticalAlignment = VerticalAlignment.Center;
                gpuChipsetMakerLbl.FontSize = 14;
                Grid.SetRow(gpuChipsetMakerLbl, 0);
                Grid.SetColumn(gpuChipsetMakerLbl, 0);
                ProductGrid.Children.Add(gpuChipsetMakerLbl);

                // Creates and styles the GPU ChipsetMaker ComboBox
                ComboBox gpuChipsetMakerComboBox = new ComboBox();
                gpuChipsetMakerComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                gpuChipsetMakerComboBox.VerticalAlignment = VerticalAlignment.Center;
                gpuChipsetMakerComboBox.FontSize = 14;
                gpuChipsetMakerComboBox.Padding = new Thickness(6);
                gpuChipsetMakerComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Storage Type ComboBox
                foreach (var chipsetMaker in gpuData.ChipsetMakers)
                {
                    ComboBoxItem typeItem = new ComboBoxItem();
                    typeItem.Content = chipsetMaker;
                    gpuChipsetMakerComboBox.Items.Add(typeItem);
                }

                // Binds the data and adds the Storage Type ComboBox to the view
                gpuChipsetMakerComboBox.SelectedValuePath = "Content";
                Binding gpuChipsetMakerBinding = new Binding()
                {
                    Path = new PropertyPath("ChipsetMaker")
                };
                gpuChipsetMakerComboBox.SetBinding(ComboBox.SelectedValueProperty, gpuChipsetMakerBinding);
                Grid.SetRow(gpuChipsetMakerComboBox, 0);
                Grid.SetColumn(gpuChipsetMakerComboBox, 1);
                ProductGrid.Children.Add(gpuChipsetMakerComboBox);

                // Fills the GPU Brand ComboBox
                foreach (var brand in gpuData.Brands)
                {
                    ComboBoxItem brandItem = new ComboBoxItem();
                    brandItem.Content = brand;
                    ProductBrandTxt.Items.Add(brandItem);
                }

                // Changes position of Brand
                Grid.SetColumn(ProductBrandLbl, 2);
                Grid.SetColumn(ProductBrandTxt, 3);
                Grid.SetColumnSpan(ProductBrandTxt, 1);

                // Changes position of Name
                Grid.SetColumn(ProductNameLbl, 4);
                Grid.SetColumn(ProductNameTxt, 5);
                Grid.SetColumnSpan(ProductNameTxt, 1);
                ProductNameTxt.Margin = new Thickness(0, 0, 10, 0);

                // Creates and adds the GPU MemoryAmount Label to the view
                Label gpuMemoryAmountLbl = new Label();
                gpuMemoryAmountLbl.Content = "Memory (GB)";
                gpuMemoryAmountLbl.HorizontalAlignment = HorizontalAlignment.Right;
                gpuMemoryAmountLbl.VerticalAlignment = VerticalAlignment.Center;
                gpuMemoryAmountLbl.FontSize = 14;
                Grid.SetRow(gpuMemoryAmountLbl, 0);
                Grid.SetColumn(gpuMemoryAmountLbl, 6);
                ProductGrid.Children.Add(gpuMemoryAmountLbl);

                // Creates and styles the GPU MemoryAmount ComboBox
                ComboBox gpuMemoryAmountComboBox = new ComboBox();
                gpuMemoryAmountComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                gpuMemoryAmountComboBox.VerticalAlignment = VerticalAlignment.Center;
                gpuMemoryAmountComboBox.FontSize = 14;
                gpuMemoryAmountComboBox.Padding = new Thickness(6);

                // Fills the GPU MemoryAmount ComboBox
                foreach (var memory in gpuData.MemoryAmount)
                {
                    ComboBoxItem memoryItem = new ComboBoxItem();
                    memoryItem.Content = memory;
                    gpuMemoryAmountComboBox.Items.Add(memoryItem);
                }

                // Binds the data and adds the GPU MemoryAmount ComboBox to the view
                gpuMemoryAmountComboBox.SelectedValuePath = "Content";
                Binding gpuMemoryAmountBinding = new Binding()
                {
                    Path = new PropertyPath("MemoryAmount")
                };
                gpuMemoryAmountComboBox.SetBinding(ComboBox.SelectedValueProperty, gpuMemoryAmountBinding);
                Grid.SetRow(gpuMemoryAmountComboBox, 0);
                Grid.SetColumn(gpuMemoryAmountComboBox, 7);
                ProductGrid.Children.Add(gpuMemoryAmountComboBox);

                // Changes position of Description
                Grid.SetRow(DescContainer, 1);

                // Creates and adds the GPU VrReady Label to the view
                Label gpuVrReadyLbl = new Label();
                gpuVrReadyLbl.Content = "VR Ready";
                gpuVrReadyLbl.HorizontalAlignment = HorizontalAlignment.Right;
                gpuVrReadyLbl.VerticalAlignment = VerticalAlignment.Center;
                gpuVrReadyLbl.FontSize = 14;
                Grid.SetRow(gpuVrReadyLbl, 2);
                Grid.SetColumn(gpuVrReadyLbl, 0);
                ProductGrid.Children.Add(gpuVrReadyLbl);

                // Creates, binds the data and adds the GPU VrReady CheckBox to the view
                CheckBox gpuVrReadyCheckBox = new CheckBox();
                gpuVrReadyCheckBox.HorizontalAlignment = HorizontalAlignment.Left;
                gpuVrReadyCheckBox.VerticalAlignment = VerticalAlignment.Center;
                Binding gpuVrReadyBinding = new Binding()
                {
                    Path = new PropertyPath("VrReady")
                };
                gpuVrReadyCheckBox.SetBinding(CheckBox.IsCheckedProperty, gpuVrReadyBinding);
                Grid.SetRow(gpuVrReadyCheckBox, 2);
                Grid.SetColumn(gpuVrReadyCheckBox, 1);
                ProductGrid.Children.Add(gpuVrReadyCheckBox);

                // Changes position of Condition
                Grid.SetRow(ProductConditionLbl, 2);
                Grid.SetColumn(ProductConditionLbl, 2);
                Grid.SetRow(ProductConditionTxt, 2);
                Grid.SetColumn(ProductConditionTxt, 3);
                Grid.SetColumnSpan(ProductConditionTxt, 1);

                // Changes position of Stock
                Grid.SetRow(ProductStockLbl, 2);
                Grid.SetRow(ProductStockTxt, 2);

                // Changes position of Price
                Grid.SetRow(ProductPriceLbl, 2);
                Grid.SetRow(ProductPriceTxt, 2);

                // Sets active tab of the product list after save or cancel actions
                ProductBtns.Tag = gpuProduct.GetType().Name;
            }
            else if (product is Motherboard)
            {
                MotherboardData motherboardData = new MotherboardData();
                Motherboard motherboardProduct = (Motherboard)product;

                // Creates and adds the Motherboard Platform Label to the view
                Label motherboardPlatformLabel = new Label();
                motherboardPlatformLabel.Content = "Platform";
                motherboardPlatformLabel.HorizontalAlignment = HorizontalAlignment.Right;
                motherboardPlatformLabel.VerticalAlignment = VerticalAlignment.Center;
                motherboardPlatformLabel.FontSize = 14;
                Grid.SetRow(motherboardPlatformLabel, 0);
                Grid.SetColumn(motherboardPlatformLabel, 0);
                ProductGrid.Children.Add(motherboardPlatformLabel);

                // Creates and styles the Motherboard Platform ComboBox
                ComboBox motherboardPlatformComboBox = new ComboBox();
                motherboardPlatformComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                motherboardPlatformComboBox.VerticalAlignment = VerticalAlignment.Center;
                motherboardPlatformComboBox.FontSize = 14;
                motherboardPlatformComboBox.Padding = new Thickness(6);
                motherboardPlatformComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Motherboard Platform ComboBox
                foreach (var platform in motherboardData.Platforms)
                {
                    ComboBoxItem platformItem = new ComboBoxItem();
                    platformItem.Content = platform;
                    motherboardPlatformComboBox.Items.Add(platformItem);
                }

                // Binds the data and adds the Motherboard Platform ComboBox to the view
                motherboardPlatformComboBox.SelectedValuePath = "Content";
                Binding motherboardPlatformBinding = new Binding()
                {
                    Path = new PropertyPath("Platform"),
                    NotifyOnTargetUpdated = true
                };
                motherboardPlatformComboBox.SetBinding(ComboBox.SelectedValueProperty, motherboardPlatformBinding);
                motherboardPlatformComboBox.TargetUpdated += MotherboardPlatformUpdated;
                motherboardPlatformComboBox.SelectionChanged += MotherboardPlatformChanged;
                Grid.SetRow(motherboardPlatformComboBox, 0);
                Grid.SetColumn(motherboardPlatformComboBox, 1);
                ProductGrid.Children.Add(motherboardPlatformComboBox);

                // Creates and adds the Motherboard Chipset Label to the view
                Label motherboardChipsetLabel = new Label();
                motherboardChipsetLabel.Content = "Chipset";
                motherboardChipsetLabel.HorizontalAlignment = HorizontalAlignment.Right;
                motherboardChipsetLabel.VerticalAlignment = VerticalAlignment.Center;
                motherboardChipsetLabel.FontSize = 14;
                Grid.SetRow(motherboardChipsetLabel, 0);
                Grid.SetColumn(motherboardChipsetLabel, 2);
                ProductGrid.Children.Add(motherboardChipsetLabel);

                // Creates, styles, binds the data and adds the Motherboard Chipset ComboBox to the view
                ComboBox motherboardChipsetComboBox = new ComboBox();
                motherboardChipsetComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                motherboardChipsetComboBox.VerticalAlignment = VerticalAlignment.Center;
                motherboardChipsetComboBox.FontSize = 14;
                motherboardChipsetComboBox.Padding = new Thickness(6);
                motherboardChipsetComboBox.Margin = new Thickness(0, 0, 10, 0);
                RegisterName("motherboardChipsetComboBox", motherboardChipsetComboBox);
                motherboardChipsetComboBox.SelectedValuePath = "Content";
                Binding motherboardChipsetBinding = new Binding()
                {
                    Path = new PropertyPath("Chipset")
                };
                motherboardChipsetComboBox.SetBinding(ComboBox.SelectedValueProperty, motherboardChipsetBinding);
                Grid.SetRow(motherboardChipsetComboBox, 0);
                Grid.SetColumn(motherboardChipsetComboBox, 3);
                Grid.SetColumnSpan(motherboardChipsetComboBox, 3);
                ProductGrid.Children.Add(motherboardChipsetComboBox);

                // Creates and adds the Motherboard CPUSocket Label to the view
                Label motherboardCpuSocketLabel = new Label();
                motherboardCpuSocketLabel.Content = "CPU Socket";
                motherboardCpuSocketLabel.HorizontalAlignment = HorizontalAlignment.Right;
                motherboardCpuSocketLabel.VerticalAlignment = VerticalAlignment.Center;
                motherboardCpuSocketLabel.FontSize = 14;
                Grid.SetRow(motherboardCpuSocketLabel, 0);
                Grid.SetColumn(motherboardCpuSocketLabel, 6);
                ProductGrid.Children.Add(motherboardCpuSocketLabel);

                // Creates, styles, binds the data and adds the Motherboard CPUSocket ComboBox to the view
                ComboBox motherboardCpuSocketComboBox = new ComboBox();
                motherboardCpuSocketComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                motherboardCpuSocketComboBox.VerticalAlignment = VerticalAlignment.Center;
                motherboardCpuSocketComboBox.FontSize = 14;
                motherboardCpuSocketComboBox.Padding = new Thickness(6);
                RegisterName("motherboardCpuSocketComboBox", motherboardCpuSocketComboBox);
                motherboardCpuSocketComboBox.SelectedValuePath = "Content";
                Binding motherboardCpuSocketBinding = new Binding()
                {
                    Path = new PropertyPath("CPUSocket")
                };
                motherboardCpuSocketComboBox.SetBinding(ComboBox.SelectedValueProperty, motherboardCpuSocketBinding);
                Grid.SetRow(motherboardCpuSocketComboBox, 0);
                Grid.SetColumn(motherboardCpuSocketComboBox, 7);
                ProductGrid.Children.Add(motherboardCpuSocketComboBox);

                // Changes position of Motherboard Brand
                Grid.SetRow(ProductBrandLbl, 1);
                Grid.SetRow(ProductBrandTxt, 1);
                Grid.SetColumnSpan(ProductBrandTxt, 1);

                // Fills the Motherboard Brand ComboBox
                foreach (var brand in motherboardData.Brands)
                {
                    ComboBoxItem brandItem = new ComboBoxItem();
                    brandItem.Content = brand;
                    ProductBrandTxt.Items.Add(brandItem);
                }

                // Changes position of Motherboard Name
                Grid.SetRow(ProductNameLbl, 1);
                Grid.SetColumn(ProductNameLbl, 2);
                Grid.SetRow(ProductNameTxt, 1);
                Grid.SetColumn(ProductNameTxt, 3);
                ProductNameTxt.Margin = new Thickness(0, 0, 10, 0);

                // Creates and adds the Motherboard Usage Label to the view
                Label motherboardUsageLabel = new Label();
                motherboardUsageLabel.Content = "Usage";
                motherboardUsageLabel.HorizontalAlignment = HorizontalAlignment.Right;
                motherboardUsageLabel.VerticalAlignment = VerticalAlignment.Center;
                motherboardUsageLabel.FontSize = 14;
                Grid.SetRow(motherboardUsageLabel, 1);
                Grid.SetColumn(motherboardUsageLabel, 6);
                ProductGrid.Children.Add(motherboardUsageLabel);

                // Creates and styles the Motherboard Usage ComboBox
                ComboBox motherboardUsageComboBox = new ComboBox();
                motherboardUsageComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                motherboardUsageComboBox.VerticalAlignment = VerticalAlignment.Center;
                motherboardUsageComboBox.FontSize = 14;
                motherboardUsageComboBox.Padding = new Thickness(6);

                // Fills the Motherboard Usage ComboBox
                foreach (var usage in motherboardData.Uses)
                {
                    ComboBoxItem usageItem = new ComboBoxItem();
                    usageItem.Content = usage;
                    motherboardUsageComboBox.Items.Add(usageItem);
                }

                // Binds the data and adds the Motherboard Usage ComboBox to the view
                motherboardUsageComboBox.SelectedValuePath = "Content";
                Binding motherboardUsageBinding = new Binding()
                {
                    Path = new PropertyPath("Usage")
                };
                motherboardUsageComboBox.SetBinding(ComboBox.SelectedValueProperty, motherboardUsageBinding);
                Grid.SetRow(motherboardUsageComboBox, 1);
                Grid.SetColumn(motherboardUsageComboBox, 7);
                ProductGrid.Children.Add(motherboardUsageComboBox);

                // Sets active tab of the product list after save or cancel actions
                ProductBtns.Tag = motherboardProduct.GetType().Name;
            }
            else if (product is Memory)
            {
                MemoryData memoryData = new MemoryData();
                Memory memoryProduct = (Memory)product;

                // Fills the Memory Brand ComboBox
                foreach (var brand in memoryData.Brands)
                {
                    ComboBoxItem brandItem = new ComboBoxItem();
                    brandItem.Content = brand;
                    ProductBrandTxt.Items.Add(brandItem);
                }

                // Creates and adds the Memory Type Label to the view
                Label memoryTypeLbl = new Label();
                memoryTypeLbl.Content = "Type";
                memoryTypeLbl.HorizontalAlignment = HorizontalAlignment.Right;
                memoryTypeLbl.VerticalAlignment = VerticalAlignment.Center;
                memoryTypeLbl.FontSize = 14;
                Grid.SetRow(memoryTypeLbl, 1);
                Grid.SetColumn(memoryTypeLbl, 0);
                ProductGrid.Children.Add(memoryTypeLbl);

                // Creates and styles the Memory Type ComboBox
                ComboBox memoryTypeComboBox = new ComboBox();
                memoryTypeComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                memoryTypeComboBox.VerticalAlignment = VerticalAlignment.Center;
                memoryTypeComboBox.FontSize = 14;
                memoryTypeComboBox.Padding = new Thickness(6);
                memoryTypeComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Memory Type ComboBox
                foreach (var type in memoryData.Types)
                {
                    ComboBoxItem typeItem = new ComboBoxItem();
                    typeItem.Content = type;
                    memoryTypeComboBox.Items.Add(typeItem);
                }

                // Binds the data and adds the Memory Type ComboBox to the view
                memoryTypeComboBox.SelectedValuePath = "Content";
                Binding memoryTypeBinding = new Binding()
                {
                    Path = new PropertyPath("Type")
                };
                memoryTypeComboBox.SetBinding(ComboBox.SelectedValueProperty, memoryTypeBinding);
                Grid.SetRow(memoryTypeComboBox, 1);
                Grid.SetColumn(memoryTypeComboBox, 1);
                ProductGrid.Children.Add(memoryTypeComboBox);

                // Creates and adds the Memory Capacity Label to the view
                Label memoryCapacityLbl = new Label();
                memoryCapacityLbl.Content = "Capacity (GB)";
                memoryCapacityLbl.HorizontalAlignment = HorizontalAlignment.Right;
                memoryCapacityLbl.VerticalAlignment = VerticalAlignment.Center;
                memoryCapacityLbl.FontSize = 14;
                Grid.SetRow(memoryCapacityLbl, 1);
                Grid.SetColumn(memoryCapacityLbl, 2);
                ProductGrid.Children.Add(memoryCapacityLbl);

                // Creates and styles the Memory Capacity ComboBox
                ComboBox memoryCapacityComboBox = new ComboBox();
                memoryCapacityComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                memoryCapacityComboBox.VerticalAlignment = VerticalAlignment.Center;
                memoryCapacityComboBox.FontSize = 14;
                memoryCapacityComboBox.Padding = new Thickness(6);
                memoryCapacityComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Memory Capacity ComboBox
                foreach (var capacity in memoryData.Capacities)
                {
                    ComboBoxItem capacityItem = new ComboBoxItem();
                    capacityItem.Content = capacity;
                    memoryCapacityComboBox.Items.Add(capacityItem);
                }

                // Binds the data and adds the Memory Capacity ComboBox to the view
                memoryCapacityComboBox.SelectedValuePath = "Content";
                Binding memoryCapacityBinding = new Binding()
                {
                    Path = new PropertyPath("Capacity")
                };
                memoryCapacityComboBox.SetBinding(ComboBox.SelectedValueProperty, memoryCapacityBinding);
                Grid.SetRow(memoryCapacityComboBox, 1);
                Grid.SetColumn(memoryCapacityComboBox, 3);
                ProductGrid.Children.Add(memoryCapacityComboBox);

                // Creates and adds the Memory Speed Label to the view
                Label memorySpeedLbl = new Label();
                memorySpeedLbl.Content = "Speed (MHz)";
                memorySpeedLbl.HorizontalAlignment = HorizontalAlignment.Right;
                memorySpeedLbl.VerticalAlignment = VerticalAlignment.Center;
                memorySpeedLbl.FontSize = 14;
                Grid.SetRow(memorySpeedLbl, 1);
                Grid.SetColumn(memorySpeedLbl, 4);
                ProductGrid.Children.Add(memorySpeedLbl);

                // Creates and styles the Memory Speed ComboBox
                ComboBox memorySpeedComboBox = new ComboBox();
                memorySpeedComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                memorySpeedComboBox.VerticalAlignment = VerticalAlignment.Center;
                memorySpeedComboBox.FontSize = 14;
                memorySpeedComboBox.Padding = new Thickness(6);
                memorySpeedComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Memory Speed ComboBox
                foreach (var speed in memoryData.Speeds)
                {
                    ComboBoxItem speedItem = new ComboBoxItem();
                    speedItem.Content = speed;
                    memorySpeedComboBox.Items.Add(speedItem);
                }

                // Binds the data and adds the Memory Speed ComboBox to the view
                memorySpeedComboBox.SelectedValuePath = "Content";
                Binding memorySpeedBinding = new Binding()
                {
                    Path = new PropertyPath("Speed")
                };
                memorySpeedComboBox.SetBinding(ComboBox.SelectedValueProperty, memorySpeedBinding);
                Grid.SetRow(memorySpeedComboBox, 1);
                Grid.SetColumn(memorySpeedComboBox, 5);
                ProductGrid.Children.Add(memorySpeedComboBox);

                // Creates and adds the Memory Latency Label to the view
                Label memoryLatencyLbl = new Label();
                memoryLatencyLbl.Content = "Latency";
                memoryLatencyLbl.HorizontalAlignment = HorizontalAlignment.Right;
                memoryLatencyLbl.VerticalAlignment = VerticalAlignment.Center;
                memoryLatencyLbl.FontSize = 14;
                Grid.SetRow(memoryLatencyLbl, 1);
                Grid.SetColumn(memoryLatencyLbl, 6);
                ProductGrid.Children.Add(memoryLatencyLbl);

                // Creates, styles, binds the data and adds the Memory Latency TextBox to the view
                TextBox memoryLatencyTextBox = new TextBox();
                memoryLatencyTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                memoryLatencyTextBox.VerticalAlignment = VerticalAlignment.Center;
                memoryLatencyTextBox.FontSize = 14;
                memoryLatencyTextBox.Padding = new Thickness(5);
                Binding memoryLatencyBinding = new Binding()
                {
                    Path = new PropertyPath("Latency")
                };
                memoryLatencyTextBox.SetBinding(TextBox.TextProperty, memoryLatencyBinding);
                memoryLatencyTextBox.PreviewKeyDown += NoSpace_PreviewKeyDown;
                memoryLatencyTextBox.PreviewTextInput += OnlyNumbers_PreviewTextInput;
                Grid.SetRow(memoryLatencyTextBox, 1);
                Grid.SetColumn(memoryLatencyTextBox, 7);
                ProductGrid.Children.Add(memoryLatencyTextBox);

                // Sets active tab of the product list after save or cancel actions
                ProductBtns.Tag = memoryProduct.GetType().Name;
            }
            else if (product is Storage)
            {
                StorageData storageData = new StorageData();
                Storage storageProduct = (Storage)product;

                // Creates and adds the Storage Type Label to the view
                Label storageTypeLbl = new Label();
                storageTypeLbl.Content = "Type";
                storageTypeLbl.HorizontalAlignment = HorizontalAlignment.Right;
                storageTypeLbl.VerticalAlignment = VerticalAlignment.Center;
                storageTypeLbl.FontSize = 14;
                Grid.SetRow(storageTypeLbl, 0);
                Grid.SetColumn(storageTypeLbl, 0);
                ProductGrid.Children.Add(storageTypeLbl);

                // Creates and styles the Storage Type ComboBox
                ComboBox storageTypeComboBox = new ComboBox();
                storageTypeComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                storageTypeComboBox.VerticalAlignment = VerticalAlignment.Center;
                storageTypeComboBox.FontSize = 14;
                storageTypeComboBox.Padding = new Thickness(6);
                storageTypeComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Storage Type ComboBox
                foreach (var type in storageData.Types)
                {
                    ComboBoxItem typeItem = new ComboBoxItem();
                    typeItem.Content = type;
                    storageTypeComboBox.Items.Add(typeItem);
                }

                // Binds the data and adds the Storage Type ComboBox to the view
                storageTypeComboBox.SelectedValuePath = "Content";
                Binding productTypeBinding = new Binding()
                {
                    Path = new PropertyPath("Type"),
                    NotifyOnTargetUpdated = true
                };
                storageTypeComboBox.SetBinding(ComboBox.SelectedValueProperty, productTypeBinding);
                storageTypeComboBox.TargetUpdated += StorageTypeUpdated;
                storageTypeComboBox.SelectionChanged += StorageTypeChanged;
                Grid.SetRow(storageTypeComboBox, 0);
                Grid.SetColumn(storageTypeComboBox, 1);
                ProductGrid.Children.Add(storageTypeComboBox);

                // Changes position of Brand ComboBox
                Grid.SetColumn(ProductBrandLbl, 2);
                Grid.SetColumn(ProductBrandTxt, 3);
                Grid.SetColumnSpan(ProductBrandTxt, 1);

                // Changes position of Name control
                Grid.SetColumn(ProductNameLbl, 4);
                Grid.SetColumn(ProductNameTxt, 5);

                // Creates and adds the Storage Capacity Label to the view.
                Label storageCapacityLbl = new Label();
                storageCapacityLbl.Content = "Capacity (GB)";
                storageCapacityLbl.HorizontalAlignment = HorizontalAlignment.Right;
                storageCapacityLbl.VerticalAlignment = VerticalAlignment.Center;
                storageCapacityLbl.FontSize = 14;
                Grid.SetRow(storageCapacityLbl, 1);
                Grid.SetColumn(storageCapacityLbl, 0);
                ProductGrid.Children.Add(storageCapacityLbl);

                // Creates and styles the Storage Capacity ComboBox
                ComboBox storageCapacityComboBox = new ComboBox();
                storageCapacityComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                storageCapacityComboBox.VerticalAlignment = VerticalAlignment.Center;
                storageCapacityComboBox.FontSize = 14;
                storageCapacityComboBox.Padding = new Thickness(6);
                storageCapacityComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Storage Capacity ComboBox
                foreach (var capacity in storageData.Capacities)
                {
                    ComboBoxItem capacityItem = new ComboBoxItem();
                    capacityItem.Content = capacity;
                    storageCapacityComboBox.Items.Add(capacityItem);
                }

                // Binds the data and adds the Storage Capacity ComboBox to the view
                storageCapacityComboBox.SelectedValuePath = "Content";
                Binding storageCapacityBinding = new Binding()
                {
                    Path = new PropertyPath("Capacity")
                };
                storageCapacityComboBox.SetBinding(ComboBox.SelectedValueProperty, storageCapacityBinding);
                Grid.SetRow(storageCapacityComboBox, 1);
                Grid.SetColumn(storageCapacityComboBox, 1);
                Grid.SetColumnSpan(storageCapacityComboBox, 3);
                ProductGrid.Children.Add(storageCapacityComboBox);

                // Creates and adds the Storage Speed Label to the view.
                Label storageSpeedLbl = new Label();
                storageSpeedLbl.Content = "Speed";
                storageSpeedLbl.HorizontalAlignment = HorizontalAlignment.Right;
                storageSpeedLbl.VerticalAlignment = VerticalAlignment.Center;
                storageSpeedLbl.FontSize = 14;
                Grid.SetRow(storageSpeedLbl, 1);
                Grid.SetColumn(storageSpeedLbl, 4);
                ProductGrid.Children.Add(storageSpeedLbl);

                // Creates, binds the data and adds the Storage Speed TextBox to the view
                TextBox storageSpeedTextBox = new TextBox();
                storageSpeedTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                storageSpeedTextBox.VerticalAlignment = VerticalAlignment.Center;
                storageSpeedTextBox.FontSize = 14;
                storageSpeedTextBox.Padding = new Thickness(5);
                Binding storageSpeedBinding = new Binding()
                {
                    Path = new PropertyPath("Speed")
                };
                storageSpeedTextBox.SetBinding(TextBox.TextProperty, storageSpeedBinding);
                Grid.SetRow(storageSpeedTextBox, 1);
                Grid.SetColumn(storageSpeedTextBox, 5);
                Grid.SetColumnSpan(storageSpeedTextBox, 3);
                ProductGrid.Children.Add(storageSpeedTextBox);

                // Sets active tab of the product list after save or cancel actions
                ProductBtns.Tag = storageProduct.GetType().Name;
            }
            else if (product is PSU)
            {
                PSUData psuData = new PSUData();
                PSU psuProduct = (PSU)product;

                // Fills the PSU Brand ComboBox
                foreach (var brand in psuData.Brands)
                {
                    ComboBoxItem brandItem = new ComboBoxItem();
                    brandItem.Content = brand;
                    ProductBrandTxt.Items.Add(brandItem);
                }

                // Creates and adds the PSU Rating Label to the view.
                Label psuRatingLbl = new Label();
                psuRatingLbl.Content = "Rating";
                psuRatingLbl.HorizontalAlignment = HorizontalAlignment.Right;
                psuRatingLbl.VerticalAlignment = VerticalAlignment.Center;
                psuRatingLbl.FontSize = 14;
                Grid.SetRow(psuRatingLbl, 1);
                Grid.SetColumn(psuRatingLbl, 0);
                ProductGrid.Children.Add(psuRatingLbl);

                // Creates and styles the PSU Rating ComboBox
                ComboBox psuRatingComboBox = new ComboBox();
                psuRatingComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                psuRatingComboBox.VerticalAlignment = VerticalAlignment.Center;
                psuRatingComboBox.FontSize = 14;
                psuRatingComboBox.Padding = new Thickness(6);
                psuRatingComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the PSU Rating ComboBox
                foreach (var rating in psuData.Ratings)
                {
                    ComboBoxItem ratingItem = new ComboBoxItem();
                    ratingItem.Content = rating;
                    psuRatingComboBox.Items.Add(ratingItem);
                }

                // Binds the data and adds the PSU Rating ComboBox to the view
                psuRatingComboBox.SelectedValuePath = "Content";
                Binding psuRatingBinding = new Binding()
                {
                    Path = new PropertyPath("Rating")
                };
                psuRatingComboBox.SetBinding(ComboBox.SelectedValueProperty, psuRatingBinding);
                Grid.SetRow(psuRatingComboBox, 1);
                Grid.SetColumn(psuRatingComboBox, 1);
                Grid.SetColumnSpan(psuRatingComboBox, 3);
                ProductGrid.Children.Add(psuRatingComboBox);

                // Creates and adds the PSU Power Label to the view.
                Label psuPowerLbl = new Label();
                psuPowerLbl.Content = "Power (W)";
                psuPowerLbl.HorizontalAlignment = HorizontalAlignment.Right;
                psuPowerLbl.VerticalAlignment = VerticalAlignment.Center;
                psuPowerLbl.FontSize = 14;
                Grid.SetRow(psuPowerLbl, 1);
                Grid.SetColumn(psuPowerLbl, 4);
                ProductGrid.Children.Add(psuPowerLbl);

                // Creates and styles the PSU Power ComboBox
                ComboBox psuPowerComboBox = new ComboBox();
                psuPowerComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                psuPowerComboBox.VerticalAlignment = VerticalAlignment.Center;
                psuPowerComboBox.FontSize = 14;
                psuPowerComboBox.Padding = new Thickness(6);
                psuPowerComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the PSU Power ComboBox
                foreach (var power in psuData.Powers)
                {
                    ComboBoxItem powerItem = new ComboBoxItem();
                    powerItem.Content = power;
                    psuPowerComboBox.Items.Add(powerItem);
                }

                // Binds the data and adds the PSU Power ComboBox to the view
                psuPowerComboBox.SelectedValuePath = "Content";
                Binding psuPowerBinding = new Binding()
                {
                    Path = new PropertyPath("Power")
                };
                psuPowerComboBox.SetBinding(ComboBox.SelectedValueProperty, psuPowerBinding);
                Grid.SetRow(psuPowerComboBox, 1);
                Grid.SetColumn(psuPowerComboBox, 5);
                ProductGrid.Children.Add(psuPowerComboBox);

                // Creates and adds the PSU IsModular Label to the view.
                Label psuIsModularLbl = new Label();
                psuIsModularLbl.Content = "Modular";
                psuIsModularLbl.HorizontalAlignment = HorizontalAlignment.Right;
                psuIsModularLbl.VerticalAlignment = VerticalAlignment.Center;
                psuIsModularLbl.FontSize = 14;
                Grid.SetRow(psuIsModularLbl, 1);
                Grid.SetColumn(psuIsModularLbl, 6);
                ProductGrid.Children.Add(psuIsModularLbl);

                // Creates, binds the data and adds the PSU IsModular CheckBox to the view
                CheckBox psuIsModularCheckBox = new CheckBox();
                psuIsModularCheckBox.HorizontalAlignment = HorizontalAlignment.Left;
                psuIsModularCheckBox.VerticalAlignment = VerticalAlignment.Center;
                Binding psuIsModularBinding = new Binding()
                {
                    Path = new PropertyPath("IsModular")
                };
                psuIsModularCheckBox.SetBinding(CheckBox.IsCheckedProperty, psuIsModularBinding);
                Grid.SetRow(psuIsModularCheckBox, 1);
                Grid.SetColumn(psuIsModularCheckBox, 7);
                ProductGrid.Children.Add(psuIsModularCheckBox);

                // Sets active tab of the product list after save or cancel actions
                ProductBtns.Tag = psuProduct.GetType().Name;
            }
            else // Case
            {
                CaseData caseData = new CaseData();
                Case caseProduct = (Case)product;

                // Fills the Case Brand ComboBox
                foreach (var brand in caseData.Brands)
                {
                    ComboBoxItem brandItem = new ComboBoxItem();
                    brandItem.Content = brand;
                    ProductBrandTxt.Items.Add(brandItem);
                }

                // Creates and adds the Case Type Label to the view
                Label caseTypeLbl = new Label();
                caseTypeLbl.Content = "Type";
                caseTypeLbl.HorizontalAlignment = HorizontalAlignment.Right;
                caseTypeLbl.VerticalAlignment = VerticalAlignment.Center;
                caseTypeLbl.FontSize = 14;
                Grid.SetRow(caseTypeLbl, 1);
                Grid.SetColumn(caseTypeLbl, 0);
                ProductGrid.Children.Add(caseTypeLbl);

                // Creates and styles the Case Type ComboBox
                ComboBox caseTypeComboBox = new ComboBox();
                caseTypeComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                caseTypeComboBox.VerticalAlignment = VerticalAlignment.Center;
                caseTypeComboBox.FontSize = 14;
                caseTypeComboBox.Padding = new Thickness(6);
                caseTypeComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Case Type ComboBox
                foreach (var type in caseData.Types)
                {
                    ComboBoxItem typeBoxItem = new ComboBoxItem();
                    typeBoxItem.Content = type;
                    caseTypeComboBox.Items.Add(typeBoxItem);
                }

                // Binds the data and adds the Case Type ComboBox to the view
                caseTypeComboBox.SelectedValuePath = "Content";
                Binding caseTypeBinding = new Binding()
                {
                    Path = new PropertyPath("Type")
                };
                caseTypeComboBox.SetBinding(ComboBox.SelectedValueProperty, caseTypeBinding);
                Grid.SetRow(caseTypeComboBox, 1);
                Grid.SetColumn(caseTypeComboBox, 1);
                Grid.SetColumnSpan(caseTypeComboBox, 2);
                ProductGrid.Children.Add(caseTypeComboBox);

                // Creates and adds the Case FrontIO Label to the view
                Label caseFrontIOLbl = new Label();
                caseFrontIOLbl.Content = "Front IO";
                caseFrontIOLbl.HorizontalAlignment = HorizontalAlignment.Right;
                caseFrontIOLbl.VerticalAlignment = VerticalAlignment.Center;
                caseFrontIOLbl.FontSize = 14;
                Grid.SetRow(caseFrontIOLbl, 1);
                Grid.SetColumn(caseFrontIOLbl, 3);
                ProductGrid.Children.Add(caseFrontIOLbl);

                // Creates and styles the Case FrontIO ComboBox
                ComboBox caseFrontIOComboBox = new ComboBox();
                caseFrontIOComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                caseFrontIOComboBox.VerticalAlignment = VerticalAlignment.Center;
                caseFrontIOComboBox.FontSize = 14;
                caseFrontIOComboBox.Padding = new Thickness(6);
                caseFrontIOComboBox.Margin = new Thickness(0, 0, 10, 0);

                // Fills the Case FrontIO ComboBox
                foreach (var frontIO in caseData.FrontIOs)
                {
                    ComboBoxItem frontIOItem = new ComboBoxItem();
                    frontIOItem.Content = frontIO;
                    caseFrontIOComboBox.Items.Add(frontIOItem);
                }

                // Binds the data and adds the Case FrontIO ComboBox to the view
                caseFrontIOComboBox.SelectedValuePath = "Content";
                Binding caseFrontIOBinding = new Binding()
                {
                    Path = new PropertyPath("FrontIO")
                };
                caseFrontIOComboBox.SetBinding(ComboBox.SelectedValueProperty, caseFrontIOBinding);
                Grid.SetRow(caseFrontIOComboBox, 1);
                Grid.SetColumn(caseFrontIOComboBox, 4);
                Grid.SetColumnSpan(caseFrontIOComboBox, 2);
                ProductGrid.Children.Add(caseFrontIOComboBox);

                // Creates and adds the Case HasWindow Label to the view
                Label caseHasWindowLbl = new Label();
                caseHasWindowLbl.Content = "Side window";
                caseHasWindowLbl.HorizontalAlignment = HorizontalAlignment.Right;
                caseHasWindowLbl.VerticalAlignment = VerticalAlignment.Center;
                caseHasWindowLbl.FontSize = 14;
                Grid.SetRow(caseHasWindowLbl, 1);
                Grid.SetColumn(caseHasWindowLbl, 6);
                ProductGrid.Children.Add(caseHasWindowLbl);

                // Creates, binds the data and adds the Case HasWindow CheckBox to the view
                CheckBox caseHasWindowCheckBox = new CheckBox();
                caseHasWindowCheckBox.HorizontalAlignment = HorizontalAlignment.Left;
                caseHasWindowCheckBox.VerticalAlignment = VerticalAlignment.Center;
                Binding caseHasWindowBinding = new Binding()
                {
                    Path = new PropertyPath("HasWindow")
                };
                caseHasWindowCheckBox.SetBinding(CheckBox.IsCheckedProperty, caseHasWindowBinding);
                Grid.SetRow(caseHasWindowCheckBox, 1);
                Grid.SetColumn(caseHasWindowCheckBox, 7);
                ProductGrid.Children.Add(caseHasWindowCheckBox);

                // Sets active tab of the product list after save or cancel actions
                ProductBtns.Tag = caseProduct.GetType().Name;
            }
        }

        /// <summary>
        /// Triggers each time the bound Motherboard Platform property is updated.
        /// </summary>
        /// <param name="sender">The Motherboard Platform ComboBox.</param>
        /// <param name="e">Additional data for the event.</param>
        private void MotherboardPlatformUpdated(object sender, DataTransferEventArgs e)
        {
            ComboBox motherboardPlatformComboBox = (ComboBox)sender;
            ComboBox motherboardCpuSocketComboBox = FindName("motherboardCpuSocketComboBox") as ComboBox;
            ComboBox motherboardChipsetComboBox = FindName("motherboardChipsetComboBox") as ComboBox;

            if (motherboardPlatformComboBox.SelectedIndex == -1)
            {
                motherboardCpuSocketComboBox.IsEnabled = false;
                motherboardChipsetComboBox.IsEnabled = false;
                return;
            }

            UpdateMotherboardPlatform(sender);
        }

        /// <summary>
        /// Triggers each time the Motherboard Platform changes.
        /// </summary>
        /// <param name="sender">The Motherboard Platform ComboBox.</param>
        /// <param name="e">Additional data for the event.</param>
        private void MotherboardPlatformChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox motherboardPlatformComBox = (ComboBox)sender;

            if (!motherboardPlatformComBox.IsLoaded)
                return;

            ComboBox motherboardCpuSocketComboBox = FindName("motherboardCpuSocketComboBox") as ComboBox;
            ComboBox motherboardChipsetComboBox = FindName("motherboardChipsetComboBox") as ComboBox;

            if (!motherboardCpuSocketComboBox.IsEnabled)
                motherboardCpuSocketComboBox.IsEnabled = true;

            if (!motherboardChipsetComboBox.IsEnabled)
                motherboardChipsetComboBox.IsEnabled = true;

            motherboardCpuSocketComboBox.Items.Clear();
            motherboardChipsetComboBox.Items.Clear();
            UpdateMotherboardPlatform(sender);
        }

        /// <summary>
        /// Loads the appropriate Motherboard CPU sockets and Chipsets depending on the selected Platform.
        /// </summary>
        /// <param name="sender">The Motherboard Platform ComboBox.</param>
        private void UpdateMotherboardPlatform(object sender)
        {
            MotherboardData motherboardData = new MotherboardData();
            ComboBox motherboardPlatformComboBox = (ComboBox)sender;
            List<string> motherboardCpuSocketList;
            List<string> motherboardChipsetList;

            if ((string)motherboardPlatformComboBox.SelectedValue == "AMD") // AMD
            {
                motherboardCpuSocketList = motherboardData.CPUSockets.First();
                motherboardChipsetList = motherboardData.Chipsets.First();
            }
            else // Intel
            {
                motherboardCpuSocketList = motherboardData.CPUSockets.Last();
                motherboardChipsetList = motherboardData.Chipsets.Last();
            }

            // Fills the Motherboard CPUSocket ComboBox
            foreach (var cpuSocket in motherboardCpuSocketList)
            {
                ComboBoxItem cpuSocketItem = new ComboBoxItem();
                cpuSocketItem.Content = cpuSocket;
                ComboBox motherboardCpuSocketComboBox = FindName("motherboardCpuSocketComboBox") as ComboBox;
                motherboardCpuSocketComboBox.Items.Add(cpuSocketItem);
            }

            // Fills the Motherboard Chipset ComboBox
            foreach (var chipset in motherboardChipsetList)
            {
                ComboBoxItem chipsetItem = new ComboBoxItem();
                chipsetItem.Content = chipset;
                ComboBox motherboardChipsetComboBox = FindName("motherboardChipsetComboBox") as ComboBox;
                motherboardChipsetComboBox.Items.Add(chipsetItem);
            }
        }

        /// <summary>
        /// Triggers each time the bound Storage Type property is updated.
        /// </summary>
        /// <param name="sender">The Storage Type ComboBox.</param>
        /// <param name="e">Additional data for the event.</param>
        private void StorageTypeUpdated(object sender, DataTransferEventArgs e)
        {
            ComboBox productTypeComboBox = (ComboBox)sender;

            if (productTypeComboBox.SelectedIndex == -1)
            {
                ProductBrandTxt.IsEnabled = false;
                return;
            }

            UpdateStorageBrand(sender);
        }

        /// <summary>
        /// Triggers each time the Storage Type changes.
        /// </summary>
        /// <param name="sender">The Storage Type ComboBox.</param>
        /// <param name="e">Additional data for the event.</param>
        private void StorageTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox storageTypeComboBox = (ComboBox)sender;

            if (!storageTypeComboBox.IsLoaded)
                return;

            if (!ProductBrandTxt.IsEnabled)
                ProductBrandTxt.IsEnabled = true;

            ProductBrandTxt.Items.Clear();
            UpdateStorageBrand(sender);
        }

        /// <summary>
        /// Loads the appropriate Storage Brands depending on the selected Yype.
        /// </summary>
        /// <param name="sender">The Storage type ComboBox.</param>
        private void UpdateStorageBrand(object sender)
        {
            StorageData storageData = new StorageData();
            ComboBox storageTypeComboBox = (ComboBox)sender;

            var brandList = (string)storageTypeComboBox.SelectedValue == "SSD" ? storageData.Brands.First() : storageData.Brands.Last();

            // Fills the Storage Brand ComboBox
            foreach (var brand in brandList)
            {
                ComboBoxItem brandItem = new ComboBoxItem();
                brandItem.Content = brand;
                ProductBrandTxt.Items.Add(brandItem);
            }
        }

        /// <summary>
        /// Cancels the addition or edition of a product and navigates back to the product list.
        /// </summary>
        /// <param name="sender">The caller object.</param>
        /// <param name="e">Additional parameters for the event.</param>
        private void CancelProductBtn_Click(object sender, RoutedEventArgs e)
        {
            string activeTabName = (string)Utility.FindParent<StackPanel>((Button)sender).Tag;
            Utility.FindParent<Page>(this).NavigationService.Navigate(new ProductListView(activeTabName));
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
        private void OnlyNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9]");
        }

        /// <summary>
        /// Only allows numbers and periods in the field.
        /// </summary>
        /// <param name="sender">Element firing the event.</param>
        /// <param name="e">Arguments associated with changes to a TextComposition.</param>
        private void OnlyNumbersPeriods_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"[0-9.]");
        }
        #endregion
    }
}
