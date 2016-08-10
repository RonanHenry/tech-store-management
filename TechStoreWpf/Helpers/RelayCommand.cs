using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TechStoreWpf.Helpers
{
    /// <summary>
    /// A command whose purpose is to relay its functionality to other objects by invoking delegates. 
    /// The default return value for the CanExecute method is true.
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Attributes
        readonly Action<object> execute;
        readonly Predicate<object> canExecute;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region Methods
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion
    }
}
