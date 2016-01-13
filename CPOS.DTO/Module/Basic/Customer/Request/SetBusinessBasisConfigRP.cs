using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.Basic.Customer.Request
{
    public class SetBusinessBasisConfigRP : IAPIRequestParameter
    {
        /// <summary>
        /// 商户全称
        /// </summary>
        public string customer_name { get; set; }
        /// <summary>
        /// 商户简称
        /// </summary>
        public string CustomerShortName { get; set; }
        /// <summary>
        /// 商户Logo
        /// </summary>
        public string WebLogo { get; set; }
        /// <summary>
        /// 客服电话
        /// </summary>
        public string CustomerPhone { get; set; }
        /// <summary>
        /// 分享标题
        /// </summary>
        public string ForwardingMessageTitle { get; set; }
        /// <summary>
        /// 分享图片
        /// </summary>
        public string ForwardingMessageLogo { get; set; }
        /// <summary>
        /// 分享摘要内容
        /// </summary>
        public string ForwardingMessageSummary { get; set; }
        /// <summary>
        /// 引导链接
        /// </summary>
        public string GuideLinkUrl { get; set; }
        /// <summary>
        /// 引导二维码
        /// </summary>
        public string GuideQRCode { get; set; }
        /// <summary>
        /// 客服欢迎语
        /// </summary>
        public string CustomerGreeting { get; set; }


        public void Validate()
        {

        }
    }
}
