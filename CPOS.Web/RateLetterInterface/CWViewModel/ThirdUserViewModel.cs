using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.RateLetterInterface
{
    public class ThirdUserViewModel
    {
        /// <summary>
        /// 请求状态码，取值000000（成功）
        /// </summary>
        public string statusCode { get; set; }

        /// <summary>
        /// 错误消息。
        /// </summary>
        public string statusMsg { get; set; }

        /// <summary>
        /// SubAccount
        /// </summary>
        public SubAccount SubAccount { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class SubAccount
    {
        /// <summary>
        /// 子账户Id。由32个英文字母和阿拉伯数字组成的子账户唯一标识符
        /// </summary>
        public string subAccountSid { get; set; }

        /// <summary>
        /// 子账户的授权令牌。由32个英文字母和阿拉伯数字组成
        /// </summary>
        public string subToken { get; set; }


        /// <summary>
        /// 子账户的创建时间
        /// </summary>
        public string dateCreated { get; set; }


        /// <summary>
        /// VoIP号码。由14位数字组成；若应用为语音类，voipAccount 不为空；若应用为短信类，voipAccount 为空
        /// </summary>
        public string voipAccount { get; set; }


        /// <summary>
        /// VoIP密码。由8位数字和字母组成；若应用为语音类，voipPwd不为空；若应用为短信类，voipPwd为空
        /// </summary>
        public string voipPwd { get; set; }
    }

}