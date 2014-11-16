using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Project.HuaAn.Send
{
    /// <summary>
    /// 回复基金赎回消息类定义。
    /// </summary>
    public class SendRansomMessage
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
        /// 赎回的实际份额
        /// </summary>
        public string Orgtotalamt { get; set; }
        
        /// <summary>
        /// 买家信息(客户协议号)	
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
        /// 客户证件类型(0为身份证)
        /// </summary>
        public string Assbuyeridtp { get; set; }

        /// <summary>
        /// 交易号(对应资产)
        /// </summary>
        public string Logisticsinfo { get; set; }

        /// <summary>
        /// 手续费(此处为0)
        /// </summary>
        public string Fee { get; set; }
        
        /// <summary>
        /// 交易类型（1：申购；2：赎回）
        /// </summary>
        public string Fundtype { get; set; }

        /// <summary>
        /// 返回码(0000为成功)
        /// </summary>
        public string Retcode { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Retmsg { get; set; }

        /// <summary>
        /// 公共回传字段
        /// </summary>
        public string Commonreturn { get; set; }
    }
}