using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Database;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Models;
using TechStoreWpf.ViewModels.Base;

namespace TechStoreWpf.ViewModels
{
    public class WorkerListViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<Worker> workers;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors
        public WorkerListViewModel()
        {
            Task.Run(() => LoadWorkers());
        }
        #endregion

        #region Methods
        public async void LoadWorkers()
        {
            MysqlManager<Worker> workerManager = new MysqlManager<Worker>(ConnectionResource.LOCALMYSQL);
            Workers = new ObservableCollection<Worker>(await workerManager.Get());
        }
        #endregion
    }
}
