﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TechStoreLibrary.Models.Base
{
    /// <summary>
    /// Defines the base of all models (INotifyPropertyChanged implementation).
    /// </summary>
    public class BaseModel : INotifyPropertyChanged
    {
        #region Attributes
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Methods
        /// <summary>
        /// We retrieve the property name that triggered the event using CallerMemberName.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
