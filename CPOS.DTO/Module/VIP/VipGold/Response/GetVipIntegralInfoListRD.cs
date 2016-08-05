using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module
{
    /// <summary>
    /// 微信积分说明返回参数
    /// </summary>
    public class GetVipIntegralInfoListRD : IAPIResponseData
    {
        public List<VipIntegralInfo> VipIntegralInfoList { get; set; }
        public GetVipIntegralInfoListRD()
        {
            VipIntegralInfoList = new List<VipIntegralInfo>();
        }
    }

    public class VipIntegralInfo
    {
        /// <summary>
        /// 积分设置值
        /// </summary>
        public string CustomerBaseSettingValue { get; set; }
        /// <summary>
        /// 积分设置描述
        /// </summary>
        public string CustomerBaseSettingDesc { get; set; }
    }
}