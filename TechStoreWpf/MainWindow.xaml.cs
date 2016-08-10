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

namespace TechStoreWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            const int initialWidth = 1100;
            const int initialHeight = 650;

            var verticalBorderWidth = SystemParameters.ResizeFrameVerticalBorderWidth;
            var horizontalBorderHeight = SystemParameters.ResizeFrameHorizontalBorderHeight;
            var captionHeight = SystemParameters.CaptionHeight;

            Width = initialWidth + 2 * verticalBorderWidth;
            Height = initialHeight + captionHeight + 2 * horizontalBorderHeight;
        }
    }
}
