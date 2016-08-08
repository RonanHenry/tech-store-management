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
using TechStoreLibrary.Models;

namespace TechStoreWpf.UserControls
{
    /// <summary>
    /// Interaction logic for WorkerUserControl.xaml
    /// </summary>
    public partial class WorkerUserControl : UserControl
    {
        public WorkerUserControl()
        {
            InitializeComponent();
        }

        private void ChangeWorkerBtn_Click(object sender, RoutedEventArgs e)
        {
            Worker worker = (Worker)WorkerGrid.DataContext;
            worker.FirstName = "Martine";
        }
    }
}
