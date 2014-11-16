using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Receive
{
    /// <summary>
    /// 资产及收益  5000
    /// </summary>
    public class ReceiveFundProfitMessage
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
        /// 当前请求页码（默认填1）
        /// </summary>
        public string Pageno { get; set; }

        /// <summary>
        /// 返回地址
        /// </summary>
        public string RetURL { get; set; }

        /// <summary>
        /// 定单描述
        /// </summary>
        public string Memo { get; set; }

    }
}