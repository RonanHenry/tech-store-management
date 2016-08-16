using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using TechStoreLibrary.Enums;
using TechStoreLibrary.Utilities;

namespace TechStoreWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Attributes
        private static ConnectionResource dataSource;
        #endregion

        #region Properties
        /// <summary>
        /// Sets the connection resource to use (API if available, local database otherwise).
        /// </summary>
        public static ConnectionResource DataSource
        {
            get
            {
                return dataSource;
            }
            set
            {
                dataSource = value;
            }
        }
        #endregion

        #region Constructors
        public App()
        {
            SetConnectionResource();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Does a request to the web service to test if it is available.
        /// </summary>
        public static void SetConnectionResource()
        {
            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(EnumString.GetStringValue(ConnectionResource.LOCALAPI));
                var response = (HttpWebResponse)webRequest.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK) // Web service is up and running
                {
                    DataSource = ConnectionResource.LOCALAPI;
                }
                else // Web service responded but might be broken
                {
                    DataSource = ConnectionResource.LOCALMYSQL;
                }
            }
            catch (Exception e) // Web service is not available
            {
                DataSource = ConnectionResource.LOCALMYSQL;
            }
        }
        #endregion
    }
}
