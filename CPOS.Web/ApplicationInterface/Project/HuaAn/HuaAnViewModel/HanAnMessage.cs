
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn
{
    /// <summary>
    /// 华安响应消息处理类。
    /// </summary>
    public class HanAnMessage
    {
        /// <summary>
        /// 版本号(20140401)
        /// </summary>
        public string verNum { get; set; }

        /// <summary>
        /// 系统日期(yyyymmdd)
        /// </summary>
        public string sysdate { get; set; }

        /// <summary>
        /// 系统时间(hhmmss)
        /// </summary>
        public string systime { get; set; }

        /// <summary>
        /// 交易代码
        /// </summary>
        public string txcode { get; set; }

        /// <summary>
        /// 世联通讯流水号，与请求表单对应
        /// </summary>
        public string seqNO { get; set; }

        /// <summary>
        /// 校验串，对 明文+密钥 进行MD5
        /// </summary>
        public string maccode { get; set; }

        /// <summary>
        /// 加密内容
        /// </summary>
        public string content { get; set; }
    }


    /// <summary>
    /// 华安请求消息处理类。
    /// </summary>
    public class HanAnRequestMessage : HanAnMessage
    {
        /// <summary>
        /// 商家ID （请求中需要用，响应中无返回此字段值）
        /// </summary>
        public string merchantID { get; set; }
    }
}