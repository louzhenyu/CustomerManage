using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send
{
    /// <summary>
    /// 货币基金支付单笔查询接口库3001
    /// </summary>
    public class SendPaySingleQueryMessage
    {

        /// <summary>
        /// 
        /// </summary>
        public string MerchantID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Orgmerchantdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrgorderNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrgHatradedate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrgTxcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrgTotalPayAmt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrgorderRet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrgorderMsg { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Retcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Retmsg { get; set; }

        /// <summary>
        /// 公共回传字段
        /// </summary>
        public string Commonreturn { get; set; }

    }
}