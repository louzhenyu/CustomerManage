
using System;
using System.Collections.Generic;
namespace JIT.CPOS.BS.BLL.WX.Const
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 微信平台验证token
        /// </summary>
      public const string TOKEN = "jitmarketing";
     //   public const string TOKEN = "zmindclouds";
      public static Dictionary<string, string> ListMsgId = new Dictionary<string, string>();//全局的变量存储信息
    }
   
}
