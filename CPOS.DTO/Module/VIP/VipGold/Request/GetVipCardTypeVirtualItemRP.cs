using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetVipCardTypeVirtualItemRP : IAPIRequestParameter
    {
        /// <summary>
        /// 应用类型 1=微信请求  2=APP请求
        /// </summary>
        public string ApplicationType { get; set; }
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(ApplicationType))
            {
                const int ERROR_LACK_ApplicationType = 311;//ApplicationType不能为空 
                const int ERROR_LACK_VipID = 312;//VipID不能为空
                if (string.IsNullOrEmpty(ApplicationType))
                {
                    throw new APIException("请求参数中缺少ApplicationType或值为空.") { ErrorCode = ERROR_LACK_ApplicationType };
                }
                else
                {
                    if (ApplicationType == "2")
                    {
                        if (string.IsNullOrEmpty(VipID))
                        {
                            throw new APIException("请求参数中缺少VipID或值为空.") { ErrorCode = ERROR_LACK_VipID };
                        }
                    }
                }
            }
        } 
    }
}
