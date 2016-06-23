using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Response
{
    public class SetSignInRD : IAPIResponseData
    {
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string UserName { get; set; }
        public string SuperRetailTraderID { get; set; }
        /// <summary>
        /// 用户状态1：正常；-1不正常
        /// </summary>
        public int Status { get; set; }
        public RoleCodeInfo[] RoleCodeList { get; set; }
        //用户汇集店ID
        public string UnitId { get; set; }
           //用户汇集店ID
        public string UnitName { get; set; }
        
        /// <summary>
        /// app权限code数组
        /// </summary>
        public List<Menu> MenuCodeList { get; set; }
        /// <summary>
        /// app配置信息
        /// </summary>
        public List<CustomerBasicCodeInfo> CustomerBasicCodeList { get; set; }

        //头像信息
        public string HeadImg { get; set; }

    }

    public class RoleCodeInfo
    {
        public string RoleCode { get; set; }
 
    }
    /// <summary>
    /// app权限code
    /// </summary>
    public class Menu
    {
        public string MenuCode { get; set; }
    }

    /// <summary>
    /// 获取配置信息
    /// </summary>
    public class CustomerBasicCodeInfo
    {
        public string WebLogo { get; set; }
    }
}
