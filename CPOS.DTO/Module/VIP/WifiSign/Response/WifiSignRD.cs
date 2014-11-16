using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.WifiSign.Response
{
    public class WifiSignRD : IAPIResponseData
    {
        /// <summary>
        /// 数据
        /// </summary>
        public VipInfo[] Items { get; set; }
    }

    public class VipInfo
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 位置描述
        /// </summary>
        public string LocationDesc { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 微信服务号
        /// </summary>
        public string WeiXin { get; set; }
        /// <summary>
        /// 头像URL
        /// </summary>
        public string VipPhoto { get; set; }
    }
}
