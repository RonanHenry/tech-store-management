﻿using System;
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

namespace TechStoreWpf.ViewModels
{
    public class WorkerListViewModel : BaseViewModel
    {
        #region Attributes
        private WorkerListView workerListView;
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
