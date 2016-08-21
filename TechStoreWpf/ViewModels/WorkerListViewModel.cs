using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStoreLibrary.Database;
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.ViewModels.Base;
using TechStoreWpf.Views;
using TechStoreLibrary.Enums;
using System.Windows.Controls;
using System.Windows;
using System.Threading;
using TechStoreWpf.Views.Windows;

namespace TechStoreWpf.ViewModels
{
    public class WorkerListViewModel : BaseViewModel
    {
        #region Attributes
        private WorkerListView workerListView;
        private Window waitWindow;
        private ObservableCollection<Worker> workers;
        #endregion

        #region Properties
        /// <summary>
        /// Worker list view.
        /// </summary>
        public WorkerListView WorkerListView
        {
            get
            {
                return workerListView;
            }
            set
            {
                workerListView = value;
            }
        }

        /// <summary>
        /// Data loading window.
        /// </summary>
        public Window WaitWindow
        {
            get
            {
                return waitWindow;
            }
            set
            {
                waitWindow = value;
            }
        }

        /// <summary>
        /// List of workers.
        /// </summary>
        public ObservableCollection<Worker> Workers
        {
            get
            {
                return workers;
            }
            set
            {
                workers = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddWorkerCommand { get; private set; }
        public ICommand EditWorkerCommand { get; private set; }
        public ICommand DeleteWorkerCommand { get; private set; }
        #endregion

        #region Constructors
        public WorkerListViewModel(WorkerListView workerListView)
        {
            WaitWindow = new WaitWindow();
            WorkerListView = workerListView;

            Task.Run(() => LoadWorkersAsync());

            AddWorkerCommand = new RelayCommand(ExecAddWorker, CanAddWorker);
            EditWorkerCommand = new RelayCommand(ExecEditWorker, CanEditWorker);
            DeleteWorkerCommand = new RelayCommand(ExecDeleteWorkerAsync, CanDeleteWorker);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads all workers.
        /// </summary>
        public async void LoadWorkersAsync()
        {
            #pragma warning disable CS4014
            Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                try
                {
                    WaitWindow.ShowDialog();
                }
                catch (InvalidOperationException e)
                {

                }

            }));
            #pragma warning restore CS4014

            App.SetConnectionResource();

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    Workers = new ObservableCollection<Worker>(await new WebServiceManager<Worker>().GetAllAsync());
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        Workers = new ObservableCollection<Worker>(await ctx.DbSetWorkers.Include(w => w.Address).ToListAsync());
                    }
                    break;
                default:
                    break;
            }

            await Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() =>
            {
                WaitWindow.Close();
            }));
        }

        private bool CanAddWorker(object obj)
        {
            return true;
        }

        /// <summary>
        /// Navigates to the worker form view.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecAddWorker(object obj)
        {
            WorkerListView.NavigationService.Navigate(new WorkerView());
        }

        /// <summary>
        /// Edition is active only when a worker is selected.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanEditWorker(object obj)
        {
            if (WorkerListView.WorkerListUserControl.WorkerList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Navigates to the worker form view in edit mode.
        /// </summary>
        /// <param name="obj"></param>
        private void ExecEditWorker(object obj)
        {
            WorkerView workerView = new WorkerView((Worker)WorkerListView.WorkerListUserControl.WorkerList.SelectedItem);
            WorkerListView.NavigationService.Navigate(workerView);
        }

        /// <summary>
        /// Deletion is active only when a worker is selected. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool CanDeleteWorker(object obj)
        {
            if (WorkerListView.WorkerListUserControl.WorkerList.SelectedIndex != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes selected worker.
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecDeleteWorkerAsync(object obj)
        {
            Worker worker = (Worker)WorkerListView.WorkerListUserControl.WorkerList.SelectedItem;
            Workers.Remove(worker);

            switch (App.DataSource)
            {
                case ConnectionResource.LOCALAPI:
                    await new WebServiceManager<Worker>().DeleteAsync(worker);
                    break;
                case ConnectionResource.LOCALMYSQL:
                    using (var ctx = new MysqlDbContext(ConnectionResource.LOCALMYSQL))
                    {
                        ctx.DbSetWorkers.Attach(worker);
                        ctx.DbSetWorkers.Remove(worker);
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
