using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechStoreLibrary.Database;
using TechStoreLibrary.Enums;

namespace TechStoreWeb.Controllers.Base
{
    public class BaseController : ApiController
    {
        #region Attributes
        private MysqlDbContext dataResource;
        #endregion

        #region Properties
        /// <summary>
        /// API Data source.
        /// </summary>
        public MysqlDbContext DataResource
        {
            get
            {
                return dataResource;
            }
            set
            {
                dataResource = value;
            }
        }
        #endregion

        #region Constructors
        public BaseController()
        {
            DataResource = new MysqlDbContext(ConnectionResource.APIMYSQL);
        }
        #endregion

        #region Methods

        #endregion
    }
}
