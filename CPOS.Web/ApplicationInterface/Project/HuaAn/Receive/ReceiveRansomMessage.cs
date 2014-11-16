using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Receive
{
    /// <summary>
    /// 请求基金赎回消息类定义。
    /// </summary>
    public class ReceiveRansomMessage
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
        /// 交易号(对应资产)
        /// </summary>
        public string Logisticsinfo { get; set; }

        /// <summary>
        /// 交易附加信息
        /// </summary>
        public string Tradeappendinfo { get; set; }

        /// <summary>
        /// 客户协议号
        /// </summary>
        public string Assignbuyer { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string Assbuyername { get; set; }

        /// <summary>
        /// 客户手机号
        /// </summary>
        public string Assbuyermobile { get; set; }

        /// <summary>
        /// 手续费(此处填0)
        /// </summary>
        public string Fee { get; set; }

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
        /// 回传地址。
        /// </summary>
        public string PageURL { get; set; }

    }
}