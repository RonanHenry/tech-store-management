using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStoreLibrary.Utilities;

namespace TechStoreLibrary.Enums
{
    public enum ConnectionResource : int
    {
        [StringValue("http://localhost:59018/")]
        LOCALAPI = 1,
        [StringValue("Server=localhost;Port=3306;Database=rh_api_tech_store;Uid=root;Pwd=''")]
        APIMYSQL = 2,
        [StringValue("Server=localhost;Port=3306;Database=rh_local_tech_store;Uid=root;Pwd=''")]
        LOCALMYSQL = 3
    }
}
