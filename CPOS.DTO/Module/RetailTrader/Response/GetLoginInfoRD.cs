using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.RetailTrader.Response
{
    public class GetLoginInfoRD : IAPIResponseData
    {
        ///分销商用户名
        public string SuperRetailTraderName { get; set; }
        /// <summary>
        /// 分销商联系人
        /// </summary>
        public string SuperRetailTraderMan { get; set; }
        /// <summary>
        /// 分销商电话
        /// </summary>
        public string SuperRetailTraderPhone { get; set; }
        /// <summary>
        /// 分销商地址
        /// </summary>
        public string SuperRetailTraderAddress { get; set; }
        /// <summary>
        /// 分销商类型
        /// </summary>
        public string SuperRetailTraderFrom { get; set; }
        /// <summary>
        /// 加盟时间
        /// </summary>
        public DateTime? JoinTime { get; set; }
        /// <summary>
        /// 商户编号
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
