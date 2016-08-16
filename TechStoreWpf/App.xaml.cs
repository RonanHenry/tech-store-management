using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        private static TextBlock connectionStatus;
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

        /// <summary>
        /// Connection status TextBlock.
        /// </summary>
        public static TextBlock ConnectionStatus
        {
            get
            {
                return connectionStatus;
            }
            set
            {
                connectionStatus = value;
            }
        }
        #endregion

        #region Constructors
        public App()
        {
            ConnectionStatus = null;
            SetConnectionResource();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the connection status UI element.
        /// </summary>
        /// <param name="connectionStatus">The connection status UI element.</param>
        public static void SetConnectionStatus(TextBlock connectionStatus)
        {
            ConnectionStatus = connectionStatus;
        }

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
                    Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() =>
                    {
                        if (ConnectionStatus != null)
                            ConnectionStatus.Text = "Connected to : Web Service";
                    }));
                }
                else // Web service responded but might be broken
                {
                    DataSource = ConnectionResource.LOCALMYSQL;
                    Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() =>
                    {
                        if (ConnectionStatus != null)
                            ConnectionStatus.Text = "Connected to : Local Database";
                    }));
                }
            }
            catch (Exception e) // Web service is not available
            {
                DataSource = ConnectionResource.LOCALMYSQL;
                Application.Current.Dispatcher.BeginInvoke(new ThreadStart(() =>
                {
                    if (ConnectionStatus != null)
                        ConnectionStatus.Text = "Connected to : Local Database";
                }));
                
            }
        }
        #endregion
    }
}
