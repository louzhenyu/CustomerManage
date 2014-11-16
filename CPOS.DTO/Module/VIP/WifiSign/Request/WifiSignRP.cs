using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.WifiSign.Request
{
    public class WifiSignRP : IAPIRequestParameter
    {
        public void Validate()
        {
            if (string.IsNullOrEmpty(DeviceID))
                throw new Exception("DeviceID不能为空");
            //if (string.IsNullOrEmpty(UserID))
            //    throw new Exception("UserID不能为空");
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        //public string UserID { get; set; }
        /// <summary>
        /// 微信用户ID
        /// </summary>
        //public string OpenID { get; set; }
        /// <summary>
        /// 用户的WiFiCenterSessionKey
        /// </summary>
        public string DeviceID { get; set; }
    }
}
