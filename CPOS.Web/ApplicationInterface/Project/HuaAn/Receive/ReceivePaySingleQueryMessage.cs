using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Receive
{
    /// <summary>
    /// 货币基金支付单笔查询。3001
    /// </summary>
    public class ReceivePaySingleQueryMessage
    {
        /// <summary>
        /// 商家ID
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// 商户日期
        /// </summary>
        public string Merchantdate { get; set; }

        /// <summary>
        /// 原定单商户日期
        /// </summary>
        public string Orgmerchantdate { get; set; }

        /// <summary>
        /// 原定单号
        /// </summary>
        public string OrgorderNO { get; set; }

        /// <summary>
        /// 原交易类型
        /// </summary>
        public string Orgtxcode { get; set; }

        /// <summary>
        /// 公共回传字段
        /// </summary>
        public string Commonreturn { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        public string RetURL { get; set; }

        /// <summary>
        /// 定单描述
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 客户协议号
        /// </summary>
        public string Assignbuyer { get; set; }

    }
}