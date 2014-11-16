using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.VipList.Response
{
    public class GetVipListRD : IAPIResponseData
    {
        /// <summary>
        /// 数据
        /// </summary>
        public VipInfo[] VipList { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalRow { get; set; }
    }

    public class VipInfo
    {
        /// <summary>
        /// 头像URL
        /// </summary>
        public string VipPhoto { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string VipName { get; set; }
        /// <summary>
        /// 访问区
        /// </summary>
        public string VipArea { get; set; }
        /// <summary>
        /// 登录时间，返回几分钟前
        /// </summary>
        public string VipTime { get; set; }
    }
}
