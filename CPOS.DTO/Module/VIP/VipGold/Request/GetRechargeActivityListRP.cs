using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetRechargeActivityListRP : IAPIRequestParameter
    {       
        /// <summary>
        /// 会员ID
        /// </summary>
        public string VipID { get; set; }
        public void Validate()
        {
            const int ERROR_LACK_VipID = 312;//VipID不能为空
            if (string.IsNullOrEmpty(VipID))
            {
                throw new APIException("请求参数中缺少VipID或值为空.") { ErrorCode = ERROR_LACK_VipID };
            }
        } 
    }
}
