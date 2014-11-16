using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.Interface.Data.Base
{
    public class CommonReqPara
    {
        public string customerId { get; set; }
        public string locale { get; set; }//：语言，预留，支持多语言 zh
        public string userId { get; set; }//：用户Id
        public string businessZoneId { get; set; }//		：商图ID
        public string plat { get; set; }//：客户端平台分别：iphone、android
        public string deviceToken { get; set; }//：客户端设备硬件串号
        public string osInfo { get; set; }//：操作系统信息
        public string channelId { get; set; }//: 渠道ID
        public string openId { get; set; }
        public string isALD { get; set; }//:			是否同步到阿拉丁1=是（Jermyn20140314）

    }
}