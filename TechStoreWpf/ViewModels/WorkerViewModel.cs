using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Database;
using TechStoreLibrary.Models;
using TechStoreWpf.ViewModels.Base;

namespace TechStoreWpf.ViewModels
{
    public class WorkerViewModel : BaseViewModel
    {
        #region Attributes
        private Worker worker;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors
        public WorkerViewModel()
        {
            Address address = new Address("Rue Saint Michel", "35190", "Bécherel", "France");
            Worker = new Worker("Ronan", "Henry", address, "Owner", "ronan.henry@tech-store.com");
        }
        #endregion

        #region Methods

        #endregion
    }
}
