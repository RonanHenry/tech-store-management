using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TechStoreLibrary.Database;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Models;
using TechStoreWpf.Helpers;
using TechStoreWpf.ViewModels.Base;
using TechStoreWpf.Views;

namespace TechStoreWpf.ViewModels
{
    public class WorkerViewModel : BaseViewModel
    {
        #region Attributes
        private WorkerView workerView;
        private Worker worker;
        #endregion

        #region Properties
        /// <summary>
        /// Worker view.
        /// </summary>
        public WorkerView WorkerView
        {
            get
            {
                return workerView;
            }
            set
            {
                workerView = value;
            }
        }

        /// <summary>
        /// Instance of the Worker model.
        /// </summary>
        public Worker Worker
        {
            get
            {
                return worker;
            }
            set
            {
                worker = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveWorkerCommand { get; private set; }
        #endregion

        #region Constructors
        public WorkerViewModel(WorkerView workerView)
        {
            WorkerView = workerView;
            Worker = new Worker();
            Worker.Address = new Address();

            SaveWorkerCommand = new RelayCommand(ExecSaveWorkerAsync, CanSaveWorker);
        }
        #endregion

        #region Methods
        private bool CanSaveWorker(object obj)
        {
            return true;
        }

        /// <summary>
        /// Adds or updates a worker. 
        /// </summary>
        /// <param name="obj"></param>
        private async void ExecSaveWorkerAsync(object obj)
        {
            using (var ctx = new MysqlDbContext(App.DataSource))
            {
                switch (App.DataSource)
                {
                    case ConnectionResource.LOCALAPI:
                        // Code API
                        break;
                    case ConnectionResource.LOCALMYSQL:
                        if (Worker.Id == 0) // Saving new worker
                        {
                            ctx.DbSetWorkers.Add(Worker);
                            await ctx.SaveChangesAsync();
                        }
                        else // Saving updated worker
                        {
                            ctx.Entry(Worker).State = EntityState.Modified;
                            ctx.Entry(Worker.Address).State = EntityState.Modified;
                            await ctx.SaveChangesAsync();
                        }
                        break;
                    default:
                        break;
                }

                WorkerView.NavigationService.Navigate(new WorkerListView());
            }
        }
        #endregion
    }
}
